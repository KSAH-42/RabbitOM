﻿using System;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    /// <summary>
    /// Represent the client transport layer used to receive packet from the network
    /// </summary>
    internal sealed class RtspClientSessionUdpTransport : RtspClientSessionTransport
    {
        private readonly RtspUdpSocket         _socket    = null;

        private readonly int                   _port      = 0;

        private readonly TimeSpan              _timeout   = TimeSpan.Zero;
        
        
        
        
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="port">the port</param>
        /// <param name="timeout">the timeout</param>
        public RtspClientSessionUdpTransport( int port , TimeSpan timeout )
        {
            _socket  = new RtspUdpSocket();
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
        
        
       
        
        /// <summary>
        /// The thread function
        /// </summary>
        protected override void Run()
        {
            if ( ! _socket.IsOpened )
            {
                IdleTimeout = TimeSpan.FromSeconds( 5 );

                if ( ! _socket.Open( _port ) )
                {
                    return;
                }
                
                if ( ! _socket.SetReceiveTimeout( _timeout ) )
                {
                    _socket.Close();

                    return;
                }
                                    
                IdleTimeout = TimeSpan.Zero;
            }
            else
            {
                var buffer = _socket.Receive();
                        
                if ( null == buffer || buffer.Length <= 0 )
                {
                    return;
                }

                OnDataReceived( buffer );
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
