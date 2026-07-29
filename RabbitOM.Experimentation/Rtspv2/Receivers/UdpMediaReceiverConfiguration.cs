using System;

namespace RabbitOM.Streaming.RtspV2.Receivers
{
    public sealed class UdpMediaReceiverConfiguration : RtspMediaReceiverConfiguration
    {
        public int TransportPort { get; }

        public TimeSpan TransportReceiveTimeout { get; }
    }
}
