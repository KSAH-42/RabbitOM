using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    public class MediaBuildedEventArgs : EventArgs
    {
        public MediaBuildedEventArgs( MediaElement element )
        {
            MediaElement = element ?? throw new ArgumentNullException( nameof( element ) );
        }

        public MediaElement MediaElement { get; }
    }
}
