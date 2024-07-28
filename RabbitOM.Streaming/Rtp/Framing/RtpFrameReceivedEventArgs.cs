using System;

namespace RabbitOM.Streaming.Rtp.Framing
{
    public class RtpFrameReceivedEventArgs : EventArgs
    {
        private readonly RtpFrame _frame;

        public RtpFrameReceivedEventArgs( RtpFrame frame )
        {
            _frame = frame ?? throw new ArgumentNullException( nameof( frame ) ); ;
        }

        public RtpFrame Frame
        {
            get => _frame;
        }
    }
}
