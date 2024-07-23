using System;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    public sealed class JpegStreamWriterConfiguration
    {
        public byte VersionMajor { get; set; } = 1;
        public byte VersionMinor { get; set; } = 1;
        public byte Unit { get; set; }
        public UInt16 XDensity { get; set; } = 1;
        public UInt16 YDensity { get; set; } = 1;
    }
}
