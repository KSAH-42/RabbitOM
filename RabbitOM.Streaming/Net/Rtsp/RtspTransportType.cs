using System;

namespace RabbitOM.Streaming.Net.Rtsp
{
    /// <summary>
    /// Represent the transport layer types
    /// </summary>
    public enum RtspTransportType
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
