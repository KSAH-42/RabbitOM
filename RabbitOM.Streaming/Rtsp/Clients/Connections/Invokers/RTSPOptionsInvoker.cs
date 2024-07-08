using System;

namespace RabbitOM.Streaming.Rtsp.Clients.Connections.Invokers
{
    /// <summary>
    /// Represent the proxy invoker
    /// </summary>
    public sealed class RTSPOptionsInvoker : RTSPInvoker
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="proxy">the proxy</param>
        internal RTSPOptionsInvoker( RTSPProxy proxy )
            : base( proxy , RTSPMethod.Options )
        {
        }
    }
}
