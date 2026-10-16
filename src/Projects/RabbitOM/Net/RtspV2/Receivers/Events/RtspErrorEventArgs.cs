using System;

namespace RabbitOM.Net.RtspV2.Receivers
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
