using System;

namespace RabbitOM.Streaming.Rtp
{
    public class RtpClearedEventArgs : EventArgs
    {
        public static readonly RtpClearedEventArgs Default = new RtpClearedEventArgs();
    }
}
