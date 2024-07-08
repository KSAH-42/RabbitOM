using System;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent a message header seperator
    /// </summary>
    internal enum RTSPOperator
    {
        /// <summary>
        /// Equal sign (=)
        /// </summary>
        Equality = '=',

        /// <summary>
        /// Colon symbol (:)
        /// </summary>
        Colon = ':',
    }
}
