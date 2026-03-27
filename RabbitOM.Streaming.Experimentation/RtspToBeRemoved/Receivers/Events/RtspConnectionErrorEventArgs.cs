using System;

namespace RabbitOM.Streaming.Experimentation.RtspToBeRemoved.Receivers
{
    public class RtspConnectionErrorEventArgs : RtspErrorEventArgs
    {
        public RtspConnectionErrorEventArgs( string message ) : base( message ) { }
    }
}
