using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Transports;

    public sealed class RtspClientContext 
    {
        public uint ReceiveBufferSize { get; }

        public uint SendBufferSize { get; }

        public sbyte ChannelPoolSize { get; } // set 1 to emulate a single socket

        public TimeSpan ChannelPoolExpirationDelay { get; }

        public TimeSpan RetryDelay { get; }
        
        public sbyte NumberOfRetries { get; }

        public IHandler Handler { get; }

        public IClientChannelFactory ChannelFactory { get; }

        // public Action<string> Log { get; }
    }
}
