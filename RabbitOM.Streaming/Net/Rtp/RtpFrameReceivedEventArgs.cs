using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    public class RtpFrameReceivedEventArgs : EventArgs
    {
        public RtpFrameReceivedEventArgs( RtpFrame frame )
        {
            Frame = frame ?? throw new ArgumentNullException( nameof( frame ) );
        }

        public DateTime TimeStamp { get; } = DateTime.Now;
        public RtpFrame Frame { get; }
    }
}
