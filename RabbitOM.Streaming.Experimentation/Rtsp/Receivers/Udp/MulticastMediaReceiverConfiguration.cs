using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Receivers.Udp
{
    public class MulticastMediaReceiverConfiguration : RtspMediaReceiverConfiguration
    {
        public string IPAddress { get; }
        public int RtpPort { get; }
        public byte TTL { get; }
        public TimeSpan ReceiveTranportTimeout { get; }
    }
}
