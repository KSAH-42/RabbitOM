using System;

namespace RabbitOM.Net.Rtsp.Clients.Connections.Invokers
{
    /// <summary>
    /// Represent the proxy invoker
    /// </summary>
    public sealed class RTSPTearDownInvoker : RTSPInvoker
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="proxy">the proxy</param>
        internal RTSPTearDownInvoker( RTSPProxy proxy )
            : base( proxy , RTSPMethod.TearDown )
        {
        }

        /// <summary>
        /// Sets the session identifier
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public IRTSPInvoker SetSessionId( string value )
        {
            Builder.SessionId = value;

            return this;
        }
    }
}
