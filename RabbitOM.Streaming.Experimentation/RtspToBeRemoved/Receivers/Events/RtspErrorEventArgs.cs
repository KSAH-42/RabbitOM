using System;

namespace RabbitOM.Streaming.Experimentation.RtspToBeRemoved.Receivers
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
