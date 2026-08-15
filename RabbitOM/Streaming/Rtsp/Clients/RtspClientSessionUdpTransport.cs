using System;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    internal sealed class RtspClientSessionUdpTransport : RtspClientSessionTransport
    {
        private readonly RtspUdpSocket _socket;



        public RtspClientSessionUdpTransport( RtspClientSession session )
            : base ( session )
        {
            _socket = new RtspUdpSocket();

            Port = session.Configuration.RtpPort;
            Timeout = session.Configuration.ReceiveTimeout;
        }



        public int Port { get; }

        public TimeSpan Timeout { get; }



        protected override void Run()
        {
            if ( ! _socket.IsOpened )
            {
                IdleTimeout = TimeSpan.FromSeconds( 5 );

                if ( ! _socket.Open( Port ) )
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
