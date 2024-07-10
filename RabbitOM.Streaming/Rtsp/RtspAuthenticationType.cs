using System;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent the authentication type
    /// </summary>
    public enum RtspAuthenticationType
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
