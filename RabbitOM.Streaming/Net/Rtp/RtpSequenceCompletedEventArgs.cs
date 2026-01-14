using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp
{
    public class RtpSequenceCompletedEventArgs : RtpSequenceEventArgs
    {
        public RtpSequenceCompletedEventArgs( IReadOnlyCollection<RtpPacket> packets ) : base ( packets )
        {
        }
    }
}
