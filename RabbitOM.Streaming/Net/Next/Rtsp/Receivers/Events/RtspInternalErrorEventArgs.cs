using System;

namespace RabbitOM.Streaming.Net.Next.Rtsp.Receivers.Events
{
    public class RtspInternalErrorEventArgs : RtspErrorEventArgs
    {
        public RtspInternalErrorEventArgs( string message ) : base ( message ) { }
    }
}
