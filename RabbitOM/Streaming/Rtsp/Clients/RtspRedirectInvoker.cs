using System;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    public sealed class RtspRedirectInvoker : RtspInvoker
    {
        internal RtspRedirectInvoker( RtspProxy proxy )
            : base( proxy , RtspMethod.Redirect )
        {
        }
    }
}
