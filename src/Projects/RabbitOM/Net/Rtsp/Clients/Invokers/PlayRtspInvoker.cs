using System;

namespace RabbitOM.Net.Rtsp.Clients
{
    public sealed class PlayRtspInvoker : RtspInvoker
    {
        internal PlayRtspInvoker( RtspProxy proxy )
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
