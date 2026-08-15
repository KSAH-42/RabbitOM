using System;
using System.Threading;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    using RabbitOM.Threading;

    internal abstract class RtspClientSessionTransport
    {
        private readonly BackgroundWorker _thread;
        private readonly RtspClientSession _session;
        private long _timeout;



        protected RtspClientSessionTransport( RtspClientSession session )
        {
            _session = session ?? throw new ArgumentNullException( nameof( session ) );

            _thread = new BackgroundWorker( "Rtsp - Transport session thread" );
        }



        protected TimeSpan IdleTimeout
        {
            get => TimeSpan.FromTicks( Volatile.Read( ref _timeout ) );
            set => Interlocked.Exchange( ref _timeout , value.Ticks );
        }

        protected RtspClientSession Session
        {
            get => _session;
        }

        public bool IsStarted
        {
            get => _thread.IsStarted;
        }




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
            _session.Dispatcher.DispatchEvent( new RtspPacketReceivedEventArgs( new RtspPacket( data ) ) );
        }
    }
}
