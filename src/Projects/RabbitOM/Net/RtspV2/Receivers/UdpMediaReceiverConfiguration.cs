using System;

namespace RabbitOM.Net.RtspV2.Receivers
{
    public sealed class UdpMediaReceiverConfiguration : RtspMediaReceiverConfiguration
    {
        public int TransportPort { get; }

        public TimeSpan TransportReceiveTimeout { get; }
    }
}
