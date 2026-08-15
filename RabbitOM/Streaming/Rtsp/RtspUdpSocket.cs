using System;
using System.Net;
using System.Net.Sockets;

namespace RabbitOM.Streaming.Rtsp
{
    internal sealed class RtspUdpSocket : IDisposable
    {
        private const int DefaultReceiveBufferSize = 8 * 1024 * 1024;



        private Socket _socket;
        private IPEndPoint _groupEP;
        private byte[] _buffer;



        public bool IsOpening
        {
            get => _socket != null;
        }

        public bool IsOpened
        {
            get => _socket != null;
        }



        // TODO: remove the try catch and bool return value
        public bool Open(int port)
        {
            if (_socket != null)
            {
                return false;
            }

            try
            {
                _buffer = new byte[DefaultReceiveBufferSize];

                _groupEP = new IPEndPoint(IPAddress.Any, port);

                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                _socket.ReceiveBufferSize = DefaultReceiveBufferSize;
                _socket.Bind(_groupEP);

                return true;
            }
            catch (Exception ex)
            {
                OnError(ex);
            }

            Close();

            return false;
        }

        public void Close()
        {
            _socket?.Close();
            _socket = null;
            _groupEP = null;
            _buffer = null;
        }

        public void Dispose()
        {
            var socket = _socket;

            Close();
            socket?.Dispose();
        }

        public bool Send( string value )
        {
            return Send( RtspDataConverter.ConvertToBytesUTF8( value ) );
        }

        public bool Send( byte value )
        {
            return Send( new byte[1] { value } );
        }

        public bool Send( byte[] buffer )
        {
            return buffer != null && Send( buffer , 0 , buffer.Length );
        }

        public bool Send( byte[] buffer , int offset , int count )
        {
            if ( buffer == null || buffer.Length == 0 )
            {
                return false;
            }

            if ( count <= 0 || count > buffer.Length )
            {
                return false;
            }

            if ( _socket == null || _groupEP == null )
            {
                return false;
            }

            try
            {
                return _socket.SendTo( buffer, offset , buffer.Length , SocketFlags.None , _groupEP ) > 0;
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return false;
        }

        public byte[] Receive()
        {
            var bytesReceived = Receive( _buffer , 0 , _buffer.Length );

            if ( bytesReceived <= 0 )
            {
                return null;
            }

            var buffer = new byte[ bytesReceived ];

            Buffer.BlockCopy( _buffer , 0 , buffer , 0 , buffer.Length );

            return buffer;
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

            if ( _socket == null )
            {
                return 0;
            }

            var endpoint = _groupEP as EndPoint;

            if ( endpoint == null )
            {
                return 0;
            }

            try
            {
                return _socket.ReceiveFrom( buffer, offset , buffer.Length , SocketFlags.None , ref endpoint );
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return -1;
        }

        public TimeSpan GetReceiveTimeout()
        {
            if ( _socket == null )
            {
                return TimeSpan.Zero;
            }

            try
            {
                return TimeSpan.FromMilliseconds( _socket.ReceiveTimeout );
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return TimeSpan.Zero;
        }

        public TimeSpan GetSendTimeout()
        {
            if ( _socket == null )
            {
                return TimeSpan.Zero;
            }

            try
            {
                return TimeSpan.FromMilliseconds( _socket.SendTimeout );
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return TimeSpan.Zero;
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

        public bool SetSendTimeout( TimeSpan value )
        {
            if ( _socket == null )
            {
                return false;
            }

            try
            {
                _socket.SendTimeout = (int) value.TotalMilliseconds;

                return true;
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return false;
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

        public bool PollSend( TimeSpan timeout )
        {
            if ( _socket == null )
            {
                return false;
            }

            try
            {
                return _socket.Poll( 1000 * (int) timeout.TotalMilliseconds , SelectMode.SelectWrite );
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return false;
        }

        public bool PoolError( TimeSpan timeout )
        {
            if ( _socket == null )
            {
                return false;
            }

            try
            {
                return _socket.Poll( 1000 * (int) timeout.TotalMilliseconds , SelectMode.SelectError );
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return false;
        }




        private void OnError( Exception ex )
        {
            System.Diagnostics.Debug.WriteLine( ex );
        }
    }
}
