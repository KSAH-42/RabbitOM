using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Receivers
{
    public class RtspConnectionErrorEventArgs : RtspErrorEventArgs
    {
        public RtspConnectionErrorEventArgs( string message ) : base( message ) { }
    }
}
