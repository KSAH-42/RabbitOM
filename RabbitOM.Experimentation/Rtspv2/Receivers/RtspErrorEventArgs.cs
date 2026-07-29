using System;

namespace RabbitOM.Streaming.RtspV2.Receivers
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
