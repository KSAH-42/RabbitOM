using System;

namespace RabbitOM.Net.Rtps.Remoting
{
    /// <summary>
    /// Represent an event args
    /// </summary>
    public class RTSPConnectionMessageSendedEventArgs : EventArgs
    {
        private readonly RTSPMessage _message = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">the message</param>
        /// <exception cref="ArgumentNullException"/>
        public RTSPConnectionMessageSendedEventArgs( RTSPMessage message )
        {
            _message = message ?? throw new ArgumentNullException( nameof( message ) );
        }

        /// <summary>
        /// Gets the message
        /// </summary>
        public RTSPMessage Message
        {
            get => _message;
        }
    }
}
