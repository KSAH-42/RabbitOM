using System;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    /// <summary>
    /// Represent the proxy invoker
    /// </summary>
    public sealed class RtspPlayInvoker : RtspInvoker
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="proxy">the proxy</param>
        internal RtspPlayInvoker( RtspConnector proxy )
            : base( proxy , RtspMethod.Play )
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
