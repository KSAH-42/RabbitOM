using System;

namespace RabbitOM.Net.Rtsp.Clients
{
    public sealed class RtspOptionsInvoker : RtspInvoker
    {
        internal RtspOptionsInvoker( RtspProxy proxy ) : base( proxy , RtspMethod.Options )
        {
        }
    }
}
