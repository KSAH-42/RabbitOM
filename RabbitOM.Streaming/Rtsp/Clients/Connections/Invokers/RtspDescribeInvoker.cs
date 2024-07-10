using System;

namespace RabbitOM.Streaming.Rtsp.Clients.Connections.Invokers
{
    /// <summary>
    /// Represent the proxy invoker
    /// </summary>
    public sealed class RtspDescribeInvoker : RtspInvoker
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="proxy">the proxy</param>
        internal RtspDescribeInvoker( RtspProxy proxy )
            : base( proxy , RtspMethod.Describe )
        {
        }

        /// <summary>
        /// Set the accept type default value
        /// </summary>
        /// <returns>returns the current instance</returns>
        public IRtspInvoker SetHeaderAcceptSdp()
        {
            return SetHeaderAccept(RtspMimeType.ApplicationSdp);
        }

        /// <summary>
        /// Set the content type
        /// </summary>
        /// <param name="mimeType">the mime type</param>
        /// <returns>returns the current instance</returns>
        public IRtspInvoker SetHeaderAccept(RtspMimeType mimeType)
        {
            Builder.AcceptHeader = mimeType?.ToString() ?? string.Empty;

            return this;
        }
    }
}
