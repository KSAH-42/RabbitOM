using System;

namespace RabbitOM.Net.Rtsp.Clients
{
    public sealed class RecordRtspInvoker : RtspInvoker
    {
        internal RecordRtspInvoker( RtspProxy proxy ) : base( proxy , RtspMethod.Record )
        {
        }

        public IRtspInvoker SetSessionId( string value )
        {
            Builder.SessionId = value;

            return this;
        }
    }
}
