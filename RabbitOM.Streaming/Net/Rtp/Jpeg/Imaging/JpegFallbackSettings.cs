using System;

namespace RabbitOM.Streaming.Net.Rtp.Jpeg.Imaging
{
    /// <summary>
    /// Represent the jpeg fallback settins
    /// </summary>
    public sealed class JpegFallbackSettings
    {
        /// <summary>
        /// Initialize an instance of the fallback settings
        /// </summary>
        /// <param name="resolution">the resolution</param>
        public JpegFallbackSettings( in JpegResolution resolution )
        {
            Resolution = resolution;
        }

        /// <summary>
        /// Gets the resolution
        /// </summary>
        public JpegResolution Resolution { get; private set; }
    }
}
