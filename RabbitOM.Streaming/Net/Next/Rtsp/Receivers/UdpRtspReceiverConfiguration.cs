using System;

namespace RabbitOM.Streaming.Net.Next.Rtsp.Receivers
{
    public class UdpRtspReceiverConfiguration : RtspReceiverConfiguration
    {
        public int Port { get; }
        public TimeSpan ReceiveTranportTimeout { get; }
    }
}
