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
            result = default;

            if ( buffer.Array == null || buffer.Count < Size )
            {
                return false;
            }
            
            var offset = buffer.Offset;
            
            result.SynchronizationSource  = (uint) buffer.Array[ offset ++ ] << 24;
            result.SynchronizationSource |= (uint) buffer.Array[ offset ++ ] << 16;
            result.SynchronizationSource |= (uint) buffer.Array[ offset ++ ] << 8;
            result.SynchronizationSource |=        buffer.Array[ offset ++ ];

            result.FractionLost = buffer.Array[ offset ++ ];

            result.CummulativePacketsLost  = (uint) buffer.Array[ offset ++ ] << 16;
            result.CummulativePacketsLost |= (uint) buffer.Array[ offset ++ ] << 8;
            result.CummulativePacketsLost |=        buffer.Array[ offset ++ ];

            result.ExtendedHighestSequence  = (uint) buffer.Array[ offset ++ ] << 24;
            result.ExtendedHighestSequence |= (uint) buffer.Array[ offset ++ ] << 16;
            result.ExtendedHighestSequence |= (uint) buffer.Array[ offset ++ ] << 8;
            result.ExtendedHighestSequence |=        buffer.Array[ offset ++ ];

            result.InterarrivalJitter  = (uint) buffer.Array[ offset ++ ] << 24;
            result.InterarrivalJitter |= (uint) buffer.Array[ offset ++ ] << 16;
            result.InterarrivalJitter |= (uint) buffer.Array[ offset ++ ] << 8;
            result.InterarrivalJitter |=        buffer.Array[ offset ++ ];

            result.LastSRTimestamp  = (uint) buffer.Array[ offset ++ ] << 24;
            result.LastSRTimestamp |= (uint) buffer.Array[ offset ++ ] << 16;
            result.LastSRTimestamp |= (uint) buffer.Array[ offset ++ ] << 8;
            result.LastSRTimestamp |=        buffer.Array[ offset ++ ];

            result.DelaySinceLastSR  = (uint) buffer.Array[ offset ++ ] << 24;
            result.DelaySinceLastSR |= (uint) buffer.Array[ offset ++ ] << 16;
            result.DelaySinceLastSR |= (uint) buffer.Array[ offset ++ ] << 8;
            result.DelaySinceLastSR |=        buffer.Array[ offset ++ ];

            return true;
        }
    }
}
