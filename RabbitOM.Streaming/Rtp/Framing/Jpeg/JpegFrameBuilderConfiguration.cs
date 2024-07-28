using System;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    public sealed class JpegFrameBuilderConfiguration
    {
        public const int MTU = 1500;
        public const int DefaultMaximumPayloadSize = MTU * 3;
        public const int DefaultNumberOfPacketsPerFrame = 1000;




        private readonly object _lock = new object();

        private int _maximumPayloadSize      = DefaultMaximumPayloadSize;
        private int _numberOfPacketsPerFrame = DefaultNumberOfPacketsPerFrame;




        public int MaximumPayloadSize
        {
            get
            {
                lock ( _lock )
                {
                    return _maximumPayloadSize;
                }
            }

            set
            {
                lock ( _lock )
                {
                    _maximumPayloadSize = value;
                }
            }
        }

        public int NumberOfPacketsPerFrame
        {
            get
            {
                lock ( _lock )
                {
                    return _numberOfPacketsPerFrame;
                }
            }

            set
            {
                lock ( _lock )
                {
                    _numberOfPacketsPerFrame = value;
                }
            }
        }
    }
}
