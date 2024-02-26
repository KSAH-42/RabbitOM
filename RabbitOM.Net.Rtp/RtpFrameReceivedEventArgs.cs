using System;

namespace RabbitOM.Net.Rtp
{
    /// <summary>
    /// Represent the base frame class
    /// </summary>
    public class RtpFrameReceivedEventArgs : EventArgs
    {
        private readonly RtpFrame _frame;

        /// <summary>
        /// Initialize a new instance of the event args
        /// </summary>
        /// <param name="frame">the frame</param>
        /// <exception cref="RtpFrameReceivedEventArgs"/>
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
