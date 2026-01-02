using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.Experimental
{
    public class SortingSequenceEventArgs : EventArgs
    {
        public SortingSequenceEventArgs( IReadOnlyCollection<RtpPacket> packets ) 
        {
            Packets = packets ?? throw new ArgumentNullException( nameof( packets ) );
        }

        public IReadOnlyCollection<RtpPacket> Packets { get; }
    }
}
