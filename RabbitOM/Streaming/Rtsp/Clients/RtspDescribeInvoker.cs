using System;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    public sealed class RtspDescribeInvoker : RtspInvoker
    {
        internal RtspDescribeInvoker( RtspConnector proxy )
            : base( proxy , RtspMethod.Describe )
        {
        }

        public IRtspInvoker SetHeaderAcceptSdp()
        {
            return SetHeaderAccept(RtspMimeType.ApplicationSdp);
        }

        public IRtspInvoker SetHeaderAccept(RtspMimeType mimeType)
        {
            Builder.AcceptHeader = mimeType?.ToString() ?? string.Empty;

            return this;
        }
    }
}
