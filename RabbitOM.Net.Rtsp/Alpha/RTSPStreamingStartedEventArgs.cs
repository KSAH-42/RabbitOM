﻿using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    /// <summary>
    /// Represent an event args
    /// </summary>
    public class RTSPStreamingStartedEventArgs : EventArgs
    {
        private readonly RTSPTrackInfo _trackInfo = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="trackInfo">the track infos</param>
        /// <exception cref="ArgumentNullException"/>
        public RTSPStreamingStartedEventArgs(RTSPTrackInfo trackInfo)
        {
            _trackInfo = trackInfo ?? throw new ArgumentNullException(nameof(trackInfo));
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
