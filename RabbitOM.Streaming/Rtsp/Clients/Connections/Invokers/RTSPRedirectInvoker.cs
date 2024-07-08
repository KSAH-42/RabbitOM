using System;

namespace RabbitOM.Streaming.Rtsp.Clients.Connections.Invokers
{
    /// <summary>
    /// Represent the proxy invoker
    /// </summary>
    public sealed class RTSPRedirectInvoker : RTSPInvoker
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="proxy">the proxy</param>
        internal RTSPRedirectInvoker( RTSPProxy proxy )
            : base( proxy , RTSPMethod.Redirect )
        {
        }
    }
}
