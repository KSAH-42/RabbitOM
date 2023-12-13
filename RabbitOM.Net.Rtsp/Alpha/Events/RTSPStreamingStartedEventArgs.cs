using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    public class RTSPStreamingStartedEventArgs : EventArgs
    {
        private readonly RTSPTrackInfo _trackInfo;

        public RTSPStreamingStartedEventArgs( RTSPTrackInfo trackInfo )
        {
            _trackInfo = trackInfo ?? throw new ArgumentNullException( nameof( trackInfo ) );
        }

        public RTSPTrackInfo TrackInfo
        {
            get => _trackInfo;
        }
    }
}
