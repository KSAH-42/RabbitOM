using System;

namespace RabbitOM.Streaming.RtspV2.Receivers
{
    public class RtspConnectionErrorEventArgs : RtspErrorEventArgs
    {
        public RtspConnectionErrorEventArgs( string message ) : base( message ) { }
    }
}
