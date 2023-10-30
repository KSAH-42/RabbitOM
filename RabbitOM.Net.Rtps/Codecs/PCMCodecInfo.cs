using System;

namespace RabbitOM.Net.Rtsp.Codecs
{
    /// <summary>
    /// Represent a video codec
    /// </summary>
    public sealed class PCMCodecInfo : AudioCodecInfo
    {
        private readonly int _samplingRate = 0;

        private readonly int _resolution   = 0;

        private readonly int _channels     = 0;



        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="samplingRate">the sampling rate</param>
        /// <param name="resolution">the resolution</param>
        /// <param name="channels">the channels</param>
        public PCMCodecInfo( int samplingRate , int resolution , int channels )
        {
            _samplingRate = samplingRate;
            _resolution   = resolution;
            _channels     = channels;
        }



        /// <summary>
        /// Gets codec type
        /// </summary>
        public override CodecType Type
        {
            get => CodecType.PCM;
        }

        /// <summary>
        /// Gets the sampling rate
        /// </summary>
        public int SamplingRate
        {
            get => _samplingRate;
        }

        /// <summary>
        /// Gets the resolution
        /// </summary>
        public int Resolution
        {
            get => _resolution;
        }

        /// <summary>
        /// Gets the channels
        /// </summary>
        public int Channels
        {
            get => _channels;
        }
        


        /// <summary>
        /// Validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool TryValidate()
        {
            if ( _samplingRate <= 0 )
            {
                return false;
            }

            if ( _resolution <= 0 )
            {
                return false;
            }

            if ( _channels < 0 )
            {
                return false;
            }

            return true;
        }
    }
}
