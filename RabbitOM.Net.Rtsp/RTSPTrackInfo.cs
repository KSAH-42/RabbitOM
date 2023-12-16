using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a track information class
    /// </summary>
    public sealed class RTSPTrackInfo
    {
        /// <summary>
        /// Represent an empty track infos instance
        /// </summary>
        public static readonly RTSPTrackInfo Empty = new RTSPTrackInfo();







        private readonly byte _payloadType;

        private readonly string _encoder = string.Empty;

        private readonly uint _clockRate;

        private readonly string _controlUri = string.Empty;

        private readonly string _profileId = string.Empty;

        private readonly string _sps = string.Empty;

        private readonly string _pps = string.Empty;

        private readonly string _vps = string.Empty;

        
        



        
        
        /// <summary>
        /// Initialize a new instance of track information object
        /// </summary>
        private RTSPTrackInfo()
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
        public RTSPTrackInfo(byte payloadType, string encoder, uint clockRate, string controlUri, string profileId, string sps, string pps, string vps)
        {
            _payloadType = payloadType;
            _encoder     = (encoder ?? string.Empty);
            _clockRate   = clockRate;
            _controlUri  = (controlUri ?? string.Empty);
            _profileId   = (profileId ?? string.Empty);
            _sps = (sps ?? string.Empty);
            _pps = (pps ?? string.Empty);
            _vps = (vps ?? string.Empty);
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
            get => _encoder;
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
        [Obsolete("For security reason, this property will be removed when the next client implementation will be ready")]
        public string ControlUri
        {
            get => _controlUri;
        }

        /// <summary>
        /// Gets the profile identifier
        /// </summary>
        public string ProfileId
        {
            get => _profileId;
        }

        /// <summary>
        /// Gets the SPS
        /// </summary>
        public string SPS
        {
            get => _sps;
        }

        /// <summary>
        /// Gets the pps
        /// </summary>
        public string PPS
        {
            get => _pps;
        }

        /// <summary>
        /// Gets the vps
        /// </summary>
        public string VPS
        {
            get => _vps;
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
        public static RTSPTrackInfo NewVideoTrackInfo(byte payloadType, string encoder, uint clockRate, string controlUri, string profileId, string sps, string pps)
        {
            return new RTSPTrackInfo(payloadType, encoder, clockRate, controlUri, profileId, sps, pps, string.Empty);
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
        public static RTSPTrackInfo NewVideoTrackInfo(byte payloadType, string encoder, uint clockRate, string controlUri, string profileId, string sps, string pps, string vps)
        {
            return new RTSPTrackInfo(payloadType, encoder, clockRate, controlUri, profileId, sps, pps, vps);
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
        public static RTSPTrackInfo NewAudioTrackInfo(byte payloadType, string encoder, uint clockRate, string controlUri, string profileId)
        {
            return new RTSPTrackInfo(payloadType, encoder, clockRate, controlUri, profileId, string.Empty, string.Empty, string.Empty);
        }

        
        
        
        
        
        
        /// <summary>
        /// Validate the internal members
        /// </summary>
        /// <returns>returns true for a success, otherwise false.</returns>
        public bool TryValidate()
        {
            if ( _clockRate == 0)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(_encoder))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(_controlUri))
            {
                return false;
            }

            return true;
        }
    }
}
