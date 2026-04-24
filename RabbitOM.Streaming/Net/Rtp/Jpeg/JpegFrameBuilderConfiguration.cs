using System;

namespace RabbitOM.Streaming.Net.Rtp.Jpeg
{
    /// <summary>
    /// Represent the jpeg configuration
    /// </summary>
    public class JpegFrameBuilderConfiguration
    {
        /// <summary>
        /// The defaut configuration value
        /// </summary>
        public static readonly JpegFrameBuilderConfiguration Empty = new JpegFrameBuilderConfiguration();





        /// <summary>
        /// Initialize an instance of the fallback settings
        /// </summary>
        private JpegFrameBuilderConfiguration()
        {
        }

        /// <summary>
        /// Initialize an instance of the fallback settings
        /// </summary>
        /// <param name="resolutionFallBack">the resolution</param>
        public JpegFrameBuilderConfiguration( ResolutionInfo resolutionFallBack )
        {
            ResolutionFallBack = resolutionFallBack;
        }

        /// <summary>
        /// Gets the resolution
        /// </summary>
        public ResolutionInfo? ResolutionFallBack { get; }




        /// <summary>
        /// Create con
        /// </summary>
        /// <param name="resolutionFallBack"></param>
        /// <returns></returns>
        public static JpegFrameBuilderConfiguration CreateConfigurationFrom( ResolutionInfo? resolutionFallBack )
        {
            return resolutionFallBack.HasValue ? new JpegFrameBuilderConfiguration( resolutionFallBack.Value ) : JpegFrameBuilderConfiguration.Empty;
        }
    }
}
