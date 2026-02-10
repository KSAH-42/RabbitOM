using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Receivers.Udp
{
    public class MulticastMediaReceiver : RtspMediaReceiver, IReceiverConfigurer<MulticastMediaReceiverConfiguration>
    {
        private readonly MulticastStreamingSession _session;

        public MulticastMediaReceiver()
        {
            _session = new MulticastStreamingSession( this );
        }

        public override bool IsConnected
        {
            get => _session.IsOpened;
        }

        public override bool IsStreamingStarted
        {
            get => _session.IsStreamingStarted;
        }

        public override bool IsReceivingData
        {
            get => _session.IsReceivingData;
        }

        public void Configure( MulticastMediaReceiverConfiguration configuration )
        {
            throw new NotImplementedException();
        }

        protected override void Dispose( bool disposing )
        {
            if ( disposing )
            {
                _session.Dispose();
            }

            base.Dispose( disposing );
        }

        protected override RtspStateMachine CreateStateMachine()
        {
            return new StreamingRtspStateMachine( _session );
        }
    }
}
