using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    public sealed class RTSPEventDispatcher : IRTSPEventDispatcher , IDisposable
    {
        private readonly RTSPEventQueue _queue;

        private readonly RTSPThread _thread;

        private readonly Action<EventArgs> _handler;





        public RTSPEventDispatcher( Action<EventArgs> handler )
        {
            _handler = handler ?? throw new ArgumentNullException( nameof( handler ) );

            _queue = new RTSPEventQueue();
            _thread = new RTSPThread("RTSP - Dispatcher thread");
        }





        public void Start()
        {
            _thread.Start( DoEvents ); 
        }

        public void Stop()
        {
            _thread.Stop();
            _queue.Clear();
        }

        public void Dispose()
        {
            Stop();
        }

        public void Dispatch( EventArgs e )
        {
            _queue.Enqueue( e );
        }

        public void RaiseEvent( EventArgs e )
        {
            _handler.Invoke( e );
        }





        private void DoEvents()
        {
            var pumpEvents = new Action( () =>
            {
                while ( _queue.TryDequeue( out EventArgs e ) )
                {
                    _handler.Invoke( e );
                }
            });

            while ( RTSPEventQueue.Wait( _queue , _thread.ExitHandle ) )
            {
                pumpEvents();
            }

            pumpEvents();
        }
    }
}
