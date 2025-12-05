using System;

namespace RabbitOM.Streaming.Net.Rtsp.Clients.Connections.Invokers
{
    /// <summary>
    /// Represent the proxy invoker
    /// </summary>
    public sealed class RtspAnnounceInvoker : RtspInvoker
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="proxy">the proxy</param>
        internal RtspAnnounceInvoker( RtspProxy proxy )
            : base( proxy , RtspMethod.Announce )
        {
        }

        /// <summary>
        /// Sets the session identifier
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public IRtspInvoker SetSessionId( string value )
        {
            Builder.SessionId = value;

            return this;
        }
    }
}
