using System;

namespace RabbitOM.Streaming.Net.Next.Rtsp.Receivers
{
    public class MulticastRtspReceiverConfiguration : RtspReceiverConfiguration
    {
        public string IPAddress { get; }
        public int RtpPort { get; }
        public byte TTL { get; }
        public TimeSpan ReceiveTranportTimeout { get; }
    }
}
