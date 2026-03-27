using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp2BeRemoved.Receivers
{
    public class RtspStreamingStartErrorEventArgs : RtspErrorEventArgs
    {
        public RtspStreamingStartErrorEventArgs( string message ) : base ( message ) { }
    }
}
