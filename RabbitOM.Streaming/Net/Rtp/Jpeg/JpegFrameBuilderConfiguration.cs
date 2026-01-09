using System;

namespace RabbitOM.Streaming.Net.Rtp.Jpeg
{
    /// <summary>
    /// Represent the jpeg configuration
    /// </summary>
    public sealed class JpegFrameBuilderConfiguration
    {
        /// <summary>
        /// Initialize an instance of the fallback settings
        /// </summary>
        /// <param name="resolutionFallBack">the resolution</param>
        public JpegFrameBuilderConfiguration( in ResolutionInfo resolutionFallBack )
        {
            ResolutionFallBack = resolutionFallBack;
        }

        /// <summary>
        /// Gets the resolution
        /// </summary>
        public ResolutionInfo ResolutionFallBack { get; private set; }
    }
}
