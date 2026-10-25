using System;

namespace RabbitOM.Net.Rtp.Jpeg
{
    public sealed class JpegStreamWriterSettings
    {
        public byte Unit { get; set; }

        public byte VersionMajor { get; set; } = 1;

        public byte VersionMinor { get; set; } = 1;

        public UInt16 XDensity { get; set; } = 1;

        public UInt16 YDensity { get; set; } = 1;

        public ResolutionInfo? ResolutionFallback { get; set; }
    }
}
