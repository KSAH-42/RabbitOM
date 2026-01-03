using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    public interface IMediaBuilder
    {
        event EventHandler<RtpFilteringPacketEventArgs> FilteringPacket;

        event EventHandler<RtpPacketReceivedEventArgs> PacketReceived;

        event EventHandler<RtpClearedEventArgs> Cleared;



        void AddPacket( RtpPacket packet );

        void Clear();
    }
}
