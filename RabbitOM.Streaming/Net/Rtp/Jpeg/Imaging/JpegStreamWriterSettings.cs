using System;

namespace RabbitOM.Streaming.Net.Rtp.Jpeg.Imaging
{
    /// <summary>
    /// Represent the writer settings class
    /// </summary>
    public sealed class JpegStreamWriterSettings
    {
        /// <summary>
        /// Gets / Sets the unit value
        /// </summary>
        public byte Unit { get; set; }

        /// <summary>
        /// Gets / Sets the major version value
        /// </summary>
        public byte VersionMajor { get; set; } = 1;

        /// <summary>
        /// Gets / Sets the minor version value
        /// </summary>
        public byte VersionMinor { get; set; } = 1;

        /// <summary>
        /// Gets / Sets the X density value
        /// </summary>
        public UInt16 XDensity { get; set; } = 1;

        /// <summary>
        /// Gets / Sets the Y density value
        /// </summary>
        public UInt16 YDensity { get; set; } = 1;

        /// <summary>
        /// Gets / Sets the resolution fallback value
        /// </summary>
        public ResolutionInfo ResolutionFallback { get; set; }

        /// <summary>
        /// Gets / Sets a flag to always the resolution replace by the resolution fallback
        /// </summary>
        public bool ForceResolutionFallBack { get; set; }
    }
}
