using System;

namespace RabbitOM.Net.Rtsp.Clients
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
