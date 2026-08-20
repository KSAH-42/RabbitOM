using System;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    public class RtspMessageSendedEventArgs : EventArgs
    {
        private readonly RtspMessage _message = null;

        public RtspMessageSendedEventArgs( RtspMessage message )
        {
            _message = message ?? throw new ArgumentNullException( nameof( message ) );
        }

        public RtspMessage Message
        {
            get => _message;
        }
    }
}
