using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp
{
    public class SequenceEventArgs : EventArgs
    {
        public SequenceEventArgs( IReadOnlyCollection<RtpPacket> packets ) 
        {
            Packets = packets ?? throw new ArgumentNullException( nameof( packets ) );
        }

        public IReadOnlyCollection<RtpPacket> Packets { get; }
    }
}
