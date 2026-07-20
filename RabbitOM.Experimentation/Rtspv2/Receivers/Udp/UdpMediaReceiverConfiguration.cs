using System;

namespace RabbitOM.Streaming.RtspV2.Receivers.Udp
{
    public sealed class UdpMediaReceiverConfiguration : RtspMediaReceiverConfiguration
    {
        public int TransportPort { get; }

        public TimeSpan TransportReceiveTimeout { get; }
    }
}
