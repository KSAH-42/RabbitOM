using System;

namespace RabbitOM.Streaming.Net.Next.Rtsp.Receivers.Events
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
