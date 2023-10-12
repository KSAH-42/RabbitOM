using System;

namespace RabbitOM.Net.Rtps.Remoting.Invokers
{
    /// <summary>
    /// Represent the proxy invoker
    /// </summary>
    public sealed class RTSPKeepAliveInvoker : RTSPInvoker
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="proxy">the proxy</param>
        /// <param name="method">the method</param>
        internal RTSPKeepAliveInvoker( RTSPProxy proxy , RTSPMethodType method )
            : base( proxy , method )
        {
        }

        /// <summary>
        /// Sets the session identifier. In somes cases, the options can be used as a keep alive method, at this moment you must specify a session identifier to maintain the existance of the session.
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
