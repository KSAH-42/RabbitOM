using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    public abstract class RTSPMediaTransport : IDisposable
    {
        protected readonly IRTSPMediaChannel _channel;



		protected RTSPMediaTransport( IRTSPMediaChannel channel )
		{
            _channel = channel ?? throw new ArgumentNullException( nameof( channel ) );
        }



        public abstract object SyncRoot { get; }
        public abstract bool IsStarted { get; }
        public abstract bool IsPlaying { get; }


        public abstract bool Options();
        public abstract bool Describe();
        public abstract bool Setup();
        public abstract bool Play();
        public abstract void TearDown();
        public abstract void Dispose();



        protected virtual void OnStreamingStarted(RTSPStreamingStartedEventArgs e)
        {
            _channel.Dispatcher.Dispatch(e);
        }

        protected virtual void OnStreamingStopped(RTSPStreamingStoppedEventArgs e)
        {
            _channel.Dispatcher.Dispatch(e);
        }

        protected virtual void OnStreamingStatusChanged(RTSPStreamingStatusChangedEventArgs e)
        {
            _channel.Dispatcher.Dispatch(e);
        }

        protected virtual void OnPacketReceived(RTSPPacketReceivedEventArgs e)
        {
            _channel.Dispatcher.RaiseEvent(e);
        }

        protected virtual void OnError(RTSPErrorEventArgs e)
        {
            _channel.Dispatcher.Dispatch(e);
        }
    }
}
