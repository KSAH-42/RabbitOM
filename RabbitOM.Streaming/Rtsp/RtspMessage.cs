using System;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent the base message class
    /// </summary>
    public abstract class RtspMessage
    {
        /// <summary>
        /// Gets the headers
        /// </summary>
        public abstract RtspHeaderCollection Headers
        {
            get;
        }

        /// <summary>
        /// Gets the body
        /// </summary>
        public abstract RtspMessageBody Body
        {
            get;
        }

        /// <summary>
        /// Gets the version
        /// </summary>
        public abstract RtspMessageVersion Version
        {
            get;
        }

        /// <summary>
        /// Validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public abstract bool TryValidate();
    }
}
