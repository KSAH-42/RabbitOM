using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    public sealed class RtspClientContext 
    {
        public ushort SocketBufferSize { get; }

        public sbyte ConnectionPoolSize { get; } // set 1 to emulate a single single socket

        public TimeSpan ConnectionPoolExpirationDelay { get; }

        public TimeSpan RetriesDelay { get; }
        
        public sbyte NumberOfRetries { get; }

        public IHandler Handler { get; }

        // public EventHandler ConnectedHandler { get; }

        // public EventHandler DisconnectedHandler { get; }

        // public EventHandler<InterleavedPacket> InterleavedPacketReceivedHandler { get; }

        // public Action<string> LogInfo { get; }

        // public Action<string> LogDebug { get; }

        // public Action<string> LogWarning { get; }

        // public Action<string> LogError { get; }

        // public StringValueNormalizer ValueNormalizer { get; }

        // public StringValueChecker ValueChecker { get; }
    }
}
