using System;

namespace RabbitOM.Net.Rtsp.Clients
{
    public sealed class RedirectRtspInvoker : RtspInvoker
    {
        internal RedirectRtspInvoker( RtspProxy proxy )
            : base( proxy , RtspMethod.Redirect )
        {
        }
    }
}
