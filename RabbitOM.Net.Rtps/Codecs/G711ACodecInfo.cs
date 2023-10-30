using System;

namespace RabbitOM.Net.Rtsp.Codecs
{
    /// <summary>
    /// Represent an audio codec
    /// </summary>
    public sealed class G711ACodecInfo : G711CodecInfo
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public G711ACodecInfo()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="samplingRate">the sampling rate</param>
        /// <param name="channels">the channels</param>
        public G711ACodecInfo( int samplingRate , int channels )
            : base( samplingRate , channels )
        {
        }

        /// <summary>
        /// Gets the sampling rate
        /// </summary>
        public override CodecType Type
        {
            get => CodecType.G711A;
        }
    }
}
