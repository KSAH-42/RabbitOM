using System;

namespace RabbitOM.Streaming.Net.Next.Rtsp.Receivers.Events
{
    public class RtspStreamingStartErrorEventArgs : RtspErrorEventArgs
    {
        public RtspStreamingStartErrorEventArgs( string message ) : base ( message ) { }
    }
}
