using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Receivers.Events
{
    public class RtspStreamingStartErrorEventArgs : RtspErrorEventArgs
    {
        public RtspStreamingStartErrorEventArgs( string message ) : base ( message ) { }
    }
}
