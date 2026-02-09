using System;

namespace RabbitOM.Streaming.Net.Next.Rtsp.Receivers
{
    using RabbitOM.Streaming.Net.Next.Rtsp.Receivers.Sessions;
    using RabbitOM.Streaming.Net.Next.Rtsp.Receivers.Sessions.States;

    public class TcpRtspReceiver : RtspReceiver, IRtspReceiverConfigurer<TcpRtspReceiverConfiguration>
    {
        private readonly TcpRtspStreamingSession _session;

        public TcpRtspReceiver()
        {
            _session = new TcpRtspStreamingSession( this );
        }

        public override bool IsConnected => _session.IsOpened;

        public override bool IsStreamingStarted => _session.IsStreamingStarted;

        public override bool IsReceivingData => _session.IsReceivingData;

        public void Configure( TcpRtspReceiverConfiguration configuration )
        {
            throw new NotImplementedException();
        }

        protected override RtspStateMachine CreateStateMachine()
        {
            return new StreamingRtspStateMachine(  _session );
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
