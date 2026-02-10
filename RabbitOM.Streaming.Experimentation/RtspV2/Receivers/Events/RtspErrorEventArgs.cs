using System;

namespace RabbitOM.Streaming.Net.RtspV2.Receivers.Events
{
    public class RtspErrorEventArgs : EventArgs
    {
        public RtspErrorEventArgs( string message )
        {
            Message = message;
        }
        
        public string Message { get; }
    }
}
