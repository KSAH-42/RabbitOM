using System;

namespace RabbitOM.Net.Rtsp.Clients
{
    public sealed class TearDownRtspInvoker : RtspInvoker
    {
        internal TearDownRtspInvoker( RtspProxy proxy ) : base( proxy , RtspMethod.TearDown )
        {
        }

        public IRtspInvoker SetSessionId( string value )
        {
            Builder.SessionId = value;

            return this;
        }
    }
}
