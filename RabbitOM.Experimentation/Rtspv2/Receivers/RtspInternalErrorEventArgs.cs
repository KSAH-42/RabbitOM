using System;

namespace RabbitOM.Streaming.RtspV2.Receivers
{
    public class RtspInternalErrorEventArgs : RtspErrorEventArgs
    {
        public RtspInternalErrorEventArgs( string message ) : base ( message ) { }
    }
}
