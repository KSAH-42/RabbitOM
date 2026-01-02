using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.Experimental
{
    public class SequenceSortingEventArgs : EventArgs
    {
        public SequenceSortingEventArgs( IReadOnlyCollection<RtpPacket> packets ) 
        {
            Packets = packets ?? throw new ArgumentNullException( nameof( packets ) );
        }

        public IReadOnlyCollection<RtpPacket> Packets { get; }
    }
}
