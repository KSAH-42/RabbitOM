using System;

namespace RabbitOM.Net.Rtsp.Clients
{
    public class RtspClientConnectedEventArgs : EventArgs
    {
        public RtspClientConnectedEventArgs( RtspTrackInfo trackInfo )
        {
            TrackInfo = trackInfo ?? throw new ArgumentNullException( nameof( trackInfo ) );
        }

        public RtspTrackInfo TrackInfo
        {
            get;
        }
    }
}
