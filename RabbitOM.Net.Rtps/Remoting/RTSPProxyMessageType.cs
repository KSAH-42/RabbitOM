using System;

namespace RabbitOM.Net.Rtps.Remoting
{
    /// <summary>
    /// Represent the decoder message type
    /// </summary>
    public enum RTSPProxyMessageType
    {
        /// <summary>
        /// None type
        /// </summary>
        None = 0,

        /// <summary>
        /// Message type
        /// </summary>
        Message,

        /// <summary>
        /// Interleave type
        /// </summary>
        Interleaved
    }
}
