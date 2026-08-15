using System;
using System.Net;
using System.Net.Sockets;

namespace RabbitOM.Streaming.Rtsp
{
    internal sealed class RtspUdpSocket : IDisposable
    {
        private const int DefaultReceiveBufferSize = 8 * 1024 * 1024;


        private readonly byte[] _buffer = new byte[ DefaultReceiveBufferSize ];
        private Socket _socket;
        private IPEndPoint _groupEP;


        public bool IsOpening
        {
            get => _socket != null;
        }

        public bool IsOpened
        {
            get => _socket != null;
        }


        public bool Open(int port) // TODO: remove the try catch and bool return value
        {
            if (_socket != null)
            {
                return false;
            }

            try
            {
                _groupEP = new IPEndPoint(IPAddress.Any, port);
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                _socket.Bind(_groupEP);
                _socket.ReceiveBufferSize = DefaultReceiveBufferSize;
                return true;
            }
            catch ( Exception ex )
            {
                Close();
                OnError(ex);
            }

            return false;
        }

        public void Close()
        {
            _socket?.Close();
            _socket = null;
            _groupEP = null;
        }

        public void Dispose()
        {
            _socket?.Dispose();
            _socket = null;
            _groupEP = null;
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
                var bytesReceived = _socket.ReceiveFrom( _buffer , 0 , _buffer.Length , SocketFlags.None , ref endpoint );

                if ( bytesReceived <= 0 )
                {
                    return null;
                }

                var buffer = new byte[ bytesReceived ];

                Buffer.BlockCopy( _buffer , 0 , buffer , 0 , buffer.Length );

                return buffer;
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return null;
        }


        private static void OnError( Exception ex )
        {
            System.Diagnostics.Debug.WriteLine( ex );
        }
    }
}
