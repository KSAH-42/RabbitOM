using System;

namespace RabbitOM.Streaming.Net.RtspV2.Receivers.Events
{
    public class RtspInternalErrorEventArgs : RtspErrorEventArgs
    {
        public RtspInternalErrorEventArgs( string message ) : base ( message ) { }
    }
}
