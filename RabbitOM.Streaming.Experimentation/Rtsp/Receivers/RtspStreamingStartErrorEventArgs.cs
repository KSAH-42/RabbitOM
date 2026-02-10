using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Receivers
{
    public class RtspStreamingStartErrorEventArgs : RtspErrorEventArgs
    {
        public RtspStreamingStartErrorEventArgs( string message ) : base ( message ) { }
    }
}
