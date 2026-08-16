using System;
using System.Net;
using System.Net.Sockets;

namespace RabbitOM.Streaming.Rtsp
{
    internal sealed class RtspMulticastSocket : IDisposable
    {
        private const int DefaultReceiveBufferSize = 8 * 1024 * 1024;

        private readonly byte[] _buffer = new byte[ DefaultReceiveBufferSize ];
        private IPAddress _ipAddress;
        private IPEndPoint _groupEP;
        private Socket _socket;

        public bool IsOpened
        {
            get => _socket != null;
        }

        public bool Open( string ipAddress , int port , byte ttl ) // TODO: remove the try catch and bool return value
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
                _socket.SetSocketOption( SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, ttl);
                _socket.Bind(_groupEP);
                _socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption( _ipAddress ));
                _socket.ReceiveBufferSize = DefaultReceiveBufferSize;
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
        }

        public void Dispose()
        {
            var socket = _socket;
            Close();
            socket?.Dispose();
        }

        public bool SetReceiveTimeout( TimeSpan value )
        {
            if ( _socket == null )
            {
                return false;
            }

            try
            {
                _socket.ReceiveTimeout = (int) value.TotalMilliseconds;
                return true;
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return false;
        }

        public byte[] Receive()
        {
            var endpoint = _groupEP as EndPoint;

            if ( endpoint == null || _socket == null )
            {
                return null;
            }

            try
            {
                var bytesReceived = _socket.ReceiveFrom(_buffer , ref endpoint);

                if ( bytesReceived > 0 )
                {
                    var buffer = new byte[ bytesReceived ];

                    Buffer.BlockCopy( _buffer , 0 , buffer , 0 , buffer.Length );

                    return buffer;
                }
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return null;
        }


        private static void OnError( Exception exception )
        {
            System.Diagnostics.Debug.WriteLine( exception );
        }
    }
}
