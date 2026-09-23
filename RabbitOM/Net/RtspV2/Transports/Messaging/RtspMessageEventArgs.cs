using System;

namespace RabbitOM.Net.RtspV2.Transports
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
