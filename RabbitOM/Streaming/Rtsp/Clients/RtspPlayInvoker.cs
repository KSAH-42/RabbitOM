using System;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    public sealed class RtspPlayInvoker : RtspInvoker
    {
        internal RtspPlayInvoker( RtspConnector proxy )
            : base( proxy , RtspMethod.Play )
        {
        }

        public IRtspInvoker SetSessionId( string value )
        {
            Builder.SessionId = value;

            return this;
        }
    }
}
