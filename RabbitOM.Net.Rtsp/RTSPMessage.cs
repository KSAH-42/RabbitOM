using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent the base message class
    /// </summary>
    public abstract class RTSPMessage
    {
        /// <summary>
        /// Gets the headers
        /// </summary>
        public abstract RTSPHeaderList Headers
        {
            get;
        }

        /// <summary>
        /// Gets the body
        /// </summary>
        public abstract RTSPMessageBody Body
        {
            get;
        }

        /// <summary>
        /// Gets the version
        /// </summary>
        public abstract RTSPMessageVersion Version
        {
            get;
        }

        /// <summary>
        /// Validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public abstract bool Validate();
    }
}
