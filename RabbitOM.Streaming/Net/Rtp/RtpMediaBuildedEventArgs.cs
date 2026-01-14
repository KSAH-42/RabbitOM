using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    public class RtpMediaBuildedEventArgs : EventArgs
    {
        public RtpMediaBuildedEventArgs( RtpMediaElement element )
        {
            MediaElement = element ?? throw new ArgumentNullException( nameof( element ) );
        }

        public RtpMediaElement MediaElement { get; }
    }
}
