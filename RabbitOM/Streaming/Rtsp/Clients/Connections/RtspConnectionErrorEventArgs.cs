using System;

namespace RabbitOM.Streaming.Rtsp.Clients.Connections
{
    /// <summary>
    /// Represent an event args
    /// </summary>
    public class RtspConnectionErrorEventArgs : EventArgs
    {
        private readonly Exception _exception = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="exception">the exception</param>
        public RtspConnectionErrorEventArgs( Exception exception )
        {
            _exception = exception;
        }

        /// <summary>
        /// Gets the exception
        /// </summary>
        public Exception Exception
        {
            get => _exception;
        }
    }
}
