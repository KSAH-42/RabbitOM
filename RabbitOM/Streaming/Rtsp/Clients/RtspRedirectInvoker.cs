using System;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    public sealed class RtspRedirectInvoker : RtspInvoker
    {
        internal RtspRedirectInvoker( RtspConnector proxy )
            : base( proxy , RtspMethod.Redirect )
        {
        }
    }
}
