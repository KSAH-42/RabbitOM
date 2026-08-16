using System;
using System.Net;
using System.Net.Sockets;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    internal sealed class RtspUdpDataReceiver : IDataReceiver
    {
        private const int DefaultReceiveBufferSize = 8 * 1024 * 1024;

        private readonly byte[] _buffer = new byte[ DefaultReceiveBufferSize ];
        private readonly EndPoint _endPoint;
        private readonly TimeSpan _timeout;
        private Socket _socket;

        public RtspUdpDataReceiver( int port, TimeSpan timeout )
        {
            _endPoint = new IPEndPoint( IPAddress.Any , port );
            _timeout = timeout;
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
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                _socket.Bind(_endPoint);
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
            _socket?.Dispose();
            _socket = null;
        }

        public byte[] Receive()
        {
            var socket = _socket;

            if ( socket == null )
            {
                return null;
            }
            
            var endPoint = _endPoint;
            var bytesReceived = socket.ReceiveFrom( _buffer , 0 , _buffer.Length , SocketFlags.None , ref endPoint );

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
