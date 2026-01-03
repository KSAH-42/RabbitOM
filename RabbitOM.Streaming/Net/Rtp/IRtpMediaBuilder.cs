using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    public interface IRtpMediaBuilder
    {
        event EventHandler<RtpPacketAddingEventArgs> PacketAdding;

        event EventHandler<RtpPacketAddedEventArgs> PacketAdded;

        event EventHandler<RtpBuildEventArgs> Builded;

        event EventHandler Cleared;

        void AddPacket( RtpPacket packet );

        void Clear();
    }
}
