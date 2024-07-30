using System;

namespace RabbitOM.Streaming.Rtp.Framing
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
            get => GetField( ref _maximumPayloadSize );
            set => SetField( ref _maximumPayloadSize , value );
        }

        /// <summary>
        /// Gets / Sets the numbers of packets allowed per frame
        /// </summary>
        public int NumberOfPacketsPerFrame
        {
            get => GetField( ref _numberOfPacketsPerFrame );
            set => SetField( ref _numberOfPacketsPerFrame , value );
        }






        /// <summary>
        /// Get the field value using a lock
        /// </summary>
        /// <typeparam name="TValue">type of the value</typeparam>
        /// <param name="memberValue">the member value</param>
        /// <returns>returns a value</returns>
        protected TValue GetField<TValue>( ref TValue memberValue )
        {
            lock ( _lock )
            {
                return memberValue;
            }
        }

        /// <summary>
        /// Set the field value using a lock
        /// </summary>
        /// <typeparam name="TValue">type of the value</typeparam>
        /// <param name="memberValue">the member value</param>
        /// <param name="value">the value</param>
        protected void SetField<TValue>( ref TValue memberValue , TValue value )
        {
            lock ( _lock )
            {
                memberValue = value;
            }
        }
    }
}
