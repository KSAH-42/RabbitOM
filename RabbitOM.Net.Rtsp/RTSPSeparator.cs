using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a message header seperator
    /// </summary>
    internal enum RTSPSeparator
    {
        /// <summary>
        /// Comma (,)
        /// </summary>
        Comma = ',',

        /// <summary>
        /// SemiColon (;)
        /// </summary>
        SemiColon = ';',

        /// <summary>
        /// Slash (/)
        /// </summary>
        Slash = '/',
    }
}
