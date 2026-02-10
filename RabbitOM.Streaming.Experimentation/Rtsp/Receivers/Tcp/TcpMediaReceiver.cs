using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Receivers.Tcp
{
    public class TcpMediaReceiver : RtspMediaReceiver, IReceiverConfigurer<TcpMediaReceiverConfiguration>
    {
        private readonly TcpStreamingSession _session;

        public TcpMediaReceiver()
        {
            _session = new TcpStreamingSession( this );
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
