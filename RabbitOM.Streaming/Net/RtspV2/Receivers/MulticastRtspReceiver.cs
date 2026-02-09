using System;

namespace RabbitOM.Streaming.Net.RtspV2.Receivers
{
    using RabbitOM.Streaming.Net.RtspV2.Receivers.Sessions;
    using RabbitOM.Streaming.Net.RtspV2.Receivers.Sessions.States;

    public class MulticastRtspReceiver : RtspReceiver, IReceiverConfigurer<MulticastRtspReceiverConfiguration>
    {
        private readonly MulticastStreamingSession _session;

        public MulticastRtspReceiver()
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

        public void Configure( MulticastRtspReceiverConfiguration configuration )
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
