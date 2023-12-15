﻿using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    public sealed class RTSPMulticastMediaReceiver : RTSPMediaReceiver
    {
        private readonly RTSPThread _thread;

        private readonly RTSPTrackInfo _trackInfo;

        public RTSPMulticastMediaReceiver( RTSPMediaService service , RTSPTrackInfo trackInfo )
            : base( service )
		{
            if ( trackInfo == null )
            {
                throw new ArgumentNullException( nameof( trackInfo ) );
            }

            _trackInfo = trackInfo;

            _thread = new RTSPThread( "RTSP - Multicast Receiver" );
		}

        public override bool IsStarted 
            => _thread.IsStarted;

        public override bool Start()
            => _thread.Start( () =>
            {
                OnStreamingStarted( new RTSPStreamingStartedEventArgs( _trackInfo ) );

                while ( _thread.CanContinue(100) )
                {
                }

                OnStreamingStopped( new RTSPStreamingStoppedEventArgs() );
            });

        public override void Stop()
            => _thread.Stop();

        public override void Dispose()
            => Stop();
    }
}
