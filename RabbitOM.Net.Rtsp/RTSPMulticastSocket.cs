using System;
using System.Net;
using System.Net.Sockets;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent an multicast socket
    /// </summary>
    internal sealed class RTSPMulticastSocket : IDisposable
    {
        private const int DefaultReceiveBufferSize = ushort.MaxValue;




        private IPAddress   _ipAddress       = null;

        private IPEndPoint  _groupEP         = null;

        private Socket      _socket          = null;

        private byte[]      _buffer          = null;




        /// <summary>
        /// Check if the socket has been opened
        /// </summary>
        public bool IsOpened
        {
            get => _socket != null;
        }





        /// <summary>
        /// Open the socket
        /// </summary>
        /// <param name="ipAddress">the ip address</param>
        /// <param name="port">the port</param>
        /// <param name="ttl">the ttl</param>
        /// <param name="receiveTimeout">the receive timeout</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Open( string ipAddress , int port , int ttl , TimeSpan receiveTimeout )
        {
            if ( string.IsNullOrWhiteSpace( ipAddress ) || port < 0 || ttl < 0 )
            {
                return false;
            }

            if ( _socket != null )
            {
                return false;
            }

            if ( !IPAddress.TryParse( ipAddress , out _ipAddress ) || _ipAddress == null )
            {
                return false;
            }

            try
            {
                _buffer = new byte[DefaultReceiveBufferSize];

                _groupEP = new IPEndPoint( IPAddress.Any , port );
                
                _socket = new Socket(_ipAddress.AddressFamily , SocketType.Dgram , ProtocolType.Udp );

                _socket.ExclusiveAddressUse = false;
                _socket.SetSocketOption( SocketOptionLevel.Socket , SocketOptionName.ReuseAddress , true );
                _socket.ReceiveTimeout = (int) receiveTimeout.TotalMilliseconds;
                _socket.ReceiveBufferSize = DefaultReceiveBufferSize;

                if ( _ipAddress.AddressFamily == AddressFamily.InterNetwork )
                {
                    _socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption( _ipAddress ));
                    _socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, ttl);
                }

                if (_ipAddress.AddressFamily == AddressFamily.InterNetworkV6)
                {
                    _socket.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.AddMembership, new IPv6MulticastOption(_ipAddress));
                    _socket.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.MulticastTimeToLive, ttl);
                }

                _socket.SetSocketOption(_ipAddress.AddressFamily == AddressFamily.InterNetwork ? SocketOptionLevel.IP : SocketOptionLevel.IPv6 , SocketOptionName.MulticastTimeToLive , ttl );

                _socket.Bind(_groupEP);

                return true;
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            Close();

            return false;
        }

        /// <summary>
        /// Close the socket
        /// </summary>
        public void Close()
        {
            try
            {
                if ( _socket != null && _ipAddress != null )
                {
                    if (_ipAddress.AddressFamily == AddressFamily.InterNetwork)
                    {
                        _socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.DropMembership, new MulticastOption(_ipAddress));
                    }

                    if (_ipAddress.AddressFamily == AddressFamily.InterNetworkV6)
                    {
                        _socket.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.DropMembership, new IPv6MulticastOption(_ipAddress));
                    }
                }
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            _socket?.Close();
            _socket?.Dispose();
            _socket = null;
            _groupEP = null;
            _ipAddress = null;
            _buffer = null;
        }

        /// <summary>
        /// Release internal resources
        /// </summary>
        public void Dispose()
        {
            Close();
        }

        /// <summary>
        /// Wait data during the specified timeout
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <returns>return true for a success, otherwise false</returns>
        public bool PollReceive( TimeSpan timeout )
        {
            if ( _socket == null )
            {
                return false;
            }

            try
            {
                return _socket.Poll( (int) ( timeout.TotalMilliseconds * 1000 ) , SelectMode.SelectRead );
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return false;
        }

        /// <summary>
        /// Read bytes
        /// </summary>
        /// <returns>returns an array of bytes, otherwise null</returns>
        public byte[] Receive()
        {
            if ( _socket == null || _buffer == null || _buffer.Length <= 0 )
            {
                return null;
            }

            var endpoint = _groupEP as EndPoint;

            if (endpoint == null )
            {
                return null;
            }

            try
            {
                var bytesReceived = _socket.ReceiveFrom(_buffer , ref endpoint);

                if ( bytesReceived > 0 )
                {
                    var buffer = new byte[bytesReceived];

                    Buffer.BlockCopy(_buffer , 0 , buffer , 0 , buffer.Length );

                    return buffer;
                }
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return null;
        }





        /// <summary>
        /// Occurs when an error has been detected
        /// </summary>
        /// <param name="exception">the exception</param>
        private void OnError( Exception exception )
        {
            if ( exception == null )
            {
                return;
            }

            System.Diagnostics.Debug.WriteLine( exception );
        }
    }
}
