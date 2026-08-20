using System;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    public sealed class RtspOptionsInvoker : RtspInvoker
    {
        internal RtspOptionsInvoker( RtspConnector proxy ) : base( proxy , RtspMethod.Options )
        {
        }
    }
}
