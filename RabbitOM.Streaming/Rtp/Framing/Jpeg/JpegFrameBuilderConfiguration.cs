using System;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    /// <summary>
    /// Represent the builder configuration class
    /// </summary>
    public sealed class JpegFrameBuilderConfiguration
    {
        /// <summary>
        /// The default maximum payload size
        /// </summary>
        public const int DefaultMaximumPayloadSize = 1500 * 3;

        /// <summary>
        /// The default number of packets per frame
        /// </summary>
        public const int DefaultNumberOfPacketsPerFrame = 1000;



        private readonly object _lock = new object();

        private int _maximumPayloadSize      = DefaultMaximumPayloadSize;
        private int _numberOfPacketsPerFrame = DefaultNumberOfPacketsPerFrame;



        /// <summary>
        /// Gets / Sets the maximul payload size
        /// </summary>
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

        /// <summary>
        /// Gets / Sets the numbers of packets allowed per frame
        /// </summary>
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
