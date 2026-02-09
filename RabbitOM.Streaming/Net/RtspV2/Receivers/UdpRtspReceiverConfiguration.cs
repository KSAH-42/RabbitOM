using System;

namespace RabbitOM.Streaming.Net.RtspV2.Receivers
{
    public class UdpRtspReceiverConfiguration : RtspReceiverConfiguration
    {
        public int Port { get; }
        public TimeSpan ReceiveTranportTimeout { get; }
    }
}
