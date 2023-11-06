using System;
using System.Net.Sockets;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a socket tcp
    /// </summary>
    internal sealed class RTSPTcpSocket : IDisposable
    {
        private readonly Action<Exception>  _errorHandler = null;

        private Socket                      _socket       = null;



        /// <summary>
        /// Constructor
        /// </summary>
        public RTSPTcpSocket()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="errorHandler">the error handler</param>
        public RTSPTcpSocket( Action<Exception> errorHandler )
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
        /// Check if the read operation is supported
        /// </summary>
        public bool CanRead
        {
            get
            {
                return _socket != null;
            }
        }

        /// <summary>
        /// Check if the write operation is supported
        /// </summary>
        public bool CanWrite
        {
            get
            {
                return _socket != null;
            }
        }

        /// <summary>
        /// Check if some data are available
        /// </summary>
        public bool DataAvailable
        {
            get
            {
                try
                {
                    return _socket != null && _socket.Available != 0;
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
            if ( _socket == null )
            {
                return;
            }

            try
            {
                _socket.Shutdown( SocketShutdown.Both );
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
            return Send( RTSPDataConverter.ConvertToBytesUTF8( value ) );
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

            if ( _socket == null )
            {
                return false;
            }

            try
            {
                return _socket.Send(buffer, buffer.Length , SocketFlags.None ) > 0;
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

            if ( _socket == null )
            {
                return 0;
            }

            try
            {
                return _socket.Receive(buffer, offset , buffer.Length , SocketFlags.None );
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
        /// Set the linger state
        /// </summary>
        /// <param name="status">the status</param>
        /// <param name="timeout">the timeout</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool SetLingerState( bool status , TimeSpan timeout )
        {
            if ( _socket == null )
            {
                return false;
            }

            try
            {
                _socket.LingerState = new LingerOption( status , (int) timeout.TotalSeconds );

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
