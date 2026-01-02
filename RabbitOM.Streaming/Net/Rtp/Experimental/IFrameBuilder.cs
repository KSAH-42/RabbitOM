using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.Experimental
{
    public interface IFrameBuilder
    {
        event EventHandler<PacketAddingEventArgs> PacketAdding;

        event EventHandler<PacketAddedEventArgs> PacketAdded;

        event EventHandler<SortingSequenceEventArgs> SortingSequence;

        event EventHandler<SequenceCompletedEventArgs> SequenceCompleted;

        event EventHandler<ClearedEventArgs> Cleared;

        event EventHandler<FrameReceivedEventArgs> FrameReceived;




        IReadOnlyCollection<RtpPacket> Packets { get; }




        void AddPacket( RtpPacket packet );

        void RemovePackets();

        void Clear();
    }
}
