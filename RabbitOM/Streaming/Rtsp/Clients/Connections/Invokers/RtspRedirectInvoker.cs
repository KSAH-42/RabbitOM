using System;

namespace RabbitOM.Streaming.Rtsp.Clients.Connections.Invokers
{
    /// <summary>
    /// Represent the proxy invoker
    /// </summary>
    public sealed class RtspRedirectInvoker : RtspInvoker
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="proxy">the proxy</param>
        internal RtspRedirectInvoker( RtspProxy proxy )
            : base( proxy , RtspMethod.Redirect )
        {
        }
    }
}
