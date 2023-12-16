using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    public class RTSPStreamingSetupEventArgs : EventArgs
    {
        private readonly RTSPTrackInfo _trackInfo;

        public RTSPStreamingSetupEventArgs( RTSPTrackInfo trackInfo )
        {
            _trackInfo = trackInfo ?? throw new ArgumentNullException( nameof( trackInfo ) );
        }

        public RTSPTrackInfo TrackInfo
        {
            get => _trackInfo;
        }
    }
}
