using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    public class RtpMediaBuildedEventArgs : EventArgs
    {
        public RtpMediaBuildedEventArgs( RtpMediaElement element )
        {
            MediaElement = element ?? throw new ArgumentNullException( nameof( element ) );
        }

        public DateTime TimeStamp { get; } = DateTime.Now;

        public RtpMediaElement MediaElement { get; }
    }
}
