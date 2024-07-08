using System;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    /// <summary>
    /// Represent an event args
    /// </summary>
    public class RTSPClientConnectedEventArgs : EventArgs
    {
        private readonly RTSPTrackInfo _trackInfo = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="trackInfo">the track infos</param>
        /// <exception cref="ArgumentNullException"/>
        public RTSPClientConnectedEventArgs( RTSPTrackInfo trackInfo )
        {
            _trackInfo = trackInfo ?? throw new ArgumentNullException( nameof( trackInfo ) );
        }

        /// <summary>
        /// Gets the track infos
        /// </summary>
        public RTSPTrackInfo TrackInfo
        {
            get => _trackInfo;
        }
    }
}
