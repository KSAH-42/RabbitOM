using System;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    /// <summary>
    /// Represent an event args
    /// </summary>
    public class RtspClientConnectedEventArgs : EventArgs
    {
        private readonly RtspTrackInfo _trackInfo = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="trackInfo">the track infos</param>
        /// <exception cref="ArgumentNullException"/>
        public RtspClientConnectedEventArgs( RtspTrackInfo trackInfo )
        {
            _trackInfo = trackInfo ?? throw new ArgumentNullException( nameof( trackInfo ) );
        }

        /// <summary>
        /// Gets the track infos
        /// </summary>
        public RtspTrackInfo TrackInfo
        {
            get => _trackInfo;
        }
    }
}
