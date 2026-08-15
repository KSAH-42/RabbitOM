using System;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    internal sealed class RtspClientSessionMulticastTransport : RtspClientSessionTransport
    {
        private readonly RtspMulticastSocket _socket;



        public RtspClientSessionMulticastTransport( RtspClientSession session )
            : base( session )
        {
            _socket = new RtspMulticastSocket();
            Address = session.Configuration.MulticastAddress;
            Port = session.Configuration.RtpPort;
            TTL = session.Configuration.TimeToLive;
            Timeout = session.Configuration.ReceiveTimeout;
        }



        public string Address  { get; }

        public int Port { get; }

        public byte TTL { get; }

        public TimeSpan Timeout { get; }



        protected override void Run()
        {
            if ( ! _socket.IsOpened )
            {
                IdleTimeout = TimeSpan.FromSeconds( 5 );

                if ( ! _socket.Open( Address , Port , TTL ) )
                {
                    return;
                }

                if ( ! _socket.SetReceiveTimeout( Timeout ) )
                {
                    _socket.Close();
                    return;
                }

                IdleTimeout = TimeSpan.Zero;
            }
            else
            {
                var buffer = _socket.Receive();

                if ( buffer?.Length > 0 )
                {
                    OnDataReceived( buffer );
                }
            }
        }

        protected override void Shutdown()
        {
            _socket.Close();
        }
    }
}
