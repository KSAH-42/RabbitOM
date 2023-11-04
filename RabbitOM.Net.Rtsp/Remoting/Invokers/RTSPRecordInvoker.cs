using System;

namespace RabbitOM.Net.Rtsp.Remoting.Invokers
{
    /// <summary>
    /// Represent the proxy invoker
    /// </summary>
    public sealed class RTSPRecordInvoker : RTSPInvoker
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="proxy">the proxy</param>
        internal RTSPRecordInvoker( RTSPProxy proxy )
            : base( proxy , RTSPMethod.Record )
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
