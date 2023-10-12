using System;

namespace RabbitOM.Net.Rtps
{
    /// <summary>
    /// Represent the authentication type
    /// </summary>
    public enum RTSPAuthenticationType
    {
        /// <summary>
        /// Unknown
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Basic which generaly refer as base 64 encoding
        /// </summary>
        Basic,

        /// <summary>
        /// Digest which generaly refers as MD5 hash type
        /// </summary>
        Digest
    }
}
