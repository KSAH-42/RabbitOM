using System;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    public sealed class RtspAnnounceInvoker : RtspInvoker
    {
        internal RtspAnnounceInvoker( RtspConnector proxy ) : base( proxy , RtspMethod.Announce )
        {
        }

        public IRtspInvoker SetSessionId( string value )
        {
            Builder.SessionId = value;

            return this;
        }
    }
}
