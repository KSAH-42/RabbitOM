using System;

namespace RabbitOM.Net.Rtsp.Clients
{
    public sealed class PauseRtspInvoker : RtspInvoker
    {
        internal PauseRtspInvoker( RtspProxy proxy ) : base( proxy , RtspMethod.Pause )
        {
        }

        public IRtspInvoker SetSessionId( string value )
        {
            Builder.SessionId = value;

            return this;
        }
    }
}
