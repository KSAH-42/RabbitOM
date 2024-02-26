using System;

namespace RabbitOM.Net.Rtp
{
    /// <summary>
    /// Represent the media payload parser
    /// </summary>
    public interface IRtpPayloadParser : IDisposable
    {
        /// <summary>
        /// Occurs when a frame has been received
        /// </summary>
        event EventHandler<RtpFrameReceivedEventArgs> FrameReceived;

        /// <summary>
        /// Parse the data
        /// </summary>
        /// <param name="timeOffset">the time stamp</param>
        /// <param name="packet">the packet</param>
        void Parse( TimeSpan timeOffset , RtpPacket packet );

        /// <summary>
        /// Reset
        /// </summary>
        void Reset();
    }
}
