using System;

namespace RabbitOM.Streaming.Net.RtspV2.Receivers
{
    public class UdpMediaReceiverConfiguration : RtspMediaReceiverConfiguration
    {
        public int RtpPort { get; }
        public TimeSpan ReceiveTranportTimeout { get; }
    }
}
