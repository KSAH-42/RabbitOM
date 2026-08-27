using System;

namespace RabbitOM.Net.Rtsp.Clients
{
    public sealed class RtspKeepAliveInvoker : RtspInvoker
    {
        internal RtspKeepAliveInvoker( RtspProxy proxy , RtspMethod method ) : base( proxy , method )
        {
        }

        public IRtspInvoker SetSessionId( string value )
        {
            Builder.SessionId = value;

            return this;
        }
    }
}
