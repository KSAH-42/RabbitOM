using System;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    public sealed class JpegFrameBuilderConfiguration
    {
        public const int MTU = 1500;
        
        public const int DefaultPayloadMaxSize = MTU * 2;
        public const int DefaultMaximumOfPackets = 1000;


        public int PayloadMaxSize { get; set; } = DefaultPayloadMaxSize;
        public int MaximumOfPackets { get; set; } = DefaultPayloadMaxSize;
    }
}
