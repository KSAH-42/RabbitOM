using System;

namespace RabbitOM.Streaming.Net.RtspV2.Receivers
{
    using RabbitOM.Streaming.Net.RtspV2.Receivers.Sessions;
    using RabbitOM.Streaming.Net.RtspV2.Receivers.Sessions.States;

    public class UdpRtspReceiver : RtspReceiver, IReceiverConfigurer<UdpRtspReceiverConfiguration>
    {
        private readonly UdpStreamingSession _session;

        public UdpRtspReceiver()
        {
            _session = new UdpStreamingSession( this );
        }

        public override bool IsConnected => _session.IsOpened;

        public override bool IsStreamingStarted => _session.IsStreamingStarted;

        public override bool IsReceivingData => _session.IsReceivingData;

        public void Configure( UdpRtspReceiverConfiguration configuration )
        {
            throw new NotImplementedException();
        }

        protected override RtspStateMachine CreateStateMachine()
        {
            return new StreamingRtspStateMachine( _session );
        }

        protected override void Dispose( bool disposing )
        {
            if ( disposing )
            {
                _session.Dispose();
            }

            base.Dispose( disposing );
        }
    }
}
