using System;
using System.Net;
using System.Net.Sockets;

namespace RabbitOM.Streaming.Rtsp
{
    internal sealed class RtspMulticastSocket : IDisposable
    {
        private const int DefaultReceiveBufferSize = 8 * 1024 * 1024;

        private IPAddress _ipAddress;
        private IPEndPoint _groupEP;
        private Socket _socket;
        private byte[] _buffer;

        public bool IsOpened
        {
            get => _socket != null;
        }

        // TODO: remove the try catch and bool return value
        public bool Open( string ipAddress , int port , byte ttl , TimeSpan receiveTimeout )
        {
            if ( _socket != null || port < 0 || ! IPAddress.TryParse( ipAddress , out _ipAddress ) )
            {
                return false;
            }

            try
            {
                _socket = new Socket(_ipAddress.AddressFamily , SocketType.Dgram , ProtocolType.Udp );
                _groupEP = new IPEndPoint( IPAddress.Any , port );

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

                _socket.Bind(_groupEP);

                _buffer = new byte[DefaultReceiveBufferSize];

                return true;
            }
            catch ( Exception ex )
            {
                Close();

                OnError( ex );
            }

            return false;
        }

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
            _socket = null;
            _groupEP = null;
            _ipAddress = null;
            _buffer = null;
        }

        public void Dispose()
        {
            var socket = _socket;

            Close();
            socket?.Dispose();
        }

        public bool PollReceive( TimeSpan timeout )
        {
            try
            {
                return _socket?.Poll( (int) ( timeout.TotalMilliseconds * 1000 ) , SelectMode.SelectRead ) ?? false;
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return false;
        }

        public byte[] Receive()
        {
            if ( _socket == null || _buffer == null || _buffer.Length == 0 )
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

        private void OnError( Exception exception )
        {
            System.Diagnostics.Debug.WriteLine( exception );
        }
    }
}
