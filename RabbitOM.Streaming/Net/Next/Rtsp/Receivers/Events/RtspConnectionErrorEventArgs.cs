using System;

namespace RabbitOM.Streaming.Net.Next.Rtsp.Receivers.Events
{
    public class RtspConnectionErrorEventArgs : RtspErrorEventArgs
    {
        public RtspConnectionErrorEventArgs( string message ) : base( message ) { }
    }
}
