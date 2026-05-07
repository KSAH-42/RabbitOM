using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports
{
    public sealed class EndPoint
    {
        public string IpAddress { get; }

        public ushort Port { get; }

        public uint BufferSize { get; }

        public bool IsSecured { get; }

        public TimeSpan ReceiveTimeout { get; }

        public TimeSpan SendTimeout { get; }        
    }
}
