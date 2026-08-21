using System;
using System.Net;
using System.Net.Sockets;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    internal sealed class RtspMulticastDataReceiver : IDataReceiver
    {
        private const int DefaultReceiveBufferSize = 8 * 1024 * 1024;

        private readonly byte[] _buffer;
        private readonly IPAddress _ipAddress;
        private readonly EndPoint _endPoint;
        private readonly byte _ttl;
        private readonly TimeSpan _timeout;
        private Socket _socket;

        public RtspMulticastDataReceiver( string ipAddress , int port, byte ttl , TimeSpan timeout )
        {
            _ipAddress = IPAddress.Parse( ipAddress );
            _endPoint = new IPEndPoint( IPAddress.Any , port );
            _ttl = ttl;
            _timeout = timeout;
            _buffer = new byte[ DefaultReceiveBufferSize ];
        }

        public bool IsOpened
        {
            get => _socket != null;
        }

        public void Open()
        {
            if ( _socket != null )
            {
                throw new InvalidOperationException( "the socket is already opened" );
            }

            try
            {
                _socket = new Socket(_ipAddress.AddressFamily , SocketType.Dgram , ProtocolType.Udp );
                _socket.ExclusiveAddressUse = false;
                _socket.SetSocketOption( SocketOptionLevel.Socket , SocketOptionName.ReuseAddress , true );
                _socket.SetSocketOption( SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, _ttl );
                _socket.Bind(_endPoint);
                _socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption( _ipAddress ));
                _socket.ReceiveBufferSize = DefaultReceiveBufferSize;
                _socket.ReceiveTimeout = (int) _timeout.TotalMilliseconds;
            }
            catch ( Exception )
            {
                Close();
                throw;
            }
        }

        public void Close()
        {
            Dispose();
        }

        public void Dispose()
        {
            var socket = _socket;

            _socket = null;

            if ( socket != null )
            {
                try
                {
                    socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.DropMembership, new MulticastOption(_ipAddress));
                }
                catch ( Exception ex )
                {
                    System.Diagnostics.Debug.WriteLine( ex );
                }
                finally
                {
                    socket.Dispose();
                }
            }
        }

        public byte[] Receive()
        {
            var socket = _socket;

            if ( socket == null )
            {
                return null;
            }

            var endPoint = _endPoint;

            var bytesReceived = socket.ReceiveFrom( _buffer , ref endPoint );

            if ( bytesReceived <= 0 )
            {
                return null;
            }

            var buffer = new byte[ bytesReceived ];

            Buffer.BlockCopy( _buffer , 0 , buffer , 0 , buffer.Length );

            return buffer;
        }
    }
}
