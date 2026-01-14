using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    public class RtpClearedEventArgs : EventArgs
    {
        public static readonly RtpClearedEventArgs Default = new RtpClearedEventArgs();
    }
}
