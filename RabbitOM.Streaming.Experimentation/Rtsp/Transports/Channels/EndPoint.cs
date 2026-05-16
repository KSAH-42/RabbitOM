using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    public sealed class EndPoint
    {
        public string IpAddress { get; }

        public ushort Port { get; }

        public uint ReceiveBufferSize { get; }

        public uint SendBufferSize { get; }

        public bool IsSecured { get; }

        public TimeSpan ReceiveTimeout { get; }

        public TimeSpan SendTimeout { get; }        
    }
}
