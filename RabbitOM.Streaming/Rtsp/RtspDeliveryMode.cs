using System;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent the diffusion mode type
    /// </summary>
    public enum RtspDeliveryMode
    {
        /// <summary>
        /// Unicast Udp
        /// </summary>
        Udp = 0,

        /// <summary>
        /// Unicast Tcp which represent the interleaved mode
        /// </summary>
        Tcp,

        /// <summary>
        /// Multicast
        /// </summary>
        Multicast,
    }
}
