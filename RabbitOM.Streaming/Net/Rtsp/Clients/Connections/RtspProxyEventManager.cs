using RabbitOM.Streaming.Collections;
using RabbitOM.Streaming.Threading;
using System;

namespace RabbitOM.Streaming.Net.Rtsp.Clients.Connections
{
    /// <summary>
    /// Represent the internal proxy event manager class
    /// </summary>
    internal sealed class RtspProxyEventManager
    {
        private readonly EventConcurrentQueue _eventQueue;

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

            _eventQueue = new EventConcurrentQueue();
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

            while ( EventConcurrentQueue.Wait( _eventQueue , _thread.ExitHandle ) )
            {
                pumpEvents();
            }

            pumpEvents();
        }
    }
}
