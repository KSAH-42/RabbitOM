using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp
{
    public interface IFrameBuilder
    {
        event EventHandler<RtpPacketAddedEventArgs> PacketAdded;

        event EventHandler<RtpPacketAddingEventArgs> PacketAdding;

        event EventHandler<RtpSequenceCompletedEventArgs> SequenceCompleted;

        event EventHandler<RtpSequenceSortingEventArgs> SequenceSorting;

        event EventHandler<RtpFrameReceivedEventArgs> FrameReceived;

        event EventHandler<RtpClearedEventArgs> Cleared;




        IReadOnlyCollection<RtpPacket> Packets { get; }




        void AddPacket( RtpPacket packet );

        void Clear();
    }
}
