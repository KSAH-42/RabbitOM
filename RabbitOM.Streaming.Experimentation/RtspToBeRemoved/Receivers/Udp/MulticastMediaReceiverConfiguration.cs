using System;

namespace RabbitOM.Streaming.Experimentation.RtspToBeRemoved.Receivers.Udp
{
    public class MulticastMediaReceiverConfiguration : RtspMediaReceiverConfiguration
    {
        public string MulticastIPAddress { get; }
        public int TransportPort { get; }
        public byte TransportTTL { get; }
        public TimeSpan TransportReceiveTimeout { get; }
    }
}
