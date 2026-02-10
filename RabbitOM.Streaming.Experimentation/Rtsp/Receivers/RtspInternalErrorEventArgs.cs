using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Receivers
{
    public class RtspInternalErrorEventArgs : RtspErrorEventArgs
    {
        public RtspInternalErrorEventArgs( string message ) : base ( message ) { }
    }
}
