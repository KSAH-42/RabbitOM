using System;

namespace RabbitOM.Net.Rtsp.Clients
{
    public sealed class RtspDescribeInvoker : RtspInvoker
    {
        internal RtspDescribeInvoker( RtspProxy proxy )
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
