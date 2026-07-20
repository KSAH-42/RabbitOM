using System;

namespace RabbitOM.Streaming.RtspV2.Receivers.Udp
{
    public sealed class MulticastMediaReceiverConfiguration : RtspMediaReceiverConfiguration
    {
        public string TransportIPAddress { get; }

        public int TransportPort { get; }

        public byte TransportTTL { get; }

        public TimeSpan TransportReceiveTimeout { get; }
    }
}
