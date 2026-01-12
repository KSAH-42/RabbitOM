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
        /// <param name="forceResolutionFallBack">set true to force the replacement by the resolution fallback</param>
        public JpegFrameBuilderConfiguration( in ResolutionInfo resolutionFallBack , bool forceResolutionFallBack = false )
        {
            ResolutionFallBack = resolutionFallBack;
            ForceResolutionFallBack = forceResolutionFallBack;
        }

        /// <summary>
        /// Gets the resolution
        /// </summary>
        public ResolutionInfo ResolutionFallBack { get; }

        /// <summary>
        /// Gets the flag status to force the resolution fallback
        /// </summary>
        public bool ForceResolutionFallBack { get; }
    }
}
