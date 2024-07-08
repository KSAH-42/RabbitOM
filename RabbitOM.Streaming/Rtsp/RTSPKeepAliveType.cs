using System;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent the keep alive method type
    /// </summary>
    public enum RTSPKeepAliveType
    {
        /// <summary>
        /// The OPTIONS method as a keep alive method is used in many cameras, we strongly recommand to use this method for the majority of camera
        /// </summary>
        Options = 0,

        /// <summary>
        /// The GET_PARAMETER method as a keep alive method is used for onvif compliant device
        /// </summary>
        GetParameter,

        /// <summary>
        /// The SET_PARAMETER method as a keep alive method
        /// </summary>
        SetParameter,
    }
}
