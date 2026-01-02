using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.Experimental
{
    public class SequenceCompletedEventArgs : EventArgs
    {
        public SequenceCompletedEventArgs( IReadOnlyCollection<RtpPacket> packets ) 
            => Packets = packets;

        public IReadOnlyCollection<RtpPacket> Packets { get; }
    }
}
