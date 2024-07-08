using System;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    /// <summary>
    /// Represent an event args
    /// </summary>
    public sealed class RTSPClientErrorEventArgs : EventArgs
    {
        private readonly RTSPClientErrorCode _code    = RTSPClientErrorCode.Unknown;

        private readonly string              _message = string.Empty;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="code">the error code</param>
        /// <param name="message">the message</param>
        public RTSPClientErrorEventArgs( RTSPClientErrorCode code , string message )
        {
            _code    = code;
            _message = message ?? string.Empty;
        }

        /// <summary>
        /// Gets the error code
        /// </summary>
        public RTSPClientErrorCode Code
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
