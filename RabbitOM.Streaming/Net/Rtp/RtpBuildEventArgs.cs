using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    public abstract class RtpBuildEventArgs : EventArgs
    {
        public DateTime TimeStamp { get; } = DateTime.Now;
    }
}
