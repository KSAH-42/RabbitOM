using System;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    public sealed class RtspKeepAliveInvoker : RtspInvoker
    {
        internal RtspKeepAliveInvoker( RtspConnector proxy , RtspMethod method ) : base( proxy , method )
        {
        }

        public IRtspInvoker SetSessionId( string value )
        {
            Builder.SessionId = value;

            return this;
        }
    }
}
