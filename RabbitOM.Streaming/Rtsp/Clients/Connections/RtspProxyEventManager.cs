using System;

namespace RabbitOM.Streaming.Rtsp.Clients.Connections
{
    /// <summary>
    /// Represent the internal proxy event manager class
    /// </summary>
    internal sealed class RtspProxyEventManager
    {
        private readonly RtspEventQueue _eventQueue;

        private readonly RtspThread _thread;

        private readonly RtspProxy _proxy;






        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="proxy">the proxy</param>
        /// <exception cref="ArgumentNullException"/>
        public RtspProxyEventManager( RtspProxy proxy )
        {
            _proxy = proxy ?? throw new ArgumentNullException( nameof( proxy ) );

            _eventQueue = new RtspEventQueue();
            _thread= new RtspThread( "Rtsp - Proxy Event Manager");
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
            if ( e == null )
            {
                return;
            }

            _eventQueue.Enqueue( e );
        }






        /// <summary>
        /// Pump events
        /// </summary>
        private void DoEvents()
        {
            void pumpEvents()
            {
                while ( _eventQueue.Any() )
                {
                    if ( _eventQueue.TryDequeue( out EventArgs eventArgs ) )
                    {
                        _proxy.RaiseEvent( eventArgs );
                    }
                }
            }

            while ( RtspEventQueue.Wait( _eventQueue , _thread.ExitHandle ) )
            {
                pumpEvents();
            }

            pumpEvents();
        }
    }
}
