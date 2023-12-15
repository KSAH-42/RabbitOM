using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    public abstract class RTSPMediaTransport 
    {
        private readonly RTSPMediaChannelService _service;

		public RTSPMediaTransport( RTSPMediaChannelService service )
		{
            _service = service ?? throw new ArgumentNullException( nameof( service ) );
		}

        protected RTSPMediaChannelService Service
            => _service;
      
        public abstract bool IsStarted { get; }
        public abstract bool IsReceivingPacket { get; }

        public abstract bool Start();
        public abstract void Stop();
        public abstract void Dispose();
    }
}
