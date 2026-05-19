using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Receivers
{
    /*
        a rtsp state machine will used a session using a series of calls like options->describe->setup->play and ends by a teardown
        we used the kiss approach, because some rtsp client will called setup with a play, directly fort rtsp client used by smarttv, which are not standard.
        that's the main reasons, or to invoke custom methods
    */
            
    public sealed class StreamingRtspStateMachine : RtspStateMachine
    {
        private readonly IMediaStreamingSession _session;

        public StreamingRtspStateMachine( IMediaStreamingSession session )
        {
            _session = session ?? throw new ArgumentNullException( nameof( session ) );
        } 

        public override TimeSpan IdleTime 
        { 
            get => _session.IsOpened ? _session.PingInteral : _session.RetryInterval;
        }

        public override void Run()
        {
            if ( _session.IsOpened )
            {
                if ( ! _session.SendHeartBeat() )
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
                OnDispose();

                _session.Dispose();
            }

            base.Dispose( disposing );
        }

        private void OnDispose()
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
    }
}
