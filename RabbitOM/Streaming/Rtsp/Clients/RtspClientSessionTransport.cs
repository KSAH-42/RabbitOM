using System;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    using RabbitOM.Threading;

    internal abstract class RtspClientSessionTransport
    {
        private readonly BackgroundWorker _thread;



        protected RtspClientSessionTransport( RtspClientSession session )
        {
            Session = session ?? throw new ArgumentNullException( nameof( session ) );

            _thread = new BackgroundWorker( "Rtsp - Transport session thread" );
        }



        public bool IsStarted { get => _thread.IsStarted; }

        protected RtspClientSession Session { get; }

        protected TimeSpan IdleTimeout {  get; set; }



        public bool Start()
        {
            return _thread.Start( () =>
            {
                IdleTimeout = TimeSpan.Zero;

                while ( _thread.CanContinue( IdleTimeout ) )
                {
                    Run();
                }
            } );
        }

        public void Stop()
        {
            Shutdown();
            _thread.Stop();
        }

        protected abstract void Run();

        protected abstract void Shutdown();




        protected virtual void OnDataReceived( byte[] data )
        {
            Session.Dispatcher.DispatchEvent( new RtspPacketReceivedEventArgs( new RtspPacket( data ) ) );
        }
    }
}
