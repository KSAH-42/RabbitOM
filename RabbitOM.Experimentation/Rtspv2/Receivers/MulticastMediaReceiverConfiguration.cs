using System;

namespace RabbitOM.Net.RtspV2.Receivers
{
    public sealed class MulticastMediaReceiverConfiguration : RtspMediaReceiverConfiguration
    {
        public string TransportIPAddress { get; }

        public int TransportPort { get; }

        public byte TransportTTL { get; }

        public TimeSpan TransportReceiveTimeout { get; }
    }
}
