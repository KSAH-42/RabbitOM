using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    internal abstract class RTSPMediaReceiver : IDisposable
    {
        private readonly RTSPMediaService _service;

        public RTSPMediaReceiver( RTSPMediaService service )
        {
            _service = service ?? throw new ArgumentNullException( nameof( service ) );
        }

        ~RTSPMediaReceiver()
        {
            Dispose( false );
        }

        protected RTSPMediaService Service
            => _service;
      
        public abstract bool IsStarted { get; }
        
        public abstract bool Start();
        public abstract void Stop();
        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize(this);
        }

        protected abstract void Dispose(bool disposing);

        protected void OnStreamingStarted( RTSPStreamingStartedEventArgs e )
        {
            _service.UpdateStreamingRunningStatus( true );
            _service.UpdateReceivingStatus( false );

            _service.Dispatcher.Dispatch( e );
        }

        protected void OnStreamingStopped( RTSPStreamingStoppedEventArgs e )
        {
            _service.UpdateReceivingStatus( false );
            _service.UpdateStreamingRunningStatus( false );

            _service.Dispatcher.Dispatch( e );
        }

        protected void OnPacketReceived( RTSPPacketReceivedEventArgs e )
        {
            _service.UpdateReceivingStatus( true );

            _service.Dispatcher.Dispatch( e );
        }

        protected void OnError( RTSPErrorEventArgs e )
        {
            _service.Dispatcher.Dispatch( e );
        }
    }
}
