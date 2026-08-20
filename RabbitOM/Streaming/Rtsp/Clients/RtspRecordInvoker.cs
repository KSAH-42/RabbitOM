using System;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    public sealed class RtspRecordInvoker : RtspInvoker
    {
        internal RtspRecordInvoker( RtspConnector proxy ) : base( proxy , RtspMethod.Record )
        {
        }

        public IRtspInvoker SetSessionId( string value )
        {
            Builder.SessionId = value;

            return this;
        }
    }
}
