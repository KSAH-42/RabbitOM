using System;

namespace RabbitOM.Streaming.Rtsp.Clients.Connections
{
    using RabbitOM.Threading;

    /// <summary>
    /// Represent the internal proxy event manager class
    /// </summary>
    internal sealed class RtspProxyEventManager
    {
        private readonly RtspEventConcurrentQueue _eventQueue;

        private readonly BackgroundWorker _thread;

        private readonly RtspProxy _proxy;






        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="proxy">the proxy</param>
        /// <exception cref="ArgumentNullException"/>
        public RtspProxyEventManager( RtspProxy proxy )
        {
            _proxy      = proxy ?? throw new ArgumentNullException( nameof( proxy ) );

            _eventQueue = new RtspEventConcurrentQueue();
            _thread     = new BackgroundWorker( "Rtsp - Proxy Event Manager");
        }






        /// <summary>
        /// Start
        /// </summary>
        public void Start()
        {
            _thread.Start( DoEvents );
        }

        /// <summary>
        /// Release internal resources
        /// </summary>
        public void Stop()
        {
            _thread.Stop();
            _eventQueue.Clear();
        }

        /// <summary>
        /// Dispatch an event
        /// </summary>
        /// <param name="e">the event args</param>
        public void Dispatch( EventArgs e )
        {
            _eventQueue.TryEnqueue( e );
        }






        /// <summary>
        /// Pump events
        /// </summary>
        private void DoEvents()
        {
            void pumpEvents()
            {
                while ( _eventQueue.TryDequeue( out EventArgs eventArgs ) )
                {
                    RtspProxy.RaiseEvent( _proxy , eventArgs );
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
