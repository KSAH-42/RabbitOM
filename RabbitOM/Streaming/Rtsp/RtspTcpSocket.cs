using System;
using System.Net.Sockets;

namespace RabbitOM.Streaming.Rtsp
{
    internal sealed class RtspTcpSocket : IDisposable
    {
        private const int DefaultReceiveBufferSize = 8 * 1024 * 1024;


        private Socket _socket;


        public bool IsOpened
        {
            get => _socket != null;
        }

        public bool IsConnected
        {
            get => _socket?.Connected ?? false;
        }

        public TimeSpan ReceiveTimeout
        {
            get
            {
                var socket = _socket;

                if ( socket != null )
                {
                    return TimeSpan.FromMilliseconds( socket.ReceiveTimeout );
                }

                return TimeSpan.Zero;
            }

            set
            {
                var socket = _socket;

                if ( socket != null )
                {
                    socket.ReceiveTimeout = (int) value.TotalMilliseconds;
                }
            }
        }

        public TimeSpan SendTimeout
        {
            get
            {
                var socket = _socket;

                if ( socket != null )
                {
                    return TimeSpan.FromMilliseconds( socket.SendTimeout );
                }

                return TimeSpan.Zero;
            }

            set
            {
                var socket = _socket;

                if ( socket != null )
                {
                    socket.SendTimeout = (int) value.TotalMilliseconds;
                }
            }
        }







        public void Connect( string ipAddress , int port )
        {
            if ( string.IsNullOrWhiteSpace( ipAddress ) )
            {
                throw new ArgumentNullException( nameof( ipAddress ) );
            }

            if ( _socket != null )
            {
                throw new InvalidOperationException( "the socket is already created" );
            }

            try
            {
                _socket = new Socket( AddressFamily.InterNetwork , SocketType.Stream , ProtocolType.Tcp );
                _socket.Connect(ipAddress, port);

                _socket.ReceiveBufferSize = DefaultReceiveBufferSize;
            }
            catch ( Exception )
            {
                _socket?.Dispose();
                throw;
            }
        }

        public void Close()
        {
            _socket?.Close();
            _socket = null;
        }

        public void Dispose()
        {
            _socket?.Dispose();
            _socket = null;
        }

        public void EnableLingerState( TimeSpan timeout )
        {
            var socket = _socket;

            if ( socket != null )
            {
                socket.LingerState = new LingerOption( true , (int) timeout.TotalSeconds );
            }
        }

        public void DisableLingerState( TimeSpan timeout )
        {
            var socket = _socket;

            if ( socket != null )
            {
                socket.LingerState = new LingerOption( false , 0 );
            }
        }

        public int Send( byte[] buffer , int offset , int count )
        {
            if ( buffer == null || buffer.Length == 0 )
            {
                return 0;
            }

            if ( count <= 0 || count > buffer.Length )
            {
                return 0;
            }

            return _socket?.Send( buffer , offset , count , SocketFlags.None ) ?? 0;
        }

        public int Receive( byte[] buffer , int offset , int count )
        {
            if ( buffer == null || buffer.Length == 0 )
            {
                return 0;
            }

            if ( count <= 0 || count > buffer.Length )
            {
                return 0;
            }

            var socket = _socket;

            if ( socket == null )
            {
                return 0;
            }

            return socket.Receive( buffer , offset , count , SocketFlags.None );
        }
    }
}
