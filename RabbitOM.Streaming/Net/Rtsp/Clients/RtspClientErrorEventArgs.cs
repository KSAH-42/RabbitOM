using System;

namespace RabbitOM.Streaming.Net.Rtsp.Clients
{
    /// <summary>
    /// Represent an event args
    /// </summary>
    public sealed class RtspClientErrorEventArgs : EventArgs
    {
        private readonly RtspClientErrorCode _code    = RtspClientErrorCode.Unknown;

        private readonly string              _message = string.Empty;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="code">the error code</param>
        /// <param name="message">the message</param>
        public RtspClientErrorEventArgs( RtspClientErrorCode code , string message )
        {
            _code    = code;
            _message = message ?? string.Empty;
        }

        /// <summary>
        /// Gets the error code
        /// </summary>
        public RtspClientErrorCode Code
        {
            get => _code;
        }

        /// <summary>
        /// Gets the message
        /// </summary>
        public string Message
        {
            get => _message;
        }
    }
}
