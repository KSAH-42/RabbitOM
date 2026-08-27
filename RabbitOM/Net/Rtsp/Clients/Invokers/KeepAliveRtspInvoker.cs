using System;

namespace RabbitOM.Net.Rtsp.Clients
{
    public sealed class KeepAliveRtspInvoker : RtspInvoker
    {
        internal KeepAliveRtspInvoker( RtspProxy proxy , RtspMethod method ) : base( proxy , method )
        {
        }

        public IRtspInvoker SetSessionId( string value )
        {
            Builder.SessionId = value;

            return this;
        }
    }
}
