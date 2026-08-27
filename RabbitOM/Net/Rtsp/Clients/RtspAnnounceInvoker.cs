using System;

namespace RabbitOM.Net.Rtsp.Clients
{
    public sealed class RtspAnnounceInvoker : RtspInvoker
    {
        internal RtspAnnounceInvoker( RtspProxy proxy ) : base( proxy , RtspMethod.Announce )
        {
        }

        public IRtspInvoker SetSessionId( string value )
        {
            Builder.SessionId = value;

            return this;
        }
    }
}
