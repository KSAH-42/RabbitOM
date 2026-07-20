using System;

namespace RabbitOM.Streaming.Rtsp.Clients.Connections
{
    /// <summary>
    /// Represent an event args
    /// </summary>
    public class RtspMessageReceivedEventArgs : EventArgs
    {
        private readonly RtspMessage _message = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">the message</param>
        /// <exception cref="ArgumentNullException"/>
        public RtspMessageReceivedEventArgs( RtspMessage message )
        {
            _message = message ?? throw new ArgumentNullException( nameof( message ) );
        }

        /// <summary>
        /// Gets the message
        /// </summary>
        public RtspMessage Message
        {
            get => _message;
        }
    }
}
