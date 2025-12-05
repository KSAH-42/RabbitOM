using System;

namespace RabbitOM.Streaming.Net.Rtsp
{
    /// <summary>
    /// Represent a track information class
    /// </summary>
    public sealed class RtspTrackInfo
    {
        /// <summary>
        /// Represent an empty track infos instance
        /// </summary>
        public static readonly RtspTrackInfo Empty = new RtspTrackInfo();







        private readonly byte _payloadType;

        private readonly string _encoder;

        private readonly uint _clockRate;

        private readonly string _controlUri;

        private readonly string _profileId;

        private readonly string _sps;

        private readonly string _pps;

        private readonly string _vps;

        
        



        
        
        /// <summary>
        /// Initialize a new instance of track information object
        /// </summary>
        private RtspTrackInfo()
        {
        }

        /// <summary>
        /// Initialize a new instance of track information object
        /// </summary>
        /// <param name="payloadType">the payload type</param>
        /// <param name="encoder">the encoder</param>
        /// <param name="clockRate">the clock rate</param>
        /// <param name="controlUri">the control uri</param>
        /// <param name="profileId">the profile identifier</param>
        /// <param name="sps">the sps</param>
        /// <param name="pps">the pps</param>
        /// <param name="vps">the vps</param>
        public RtspTrackInfo(byte payloadType, string encoder, uint clockRate, string controlUri, string profileId, string sps, string pps, string vps)
        {
            _payloadType = payloadType;
            _encoder     = encoder;
            _clockRate   = clockRate;
            _controlUri  = controlUri;
            _profileId   = profileId;
            _sps         = sps;
            _pps         = pps;
            _vps         = vps;
        }








        /// <summary>
        /// Gets the payload type
        /// </summary>
        public byte PayloadType 
        {
            get => _payloadType;
        }

        /// <summary>
        /// Gets the encoder
        /// </summary>
        public string Encoder
        {
            get => _encoder ?? string.Empty;
        }

        /// <summary>
        /// Gets the clock rate
        /// </summary>
        public uint ClockRate
        {
            get => _clockRate;
        }

        /// <summary>
        /// Gets the control uri
        /// </summary>
        public string ControlUri
        {
            get => _controlUri ?? string.Empty;
        }

        /// <summary>
        /// Gets the profile identifier
        /// </summary>
        public string ProfileId
        {
            get => _profileId ?? string.Empty;
        }

        /// <summary>
        /// Gets the SPS
        /// </summary>
        public string SPS
        {
            get => _sps ?? string.Empty;
        }

        /// <summary>
        /// Gets the pps
        /// </summary>
        public string PPS
        {
            get => _pps ?? string.Empty;
        }

        /// <summary>
        /// Gets the vps
        /// </summary>
        public string VPS
        {
            get => _vps ?? string.Empty;
        }







        /// <summary>
        /// Create an new video track info
        /// </summary>
        /// <param name="payloadType">the payload</param>
        /// <param name="encoder">the encoder</param>
        /// <param name="clockRate">the clock rate</param>
        /// <param name="controlUri">the control uri</param>
        /// <param name="profileId">the profile identifier</param>
        /// <param name="sps">the sps</param>
        /// <param name="pps">the ppz</param>
        /// <returns>returns an instance</returns>
        public static RtspTrackInfo NewVideoTrackInfo(byte payloadType, string encoder, uint clockRate, string controlUri, string profileId, string sps, string pps)
        {
            return new RtspTrackInfo(payloadType, encoder, clockRate, controlUri, profileId, sps, pps, string.Empty);
        }

        /// <summary>
        /// Create an new video track info
        /// </summary>
        /// <param name="payloadType">the payload</param>
        /// <param name="encoder">the encoder</param>
        /// <param name="clockRate">the clock rate</param>
        /// <param name="controlUri">the control uri</param>
        /// <param name="profileId">the profile identifier</param>
        /// <param name="sps">the sps</param>
        /// <param name="pps">the ppz</param>
        /// <param name="vps">the vps</param>
        /// <returns>returns an instance</returns>
        public static RtspTrackInfo NewVideoTrackInfo(byte payloadType, string encoder, uint clockRate, string controlUri, string profileId, string sps, string pps, string vps)
        {
            return new RtspTrackInfo(payloadType, encoder, clockRate, controlUri, profileId, sps, pps, vps);
        }

        /// <summary>
        /// Create an new audio track info
        /// </summary>
        /// <param name="payloadType">the payload</param>
        /// <param name="encoder">the encoder</param>
        /// <param name="clockRate">the clock rate</param>
        /// <param name="controlUri">the control uri</param>
        /// <param name="profileId">the profile identifier</param>
        /// <returns>returns an instance</returns>
        public static RtspTrackInfo NewAudioTrackInfo(byte payloadType, string encoder, uint clockRate, string controlUri, string profileId)
        {
            return new RtspTrackInfo(payloadType, encoder, clockRate, controlUri, profileId, string.Empty, string.Empty, string.Empty);
        }

        
        
        
        
        
        
        /// <summary>
        /// Validate the internal members
        /// </summary>
        /// <returns>returns true for a success, otherwise false.</returns>
        public bool TryValidate()
        {
            return _clockRate != 0

                && ! string.IsNullOrWhiteSpace( _encoder )
                && ! string.IsNullOrWhiteSpace( _controlUri )
                ;
        }
    }
}
