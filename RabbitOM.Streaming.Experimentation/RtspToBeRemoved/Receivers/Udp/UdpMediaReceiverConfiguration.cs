using System;

namespace RabbitOM.Streaming.Experimentation.RtspToBeRemoved.Receivers.Udp
{
    public class UdpMediaReceiverConfiguration : RtspMediaReceiverConfiguration
    {
        public int TransportPort { get; }

        public TimeSpan TransportReceiveTimeout { get; }
    }
}
