using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp
{
    public abstract class RtpPacketAggregator
    {
        public abstract bool HasCompleteSequence { get; }
        public abstract bool HasUnOrderedSequence { get; }
        public abstract IReadOnlyCollection<RtpPacket> Packets { get; } 
        

        public abstract void AddPacket( RtpPacket packet );
        public abstract void RemovePackets();
        public abstract void Clear();
        public abstract void SortSequence();
        public abstract IReadOnlyCollection<RtpPacket> GetSequence();
    }
}
