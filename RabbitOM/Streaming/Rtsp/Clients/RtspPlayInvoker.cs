using System;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    public sealed class RtspPlayInvoker : RtspInvoker
    {
        internal RtspPlayInvoker( RtspProxy proxy )
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
