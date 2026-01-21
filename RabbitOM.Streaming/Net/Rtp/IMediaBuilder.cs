using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    public interface IMediaBuilder
    {
        event EventHandler<RtpPacketAddingEventArgs> PacketAdding;

        event EventHandler<RtpPacketAddedEventArgs> PacketAdded;

        event EventHandler<RtpMediaBuildedEventArgs> MediaBuilded;

        event EventHandler<RtpClearedEventArgs> Cleared;




        void AddPacket( RtpPacket packet );

        void Clear();
    }
}
