using System;

namespace RabbitOM.Streaming.Net.RtspV2.Receivers.Events
{
    public class RtspConnectionErrorEventArgs : RtspErrorEventArgs
    {
        public RtspConnectionErrorEventArgs( string message ) : base( message ) { }
    }
}
