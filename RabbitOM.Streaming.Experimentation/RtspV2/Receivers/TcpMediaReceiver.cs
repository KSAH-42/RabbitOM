using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Receivers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Receivers.Sessions;
    using RabbitOM.Streaming.Experimentation.Rtsp.Receivers.Sessions.States;

    public class TcpMediaReceiver : RtspMediaReceiver, IReceiverConfigurer<TcpMediaReceiverConfiguration>
    {
        private readonly TcpRtspStreamingSession _session;

        public TcpMediaReceiver()
        {
            _session = new TcpRtspStreamingSession( this );
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

        public void Configure( TcpMediaReceiverConfiguration configuration )
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
