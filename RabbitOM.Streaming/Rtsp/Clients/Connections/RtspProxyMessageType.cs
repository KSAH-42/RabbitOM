using System;

namespace RabbitOM.Streaming.Rtsp.Clients.Connections
{
    /// <summary>
    /// Represent the decoder message type
    /// </summary>
    internal enum RtspProxyMessageType
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
