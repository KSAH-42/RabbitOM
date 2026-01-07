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
        /// Gets / Sets the fallback resolution where resolution can be determine if the rtp payload does not contains resolutions info. this case happen when the resolution is big and can not be store on the rtp payload / jpeg fragment.
        /// </summary>
        public JpegResolution ResolutionFallBack { get; set; }
    }
}
