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

        public IAllocator Allocator { get; }

        public ILogger Logger { get; }

        public ushort MinimumSequenceValue { get; }

        public ushort MaximumSequenceValue { get; }




        public ushort GetNextSequenceValue()
        {
            throw new ArgumentNullException();
        }

        public void ClearSequenceValue()
        {
            throw new ArgumentNullException();
        }
    }
}
