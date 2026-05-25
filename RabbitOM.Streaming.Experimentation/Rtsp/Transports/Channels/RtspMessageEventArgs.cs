using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    public sealed class RtspMessageEventArgs : EventArgs
    {        
        public RtspMessageEventArgs( RtspMessage message )
        {
            Message = message ?? throw new ArgumentNullException( nameof( message ) );
        }

        public RtspMessage Message { get; }
    }
}
