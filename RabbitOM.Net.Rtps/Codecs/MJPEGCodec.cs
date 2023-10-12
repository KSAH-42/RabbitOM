using System;

namespace RabbitOM.Net.Rtps.Codecs
{
    /// <summary>
    /// Represent a video codec
    /// </summary>
    public sealed class MJPEGCodec : VideoCodec
    {
        /// <summary>
        /// Gets the codec type
        /// </summary>
        public override CodecType Type
        {
            get => CodecType.MJPEG;
        }

        /// <summary>
        /// Validate
        /// </summary>
        /// <returns>returns true</returns>
        public override bool Validate()
        {
            return true;
        }
    }
}
