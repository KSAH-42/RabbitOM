using System;

namespace RabbitOM.Streaming.RtspV2.Transports.Channels
{
    public class Binding
    {
        public int ReceiveRetries { get; }

        public int ReceiveBufferSize { get; }

        public int SendBufferSize { get; }

        public TimeSpan ReceiveTimeout { get; }

        public TimeSpan SendTimeout { get; }

        public TimeSpan OpenTimeout { get; }

        public TimeSpan CloseTimeout { get; }
    }
}
