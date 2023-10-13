using System;

namespace RabbitOM.Net.Rtsp.Clients
{
    /// <summary>
    /// Represent the configuration object
    /// </summary>
    public interface IRTSPClientConfiguration
    {
        /// <summary>
        /// Gets the sync root
        /// </summary>
        object SyncRoot
        {
            get;
        }

        /// <summary>
        /// Gets / Sets the uri
        /// </summary>
        string Uri
        {
            get;
            set;
        }

        /// <summary>
        /// Gets / Sets the user name
        /// </summary>
        string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets / Sets the password
        /// </summary>
        string Password
        {
            get;
            set;
        }

        /// <summary>
        /// Gets / Sets the receive timeout
        /// </summary>
        TimeSpan ReceiveTimeout
        {
            get;
            set;
        }

        /// <summary>
        /// Gets / Sets the send timeout
        /// </summary>
        TimeSpan SendTimeout
        {
            get;
            set;
        }

        /// <summary>
        /// Gets / Sets the keep alive mode
        /// </summary>
        RTSPKeepAliveType KeepAliveType
        {
            get;
            set;
        }


        /// <summary>
        /// Apply the default parameters
        /// </summary>
        void ToDefault();
    }
}
