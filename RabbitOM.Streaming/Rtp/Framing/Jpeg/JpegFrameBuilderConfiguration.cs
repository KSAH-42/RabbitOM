using System;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    public sealed class JpegFrameBuilderConfiguration
    {
        public const int DefaultPayloadMaxSize = 1400 * 2;
        public const int DefaultMaximumOfPackets = 1000;


        public int PayloadMaxSize { get; set; } = DefaultPayloadMaxSize;
        public int MaximumOfPackets { get; set; } = DefaultPayloadMaxSize;
    }
}
