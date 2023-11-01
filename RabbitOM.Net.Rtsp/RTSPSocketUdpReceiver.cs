using System;
using System.Net;
using System.Net.Sockets;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent an udp socket that only received 
    /// </summary>
    internal sealed class RTSPSocketUdpReceiver : IDisposable
    {
        /// <summary>
        /// Represent the socket buffer size
        /// </summary>
        public const int DefaultReceiveBufferSize = 5000000;


        private IPEndPoint  _groupEP  = null;

        private UdpClient   _socket   = null;





        /// <summary>
        /// Check if the socket has been opened
        /// </summary>
        public bool IsOpened
        {
            get => _socket != null;
        }





        /// <summary>
        /// Open the socket
        /// </summary>
        /// <param name="port">the port</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Open( int port )
        {
            if ( port < 0 )
            {
                return false;
            }

            if ( _socket != null )
            {
                return false;
            }

            try
            {
                var groupEP = new IPEndPoint( IPAddress.Any, port);

                var socket = new UdpClient( port );

                socket.Client.ReceiveBufferSize = DefaultReceiveBufferSize;

                _socket = socket;
                _groupEP = groupEP;

                return true;
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return false;
        }

        /// <summary>
        /// Open the socket
        /// </summary>
        /// <param name="port">the port</param>
        /// <param name="receiveTimeout">the receive timeout</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Open( int port , TimeSpan receiveTimeout )
        {
            if ( port < 0 || receiveTimeout == TimeSpan.Zero )
            {
                return false;
            }

            if ( _socket != null )
            {
                return false;
            }

            try
            {
                _groupEP = new IPEndPoint( IPAddress.Any, port);
                _socket  = new UdpClient( port );

                _socket.Client.ReceiveTimeout = (int) receiveTimeout.TotalMilliseconds;
                _socket.Client.ReceiveBufferSize = DefaultReceiveBufferSize;

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
        /// Close the socket
        /// </summary>
        public void Close()
        {
            try
            {
                if ( _socket != null )
                {
                    _socket.Close();
                    _socket.Dispose();
                }
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            _socket = null;
            _groupEP = null;
        }

        /// <summary>
        /// Release internal resources
        /// </summary>
        public void Dispose()
        {
            Close();
        }

        /// <summary>
        /// Gets the receive timeout
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
                return TimeSpan.FromMilliseconds( _socket.Client.ReceiveTimeout );
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return TimeSpan.Zero;
        }

        /// <summary>
        /// Set the receive timeout
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
                _socket.Client.ReceiveTimeout = (int) value.TotalMilliseconds;

                return true;
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return false;
        }

        /// <summary>
        /// Wait data during the specified timeout
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <returns>return true for a success, otherwise false</returns>
        public bool PollReceive( TimeSpan timeout )
        {
            if ( _socket == null )
            {
                return false;
            }

            try
            {
                return _socket.Client.Poll( (int) ( timeout.TotalMilliseconds * 1000 ) , SelectMode.SelectRead );
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return false;
        }

        /// <summary>
        /// Read bytes
        /// </summary>
        /// <returns>returns an array of bytes, otherwise null</returns>
        public byte[] Receive()
        {
            if ( _socket == null || _groupEP == null )
            {
                return null;
            }

            try
            {
                return _socket.Receive( ref _groupEP );
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return null;
        }





        /// <summary>
        /// Occurs when an error has been detected
        /// </summary>
        /// <param name="exception">the exception</param>
        private void OnError( Exception exception )
        {
            if ( exception == null )
            {
                return;
            }

            System.Diagnostics.Debug.WriteLine( exception );
        }
    }
}
