using System;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent the RTSP digest algorithm type
    /// </summary>
    public enum RTSPDigestAlgorithmType
    {
        /// <summary>
        /// Un defined
        /// </summary>
        UnDefined  = 0,

        /// <summary>
        /// The MD5 type
        /// </summary>
        MD5,

        /// <summary>
        /// The SHA 256 type
        /// </summary>
        SHA_256,

        /// <summary>
        /// The SHA 512 type
        /// </summary>
        SHA_512
    }
}
