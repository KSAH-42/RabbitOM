using System;

namespace RabbitOM.Net.Rtsp.Clients
{
    public sealed class AnnounceRtspInvoker : RtspInvoker
    {
        internal AnnounceRtspInvoker( RtspProxy proxy ) : base( proxy , RtspMethod.Announce )
        {
        }

        public IRtspInvoker SetSessionId( string value )
        {
            Builder.SessionId = value;

            return this;
        }
    }
}
