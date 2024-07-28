using System;

namespace RabbitOM.Streaming.Rtp.Framing
{
    /// <summary>
    /// Represent the frame event args
    /// </summary>
    public class RtpFrameReceivedEventArgs : EventArgs
    {
        private readonly RtpFrame _frame;

        /// <summary>
        /// Initialize a new instancel of the frame event args
        /// </summary>
        /// <param name="frame">the frame</param>
        /// <exception cref="ArgumentNullException"/>
        public RtpFrameReceivedEventArgs( RtpFrame frame )
        {
            _frame = frame ?? throw new ArgumentNullException( nameof( frame ) );
        }

        /// <summary>
        /// Gets the frame
        /// </summary>
        public RtpFrame Frame
        {
            get => _frame;
        }
    }
}
