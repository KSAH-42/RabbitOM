using System;

namespace RabbitOM.Net.Rtps.Clients
{
    /// <summary>
    /// Represent the client error codes
    /// </summary>
    public enum RTSPClientErrorCode
    {
        /// <summary>
        /// Unknown error code
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Connection failed error indicate the connection can be establish to the remote machine, for example socket.connect error
        /// </summary>
        ConnectionFailed,

        /// <summary>
        /// Gets options failed
        /// </summary>
        GetOptionsFailed,

        /// <summary>
        /// Describe failed
        /// </summary>
        DescribeFailed,

        /// <summary>
        /// Setup failed
        /// </summary>
        SetupFailed,

        /// <summary>
        /// Play failed
        /// </summary>
        PlayFailed,
                
        /// <summary>
        /// Failed to open the transport layer
        /// </summary>
        TransportOpenFailed,

        /// <summary>
        /// Keep alive failed
        /// </summary>
        KeepAliveFailed ,
    }
}
