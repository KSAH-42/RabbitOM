using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.Experimental
{
    public interface IFrameBuilder
    {
        event EventHandler<PacketAddedEventArgs> PacketAdded;

        event EventHandler<PacketAddingEventArgs> PacketAdding;

        event EventHandler<SequenceCompletedEventArgs> SequenceCompleted;

        event EventHandler<SequenceSortingEventArgs> SequenceSorting;

        event EventHandler<FrameReceivedEventArgs> FrameReceived;

        event EventHandler<ClearedEventArgs> Cleared;




        IReadOnlyCollection<RtpPacket> Packets { get; }




        void AddPacket( RtpPacket packet );

        void RemovePackets();

        void Clear();
    }
}
