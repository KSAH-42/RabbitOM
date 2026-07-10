using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Receivers.Udp
{
    public sealed class MulticastMediaReceiverConfiguration : RtspMediaReceiverConfiguration
    {
        public string TransportIPAddress { get; }

        public int TransportPort { get; }

        public byte TransportTTL { get; }

        public TimeSpan TransportReceiveTimeout { get; }
    }
}
