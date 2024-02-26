using System;

namespace RabbitOM.Net.Rtp.Mjpeg
{
   
    /// <summary>
    /// Represent a MJPEG Frame
    /// </summary>
    [Obsolete] public sealed class JpegRtpFrame : RtpFrame
    {
        /// <summary>
        /// Initialize a new instance of the jpeg fraùe
        /// </summary>
        /// <param name="timestamp">the timestamp</param>
        /// <param name="frameSegment">the frame segment</param>
        public JpegRtpFrame( DateTime timestamp , ArraySegment<byte> frameSegment )
            : base( timestamp , frameSegment )
        {
        }
    }
}