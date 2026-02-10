using System;

namespace RabbitOM.Streaming.Net.RtspV2.Receivers.Sessions.States
{
    public sealed class StreamingRtspStateMachine : RtspStateMachine
    {
        private readonly IStreamingSession _session;

        public StreamingRtspStateMachine( IStreamingSession session )
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
