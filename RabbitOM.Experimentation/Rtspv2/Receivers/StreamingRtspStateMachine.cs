using System;

namespace RabbitOM.Streaming.RtspV2.Receivers
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

        // a real state machine should use state
        // combine with a kind of switch cases and invoke method with status updating
        // and recall again and again
        // from the rfc any rtsp clients must use a state machine
        // here we use a kind of state of machine 
        // session state are expose as boolean not enum 
        // and the data type has a strongly a impact of how an algorithm 
        // should be implemented
        // the idea is you right the state machine has you want, instead
        // of dealing the enum states, you must deal with a contract

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
