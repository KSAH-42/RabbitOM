using System;

namespace RabbitOM.Net.Rtp
{
    /// <summary>
    /// Represent the base frame class
    /// </summary>
    public abstract class RtpFrame
    {
        private readonly DateTime _timestamp;

        private readonly ArraySegment<byte> _frameSegment;





        /// <summary>
        /// Initialize a new instance of the frame class
        /// </summary>
        /// <param name="timestamp">the time stamp</param>
        /// <param name="frameSegment">the data segment</param>
        protected RtpFrame( DateTime timestamp , ArraySegment<byte> frameSegment )
        {
            _timestamp = timestamp;
            _frameSegment = frameSegment;
        }





        /// <summary>
        ///  Gets the time stamp
        /// </summary>
        public DateTime Timestamp
        {
            get => _timestamp;
        }

        /// <summary>
        /// Gets frame segment
        /// </summary>
        public ArraySegment<byte> FrameSegment
        {
            get => _frameSegment;
        }
    }
}
