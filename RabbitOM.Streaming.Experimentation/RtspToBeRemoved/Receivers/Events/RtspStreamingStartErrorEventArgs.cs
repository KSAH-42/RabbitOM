using System;

namespace RabbitOM.Streaming.Experimentation.RtspToBeRemoved.Receivers
{
    public class RtspStreamingStartErrorEventArgs : RtspErrorEventArgs
    {
        public RtspStreamingStartErrorEventArgs( string message ) : base ( message ) { }
    }
}
