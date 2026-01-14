using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp
{
    public class RtpSequenceEventArgs : EventArgs
    {
        public RtpSequenceEventArgs( IReadOnlyCollection<RtpPacket> packets ) 
        {
            Packets = packets ?? throw new ArgumentNullException( nameof( packets ) );
        }

        public IReadOnlyCollection<RtpPacket> Packets { get; }
    }
}
