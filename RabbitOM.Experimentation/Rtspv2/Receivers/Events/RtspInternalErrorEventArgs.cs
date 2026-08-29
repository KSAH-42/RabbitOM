using System;

namespace RabbitOM.Net.RtspV2.Receivers
{
    public class RtspInternalErrorEventArgs : RtspErrorEventArgs
    {
        public RtspInternalErrorEventArgs( string message ) : base ( message ) { }
    }
}
