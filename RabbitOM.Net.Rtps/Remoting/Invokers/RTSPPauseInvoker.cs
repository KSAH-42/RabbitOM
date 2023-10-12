using System;

namespace RabbitOM.Net.Rtps.Remoting.Invokers
{
    /// <summary>
    /// Represent the proxy invoker
    /// </summary>
    public sealed class RTSPPauseInvoker : RTSPInvoker
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="proxy">the proxy</param>
        internal RTSPPauseInvoker( RTSPProxy proxy )
            : base( proxy , RTSPMethodType.Pause )
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
