using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp
{
    public interface IPacketAggregator
    {
        int MaximumNumberOfPackets { get; set; }

        bool IsSequenceTooLong { get; }

        bool HasCompleteSequence { get; }

        bool HasUnOrderedSequence { get; }

        IReadOnlyCollection<RtpPacket> Packets { get; }




        void AddPacket( RtpPacket packet );

        void RemovePackets();

        void Clear();

        void SortSequence();

        IReadOnlyCollection<RtpPacket> GetSequence();
    }
}
