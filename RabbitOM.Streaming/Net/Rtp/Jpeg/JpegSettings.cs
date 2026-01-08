using System;

namespace RabbitOM.Streaming.Net.Rtp.Jpeg
{
    /// <summary>
    /// Represent the jpeg fallback settins
    /// </summary>
    public sealed class JpegSettings
    {
        /// <summary>
        /// Initialize an instance of the fallback settings
        /// </summary>
        /// <param name="resolutionFallBack">the resolution</param>
        public JpegSettings( in ResolutionInfo resolutionFallBack )
        {
            ResolutionFallBack = resolutionFallBack;
        }

        /// <summary>
        /// Gets the resolution
        /// </summary>
        public ResolutionInfo ResolutionFallBack { get; private set; }
    }
}
