using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    public interface IMediaBuilder
    {
        event EventHandler<RtpPacketAddingEventArgs> PacketAdding;

        event EventHandler<RtpPacketAddedEventArgs> PacketAdded;

        event EventHandler<RtpBuildEventArgs> Builded;

        event EventHandler Cleared;

        void AddPacket( RtpPacket packet );

        void Clear();
    }
}
