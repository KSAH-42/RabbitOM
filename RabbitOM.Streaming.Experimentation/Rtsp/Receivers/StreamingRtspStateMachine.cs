using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Receivers
{
    public sealed class StreamingRtspStateMachine : RtspStateMachine
    {
        private readonly IMediaStreamingSession _session;

        public StreamingRtspStateMachine( IMediaStreamingSession session )
        {
            _session = session ?? throw new ArgumentNullException( nameof( session ) );
        } 

        public override TimeSpan IdleTime 
        { 
            get => _session.IdleTimeout; 
        }

        public override void Run()
        {
            if ( _session.IsOpened )
            {
                if ( ! _session.CheckStatus() )
                {
                    _session.Close();
                }
            }
            else
            {
                if ( _session.Open() )
                {
                    if ( ! _session.StartStreaming() )
                    {
                        _session.Close();
                    }
                }
            }
        }
        
        protected override void Dispose( bool disposing )
        {
            if ( disposing )
            {
                if ( _session.IsStreamingStarted )
                {
                    _session.StopStreaming();
                }

                if ( _session.IsOpened )
                {
                    _session.Close();
                }
            }

            base.Dispose( disposing );
        }
    }
}
