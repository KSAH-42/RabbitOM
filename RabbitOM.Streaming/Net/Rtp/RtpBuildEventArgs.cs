using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    public class RtpBuildEventArgs : EventArgs
    {
        public RtpBuildEventArgs( RtpMediaElement content )
        {
            MediaContent = content ?? throw new ArgumentNullException( nameof( content ) );
        }

        public DateTime TimeStamp { get; } = DateTime.Now;

        public RtpMediaElement MediaContent { get; }
    }
}
