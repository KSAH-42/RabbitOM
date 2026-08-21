using System;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    public sealed class RtspOptionsInvoker : RtspInvoker
    {
        internal RtspOptionsInvoker( RtspProxy proxy ) : base( proxy , RtspMethod.Options )
        {
        }
    }
}
