using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Receivers.Udp
{
    public class UdpMediaReceiverConfiguration : RtspMediaReceiverConfiguration
    {
        public int TransportPort { get; }

        public TimeSpan TransportReceiveTimeout { get; }
    }
}
