using System;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    using RabbitOM.Threading;

    internal sealed class RtspEventManager
    {
        private readonly RtspEventConcurrentQueue _eventQueue;
        private readonly BackgroundWorker _thread;
        private readonly RtspConnector _proxy;


        public RtspEventManager( RtspConnector proxy )
        {
            _proxy = proxy ?? throw new ArgumentNullException( nameof( proxy ) );

            _eventQueue = new RtspEventConcurrentQueue();

            _thread = new BackgroundWorker( "Rtsp - Proxy Event Manager");
        }


        public void Start()
        {
            _thread.Start( DoEvents );
        }

        public void Stop()
        {
            _thread.Stop();
            _eventQueue.Clear();
        }

        public void Dispatch( EventArgs e )
        {
            _eventQueue.TryEnqueue( e );
        }

        private void DoEvents()
        {
            void pumpEvents()
            {
                while ( _eventQueue.TryDequeue( out EventArgs eventArgs ) )
                {
                    RtspConnector.RaiseEvent( _proxy , eventArgs );
                }
            }

            while ( RtspEventConcurrentQueue.Wait( _eventQueue , _thread.ExitHandle ) )
            {
                pumpEvents();
            }

            pumpEvents();
        }
    }
}
