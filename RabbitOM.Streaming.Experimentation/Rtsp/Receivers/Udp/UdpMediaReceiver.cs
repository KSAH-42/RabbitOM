using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Receivers.Udp
{
    public class UdpMediaReceiver : RtspMediaReceiver, IReceiverConfigurer<UdpMediaReceiverConfiguration>
    {
        private readonly UdpStreamingSession _session;

        public UdpMediaReceiver()
        {
            _session = new UdpStreamingSession( this );
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

        public void Configure( UdpMediaReceiverConfiguration configuration )
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
