using System;

namespace RabbitOM.Streaming.Net.Rtsp.Clients.Connections.Invokers
{
    /// <summary>
    /// Represent the proxy invoker
    /// </summary>
    public sealed class RtspOptionsInvoker : RtspInvoker
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="proxy">the proxy</param>
        internal RtspOptionsInvoker( RtspProxy proxy )
            : base( proxy , RtspMethod.Options )
        {
        }
    }
}
