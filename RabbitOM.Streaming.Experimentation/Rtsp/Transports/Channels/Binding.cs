using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    public sealed class Binding
    {
        public int ReceiveRetries { get; } // number of retries during socket read timeout exception

        public int ReceiveBufferSize { get; }

        public int SendBufferSize { get; }

        public TimeSpan ReceiveTimeout { get; }

        public TimeSpan SendTimeout { get; }

        public TimeSpan OpenTimeout { get; }

        public TimeSpan CloseTimeout { get; }
    }
}
