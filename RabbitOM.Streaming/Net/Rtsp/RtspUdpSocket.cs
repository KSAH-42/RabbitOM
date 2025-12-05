using System;
using System.Net;
using System.Net.Sockets;

namespace RabbitOM.Streaming.Net.Rtsp
{
    /// <summary>
    /// Represent a socket tcp
    /// </summary>
    internal sealed class RtspUdpSocket : IDisposable
    {
        private const int DefaultReceiveBufferSize = ushort.MaxValue;




        private readonly Action<Exception>  _errorHandler = null;

        private Socket                      _socket       = null;

        private IPEndPoint                  _groupEP      = null;   
        
        private byte[]                      _buffer       = null;




        /// <summary>
        /// Constructor
        /// </summary>
        public RtspUdpSocket()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="errorHandler">the error handler</param>
        public RtspUdpSocket( Action<Exception> errorHandler )
        {
            _errorHandler = errorHandler;
        }





        /// <summary>
        /// Check if the socket is opening
        /// </summary>
        public bool IsOpening
        {
            get => _socket != null;
        }

        /// <summary>
        /// Check if the socket is opened and the internal stream has been acquired
        /// </summary>
        public bool IsOpened
        {
            get => _socket != null;
        }





        /// <summary>
        /// Open
        /// </summary>
        /// <param name="ipAddress">the end point</param>
        /// <returns>returns true for a success, otherwise false</returns>
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

        /// <summary>
        /// Close
        /// </summary>
        public void Close()
        {
            _socket?.Close();
            _socket?.Dispose();
            _groupEP = null;
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
        /// Send a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Send( string value )
        {
            return Send( RtspDataConverter.ConvertToBytesUTF8( value ) );
        }

        /// <summary>
        /// Send a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Send( byte value )
        {
            return Send( new byte[1] { value } );
        }

        /// <summary>
        /// Send a value
        /// </summary>
        /// <param name="buffer">the buffer</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Send( byte[] buffer )
        {
            return buffer != null && Send( buffer , 0 , buffer.Length );
        }

        /// <summary>
        /// Send a value
        /// </summary>
        /// <param name="buffer">the value</param>
        /// <param name="offset">the offset</param>
        /// <param name="count">the count</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Send( byte[] buffer , int offset , int count )
        {
            if ( buffer == null || buffer.Length <= 0 )
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

        /// <summary>
        /// Receive data
        /// </summary>
        /// <returns>returns a buffer, otherwise null.</returns>
        public byte[] Receive()
        {
            var bytesReceived = Receive( _buffer , 0 , _buffer.Length );

            if ( bytesReceived <=0 )
            {
                return null;
            }

            var buffer = new byte[ bytesReceived ];

            Buffer.BlockCopy( _buffer , 0 , buffer , 0 , buffer.Length );

            return buffer;
        }

        /// <summary>
        /// Receive data 
        /// </summary>
        /// <param name="buffer">the buffer</param>
        /// <param name="offset">the offset</param>
        /// <param name="count">the count</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public int Receive( byte[] buffer , int offset , int count )
        {
            if ( buffer == null || buffer.Length <= 0 )
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


        /// <summary>
        /// Get receive timeout
        /// </summary>
        /// <returns>returns a value</returns>
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

        /// <summary>
        /// Get send timeout
        /// </summary>
        /// <returns>returns a value</returns>
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

        /// <summary>
        /// Set receive timeout
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns true for a success, otherwise false</returns>
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

        /// <summary>
        /// Set send timeout
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns true for a success, otherwise false</returns>
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

        /// <summary>
        /// Check the status read of the socket
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <returns>returns a boolean value</returns>
        public bool PollReceive( TimeSpan timeout )
        {
            if ( _socket == null )
            {
                return false;
            }

            try
            {
                return _socket.Poll( 1000 * (int) timeout.TotalMilliseconds , SelectMode.SelectRead );
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return false;
        }

        /// <summary>
        /// Check the status send of the socket
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <returns>returns a boolean value</returns>
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

        /// <summary>
        /// Check the status error of the socket
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <returns>returns a boolean value</returns>
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





        /// <summary>
        /// Occurs when an error has been detected
        /// </summary>
        /// <param name="ex">the exception</param>
        private void OnError( Exception ex )
        {
            if ( ex == null )
            {
                return;
            }

            _errorHandler?.Invoke( ex );
        }
    }
}
