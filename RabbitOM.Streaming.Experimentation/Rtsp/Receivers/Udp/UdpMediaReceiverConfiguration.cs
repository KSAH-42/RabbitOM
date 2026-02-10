using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Receivers.Udp
{
    public class UdpMediaReceiverConfiguration : RtspMediaReceiverConfiguration
    {
        public int RtpPort { get; }
        public TimeSpan ReceiveTranportTimeout { get; }
    }
}
