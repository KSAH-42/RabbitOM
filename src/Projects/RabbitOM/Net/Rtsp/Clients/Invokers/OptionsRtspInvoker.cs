using System;

namespace RabbitOM.Net.Rtsp.Clients
{
    public sealed class OptionsRtspInvoker : RtspInvoker
    {
        internal OptionsRtspInvoker( RtspProxy proxy ) : base( proxy , RtspMethod.Options )
        {
        }
    }
}
