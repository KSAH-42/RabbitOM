using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels;
    
    public sealed class RtspClientContext 
    {
        public int ReceiveBufferSize { get; }

        public int SendBufferSize { get; }

        public sbyte ChannelPoolSize { get; } // set 1 to emulate a single socket

        public TimeSpan ChannelPoolExpirationDelay { get; }

        public TimeSpan RetryDelay { get; }
        
        public sbyte NumberOfRetries { get; }

        public int MaximumOfHeaders { get; }

        public int MaximumOfHeadersSize { get; }

        public IHandler Handler { get; }

        public IClientChannelFactory ChannelFactory { get; }

        public IAllocator Allocator { get; }

        public ILogger Logger { get; }

        public ushort MinimumSequenceValue { get; }

        public ushort MaximumSequenceValue { get; }

        public TimeSpan ReceiveTimeout { get; }

        public TimeSpan SendTimeout { get; }




        // TODO: don't forget to move this method the internal class
        public ushort GetNextSequenceValue()
        {
            throw new ArgumentNullException();
        }

        // TODO: don't forget to move this method the internal class
        public void ClearSequenceValue()
        {
            throw new ArgumentNullException();
        }
    }
}
