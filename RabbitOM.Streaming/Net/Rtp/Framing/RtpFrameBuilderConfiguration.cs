using System;

namespace RabbitOM.Streaming.Net.Rtp.Framing
{
    /// <summary>
    /// Represent the builder configuration class
    /// </summary>
    public class RtpFrameBuilderConfiguration
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
        /// Gets the sync root
        /// </summary>
        protected object SyncRoot
        {
            get => _lock;
        }

        /// <summary>
        /// Gets / Sets the maximul payload size
        /// </summary>
        public int MaximumPayloadSize
        {
            get
            {
                lock ( SyncRoot )
                {
                    return _maximumPayloadSize;
                }
            }

            set
            {
                lock ( SyncRoot )
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
                lock ( SyncRoot )
                {
                    return _numberOfPacketsPerFrame;
                }
            }

            set
            {
                lock ( SyncRoot )
                {
                    _numberOfPacketsPerFrame = value;
                }
            }
        }
    }
}
