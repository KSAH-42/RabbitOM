using System;

namespace RabbitOM.Net.Rtsp.Remoting.Invokers
{
    /// <summary>
    /// Represent the proxy invoker
    /// </summary>
    public sealed class RTSPDescribeInvoker : RTSPInvoker
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="proxy">the proxy</param>
        internal RTSPDescribeInvoker( RTSPProxy proxy )
            : base( proxy , RTSPMethod.Describe )
        {
        }

        /// <summary>
        /// Set the accept type default value
        /// </summary>
        /// <returns>returns the current instance</returns>
        public IRTSPInvoker SetHeaderAcceptSdp()
        {
            return SetHeaderAccept(RTSPMimeType.ApplicationSdp);
        }

        /// <summary>
        /// Set the content type
        /// </summary>
        /// <param name="mimeType">the mime type</param>
        /// <returns>returns the current instance</returns>
        public IRTSPInvoker SetHeaderAccept(RTSPMimeType mimeType)
        {
            Builder.AcceptHeader = mimeType?.ToString() ?? string.Empty;

            return this;
        }
    }
}
