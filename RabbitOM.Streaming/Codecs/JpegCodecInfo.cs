﻿using System;

namespace RabbitOM.Streaming.Codecs
{
    /// <summary>
    /// Represent a video codec
    /// </summary>
    public sealed class JpegCodecInfo : VideoCodecInfo
    {
        /// <summary>
        /// Gets the codec type
        /// </summary>
        public override CodecType Type
        {
            get => CodecType.Jpeg;
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
