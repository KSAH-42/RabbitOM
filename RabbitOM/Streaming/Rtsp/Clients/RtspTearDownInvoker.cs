using System;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    public sealed class RtspTearDownInvoker : RtspInvoker
    {
        internal RtspTearDownInvoker( RtspProxy proxy ) : base( proxy , RtspMethod.TearDown )
        {
        }

        public IRtspInvoker SetSessionId( string value )
        {
            Builder.SessionId = value;

            return this;
        }
    }
}
