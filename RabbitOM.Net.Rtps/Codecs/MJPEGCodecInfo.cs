using System;

namespace RabbitOM.Net.Rtsp.Codecs
{
    /// <summary>
    /// Represent a video codec
    /// </summary>
    public sealed class MJPEGCodecInfo : VideoCodecInfo
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
        public override bool TryValidate()
        {
            return true;
        }
    }
}
