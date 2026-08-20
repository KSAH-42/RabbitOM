using System;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    public class RtspMessageReceivedEventArgs : EventArgs
    {
        private readonly RtspMessage _message = null;

        public RtspMessageReceivedEventArgs( RtspMessage message )
        {
            _message = message ?? throw new ArgumentNullException( nameof( message ) );
        }

        public RtspMessage Message
        {
            get => _message;
        }
    }
}
