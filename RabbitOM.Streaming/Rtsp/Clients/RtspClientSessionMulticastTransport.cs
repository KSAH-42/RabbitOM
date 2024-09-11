using System;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    /// <summary>
    /// Represent the client transport layer used to receive packet from the network
    /// </summary>
    internal sealed class RtspClientSessionMulticastTransport : RtspClientSessionTransport
    {
        private readonly RtspMulticastSocket _socket    = null;
        
        private readonly string                      _address   = string.Empty;

        private readonly int                         _port      = 0;

        private readonly byte                        _ttl       = 0;

        private readonly TimeSpan                    _timeout   = TimeSpan.Zero;
        






        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="addresss">the address</param>
        /// <param name="port">the port</param>
        /// <param name="ttl">the ttl</param>
        /// <param name="timeout">the timeout</param>
        public RtspClientSessionMulticastTransport( string addresss , int port , byte ttl , TimeSpan timeout )
        {
            _socket  = new RtspMulticastSocket();
            _address = addresss ?? string.Empty;
            _port    = port;
            _ttl     = ttl;
            _timeout = timeout;
        }
        






        /// <summary>
        /// Gets the multicast ip address
        /// </summary>
        public string Address
        {
            get => _address;
        }

        /// <summary>
        /// Gets the port
        /// </summary>
        public int Port
        {
            get => _port;
        }

        /// <summary>
        /// Gets the ttl value
        /// </summary>
        public byte TTL
        {
            get => _ttl;
        }

        /// <summary>
        /// Gets the timeout
        /// </summary>
        public TimeSpan Timeout
        {
            get => _timeout;
        }
        






        /// <summary>
        /// Thread function
        /// </summary>
        protected override void Run()
        {
            if ( ! _socket.IsOpened )
            {
                IdleTimeout = TimeSpan.FromSeconds( 5 );

                if ( ! _socket.Open( _address , _port , _ttl , _timeout ) )
                {
                    return;
                }

                IdleTimeout = TimeSpan.Zero;
            }
            else
            {
                if ( ! _socket.PollReceive( _timeout ) )
                {
                    return;
                }

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
