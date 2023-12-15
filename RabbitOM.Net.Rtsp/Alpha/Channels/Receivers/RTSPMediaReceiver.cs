using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    public abstract class RTSPMediaReceiver 
    {
        private readonly RTSPMediaService _service;

		public RTSPMediaReceiver( RTSPMediaService service )
		{
            _service = service ?? throw new ArgumentNullException( nameof( service ) );
		}

        protected RTSPMediaService Service
            => _service;
      
        public abstract bool IsStarted { get; }
        
        public abstract bool Start();
        public abstract void Stop();
        public abstract void Dispose();

        protected void OnStreamingStarted( RTSPStreamingStartedEventArgs e )
        {
            _service.Dispatcher.Dispatch( e );
        }

        protected void OnStreamingStopped( RTSPStreamingStoppedEventArgs e )
        {
            _service.Dispatcher.Dispatch( e );
        }

        protected void OnPacketReceived( RTSPPacketReceivedEventArgs e )
        {
            _service.Dispatcher.Dispatch( e );
        }
    }
}
