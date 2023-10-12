using System;

namespace RabbitOM.Net.Rtps.Remoting.Invokers
{
    /// <summary>
    /// Represent the proxy invoker
    /// </summary>
    public sealed class RTSPGetOptionsInvoker : RTSPInvoker
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="proxy">the proxy</param>
        internal RTSPGetOptionsInvoker( RTSPProxy proxy )
            : base( proxy , RTSPMethodType.Options )
        {
        }
    }
}
