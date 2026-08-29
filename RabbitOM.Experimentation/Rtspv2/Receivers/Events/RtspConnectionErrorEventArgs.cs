using System;

namespace RabbitOM.Net.RtspV2.Receivers
{
    public class RtspConnectionErrorEventArgs : RtspErrorEventArgs
    {
        public RtspConnectionErrorEventArgs( string message ) : base( message ) { }
    }
}
