using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp
{
    public class SequenceCompletedEventArgs : SequenceEventArgs
    {
        public SequenceCompletedEventArgs( IReadOnlyCollection<RtpPacket> packets ) : base ( packets )
        {
        }
    }
}
