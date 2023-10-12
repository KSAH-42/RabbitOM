using System;

namespace RabbitOM.Net.Rtps.Clients
{
    /// <summary>
    /// Represent the client transport layer used to receive packet from the network
    /// </summary>
    public sealed class RTSPClientSessionTransportUdpAsync : RTSPClientSessionTransport
    {
        private readonly RTSPSocketUdpReceiverAsync _socket    = null;

        private readonly int                   _port      = 0;

        private readonly TimeSpan              _timeout   = TimeSpan.Zero;
        
        
        
        
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="port">the port</param>
        /// <param name="timeout">the timeout</param>
        public RTSPClientSessionTransportUdpAsync( int port , TimeSpan timeout )
        {
            _socket  = new RTSPSocketUdpReceiverAsync( data => OnDataReceived( data ) );
            _port    = port;
            _timeout = timeout;
        }
        
        
        
        
        
        /// <summary>
        /// Gets the port
        /// </summary>
        public int Port
        {
            get => _port;
        }

        /// <summary>
        /// Gets the timeout
        /// </summary>
        public TimeSpan Timeout
        {
            get => _timeout;
        }
        
        
        
        /// TODO: Un comments the code on the run method only if a lock is used inside the socket class
        
        /// <summary>
        /// The thread function
        /// </summary>
        protected override void Run()
        {
            if ( ! _socket.IsOpened )
            {
                IdleTimeout = 5000;
                
                if ( ! _socket.Open( _port ) )
                {
                    return;
                }
                
                if ( ! _socket.SetReceiveTimeout( _timeout ) )
                {
                    _socket.Close();

                    return;
                }

                if ( ! _socket.StartReceive() )
				{
                    _socket.Close();

                    return;
				}
            }
            else
			{
    //            if ( _socket.IsReceiving )
				//{
    //                return;
				//}

    //            if (!_socket.StartReceive())
    //            {
    //                _socket.Close();

    //                return;
    //            }
            }
        }

        /// <summary>
        /// Release resource
        /// </summary>
        protected override void Shutdown()
        {
            _socket.Close();
        }
    }
}
