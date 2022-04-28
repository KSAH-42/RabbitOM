using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent the transport lower types
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
