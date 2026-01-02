using System;

namespace RabbitOM.Streaming.Net.Rtp.Experimental
{
    public class FrameReceivedEventArgs : EventArgs
    {
        public FrameReceivedEventArgs( RtpFrame frame )
        {
            Frame = frame ?? throw new ArgumentNullException( nameof( frame ) );
        }

        public DateTime TimeStamp { get; } = DateTime.Now;
        public RtpFrame Frame { get; }
    }
}
