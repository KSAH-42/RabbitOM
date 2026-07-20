using System;

namespace RabbitOM.Streaming.RtspV2.Transports.Channels
{
    public class RtspMessageEventArgs : EventArgs
    {
        public RtspMessageEventArgs( RtspMessage message )
        {
            Message = message ?? throw new ArgumentNullException( nameof( message ) );
        }

        public RtspMessage Message { get; }
    }
}
