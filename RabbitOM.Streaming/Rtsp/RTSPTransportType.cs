using System;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent the transport layer types
    /// </summary>
    public enum RTSPTransportType
    {
        /// <summary>
        /// Unknown
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// RTP over UDP
        /// </summary>
        RTP_AVP_UDP,

        /// <summary>
        /// RTP over TCP
        /// </summary>
        RTP_AVP_TCP,
    }
}
