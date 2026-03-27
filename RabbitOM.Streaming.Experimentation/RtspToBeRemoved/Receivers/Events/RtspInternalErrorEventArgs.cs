using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp2BeRemoved.Receivers
{
    public class RtspInternalErrorEventArgs : RtspErrorEventArgs
    {
        public RtspInternalErrorEventArgs( string message ) : base ( message ) { }
    }
}
