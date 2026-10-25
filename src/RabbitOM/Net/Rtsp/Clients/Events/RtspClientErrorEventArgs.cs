using System;

namespace RabbitOM.Net.Rtsp.Clients
{
    public sealed class RtspClientErrorEventArgs : EventArgs
    {
        public RtspClientErrorEventArgs( RtspClientErrorCode code , string message )
        {
            Code = code;
            Message = message ?? string.Empty;
        }

        public RtspClientErrorCode Code
        {
            get;
        }

        public string Message
        {
            get;
        }
    }
}
