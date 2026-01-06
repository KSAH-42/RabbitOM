using System;

namespace RabbitOM.Streaming.Net.Rtcp
{
    public sealed class RtcpReportBlock
    {
        public const int Size = 24;




        public uint SynchronizationSource { get; private set; }
        
        public byte FractionLost { get; private set; }
        
        public uint CummulativePacketsLost { get; private set; }
        
        public uint ExtendedHighestSequence { get; private set; }
        
        public uint InterarrivalJitter { get; private set; }
        
        public uint LastSRTimestamp { get; private set; }
        
        public uint DelaySinceLastSR { get; private set; }




        public static bool TryParse( in ArraySegment<byte> buffer , out RtcpReportBlock result )
        {
            throw new NotImplementedException();
        }
    }
}
