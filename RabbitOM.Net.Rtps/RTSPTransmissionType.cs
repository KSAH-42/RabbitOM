using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent the diffusion mode type
    /// </summary>
    public enum RTSPTransmissionType
    {
        /// <summary>
        /// Unknown
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Unicast
        /// </summary>
        Unicast,

        /// <summary>
        /// Multicast
        /// </summary>
        Multicast,
    }
}
