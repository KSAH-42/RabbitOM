using System;

namespace RabbitOM.Net.Rtps.Remoting
{
    /// <summary>
    /// Represent an event args
    /// </summary>
    public class RTSPConnectionErrorEventArgs : EventArgs
    {
        private readonly Exception _exception = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="exception">the exception</param>
        public RTSPConnectionErrorEventArgs( Exception exception )
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
