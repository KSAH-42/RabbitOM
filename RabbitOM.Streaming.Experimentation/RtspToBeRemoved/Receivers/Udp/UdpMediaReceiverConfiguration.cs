using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp2BeRemoved.Receivers.Udp
{
    public class UdpMediaReceiverConfiguration : RtspMediaReceiverConfiguration
    {
        public int TransportPort { get; }

        public TimeSpan TransportReceiveTimeout { get; }
    }
}
