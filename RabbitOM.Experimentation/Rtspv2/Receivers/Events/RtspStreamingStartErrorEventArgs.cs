using System;

namespace RabbitOM.Net.RtspV2.Receivers
{
    public class RtspStreamingStartErrorEventArgs : RtspErrorEventArgs
    {
        public RtspStreamingStartErrorEventArgs( string message ) : base ( message ) { }
    }
}
