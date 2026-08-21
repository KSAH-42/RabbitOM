using System;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    public sealed class RtspPauseInvoker : RtspInvoker
    {
        internal RtspPauseInvoker( RtspProxy proxy ) : base( proxy , RtspMethod.Pause )
        {
        }

        public IRtspInvoker SetSessionId( string value )
        {
            Builder.SessionId = value;

            return this;
        }
    }
}
