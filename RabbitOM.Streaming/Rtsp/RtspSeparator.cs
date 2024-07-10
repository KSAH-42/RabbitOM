using System;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent a message header seperator
    /// </summary>
    internal enum RtspSeparator
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
