using System;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    public sealed class JpegFrameBuilderConfiguration
    {
        public const int MTU = 1500;
        public const int DefaultMaximumPayloadSize = MTU * 3;
        public const int DefaultNumberOfPacketsPerFrame = 1000;


        public int MaximumPayloadSize { get; set; } = DefaultMaximumPayloadSize;
        public int NumberOfPacketsPerFrame { get; set; } = DefaultNumberOfPacketsPerFrame;
    }
}
