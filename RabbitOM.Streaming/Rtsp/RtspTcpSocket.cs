using System;
using System.Net.Sockets;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent a socket tcp
    /// </summary>
    internal sealed class RtspTcpSocket : IDisposable
    {
        private readonly Action<Exception>  _errorHandler = null;

        private  Socket                     _socket       = null;



        /// <summary>
        /// Constructor
        /// </summary>
        public RtspTcpSocket()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="errorHandler">the error handler</param>
        /// <exception cref="ArgumentNullException"/>
        public RtspTcpSocket( Action<Exception> errorHandler )
        {
            _errorHandler = errorHandler ?? throw new ArgumentNullException( nameof( errorHandler ) );
        }





        /// <summary>
        /// Check if the socket is opened 
        /// </summary>
        public bool IsOpened
        {
            get => _socket != null;
        }

        /// <summary>
        /// Check if the socket is connected
        /// </summary>
        public bool IsConnected
        {
            get
            {
                try
                {
                    return _socket?.Connected ?? false;
                }
                catch ( Exception ex )
                {
                    OnError( ex );
                }

                return false;
            }
        }






        /// <summary>
        /// Open
        /// </summary>
        /// <param name="ipAddress">the end point</param>
        /// <param name="port">the port</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Open( string ipAddress , int port )
        {
            if ( string.IsNullOrWhiteSpace( ipAddress ) || port < 0 )
            {
                return false;
            }

            if ( _socket != null )
            {
                return false;
            }

            try
            {
                _socket = new Socket( AddressFamily.InterNetwork , SocketType.Stream , ProtocolType.Tcp );
                
                _socket.Connect(ipAddress, port);
                _socket.ReceiveBufferSize = 500000;

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
        /// Close
        /// </summary>
        public void Close()
        {
            _socket?.Close();
            _socket?.Dispose();
            _socket = null;
        }

        /// <summary>
        /// Release internal resources
        /// </summary>
        public void Dispose()
        {
            Close();
        }

        /// <summary>
        /// Shutdown
        /// </summary>
        public void Shutdown()
        {
            try
            {
                _socket?.Shutdown( SocketShutdown.Both );
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }
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

            var socket = _socket;

            try
            {
                if ( socket != null )
                {
                    return socket.Send(buffer, offset , count , SocketFlags.None ) > 0;
                }
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

            try
            {
                return _socket?.Receive(buffer, offset , buffer.Length , SocketFlags.None ) ?? 0 ;
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
            try
            {
                return TimeSpan.FromMilliseconds( _socket?.ReceiveTimeout ?? 0 );
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
            try
            {
                return TimeSpan.FromMilliseconds( _socket?.SendTimeout ?? 0 );
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
            var socket = _socket;

            try
            {
                if ( socket != null )
                {
                    socket.ReceiveTimeout = (int) value.TotalMilliseconds;

                    return true;
                }
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
            var socket = _socket;

            try
            {
                if ( socket != null )
                {
                    socket.SendTimeout = (int) value.TotalMilliseconds;

                    return true;
                }
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return false;
        }

        /// <summary>
        /// Set the linger state
        /// </summary>
        /// <param name="status">the status</param>
        /// <param name="timeout">the timeout</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool SetLingerState( bool status , TimeSpan timeout )
        {
            var socket = _socket;

            try
            {
                if ( socket != null )
                {
                    socket.LingerState = new LingerOption( status , (int) timeout.TotalSeconds );

                    return true;
                }
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
            try
            {
                return _socket?.Poll( 1000 * (int) timeout.TotalMilliseconds , SelectMode.SelectRead ) ?? false;
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
            try
            {
                return _socket?.Poll( 1000 * (int) timeout.TotalMilliseconds , SelectMode.SelectWrite ) ?? false;
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
            try
            {
                return _socket?.Poll( 1000 * (int) timeout.TotalMilliseconds , SelectMode.SelectError ) ?? false;
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
