using System;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    /// <summary>
    /// Represent the writer configuration class
    /// </summary>
    public sealed class JpegStreamWriterConfiguration
    {
        /// <summary>
        /// Gets / Sets the major version value
        /// </summary>
        public byte VersionMajor { get; set; } = 1;

        /// <summary>
        /// Gets / Sets the minor version value
        /// </summary>
        public byte VersionMinor { get; set; } = 1;

        /// <summary>
        /// Gets / Sets the unit value
        /// </summary>
        public byte Unit { get; set; }

        /// <summary>
        /// Gets / Sets the X density value
        /// </summary>
        public UInt16 XDensity { get; set; } = 1;

        /// <summary>
        /// Gets / Sets the Y density value
        /// </summary>
        public UInt16 YDensity { get; set; } = 1;
    }
}
