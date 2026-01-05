using System;

namespace RabbitOM.Streaming.Net.Rtcp
{
    public struct RtcpReportBlock
    {
        public const int Size = 24;


        public uint SynchronizationSource { get; private set; }

        public byte FractionLost { get; private set; }

        public uint CummulativePacketsLost { get; private set; }

        public uint ExtendedHighestSequence { get; private set; }

        public uint InterarrivalJitter { get; private set; }

        public uint LastSRTimestamp { get; private set; }

        public uint DelaySinceLastSR { get; private set; }




        public static bool TryParse( in ArraySegment<byte> payload , out RtcpReportBlock result )
        {
            result = default;

            if ( payload.Array == null || payload.Count < Size )
            {
                return false;
            }
            
            var offset = payload.Offset;
            
            result.SynchronizationSource  = (uint) payload.Array[ offset ++ ] << 24;
            result.SynchronizationSource |= (uint) payload.Array[ offset ++ ] << 16;
            result.SynchronizationSource |= (uint) payload.Array[ offset ++ ] << 8;
            result.SynchronizationSource |=        payload.Array[ offset ++ ];

            result.FractionLost = payload.Array[ offset ++ ];

            result.CummulativePacketsLost  = (uint) payload.Array[ offset ++ ] << 16;
            result.CummulativePacketsLost |= (uint) payload.Array[ offset ++ ] << 8;
            result.CummulativePacketsLost |=        payload.Array[ offset ++ ];

            result.ExtendedHighestSequence  = (uint) payload.Array[ offset ++ ] << 24;
            result.ExtendedHighestSequence |= (uint) payload.Array[ offset ++ ] << 16;
            result.ExtendedHighestSequence |= (uint) payload.Array[ offset ++ ] << 8;
            result.ExtendedHighestSequence |=        payload.Array[ offset ++ ];

            result.InterarrivalJitter  = (uint) payload.Array[ offset ++ ] << 24;
            result.InterarrivalJitter |= (uint) payload.Array[ offset ++ ] << 16;
            result.InterarrivalJitter |= (uint) payload.Array[ offset ++ ] << 8;
            result.InterarrivalJitter |=        payload.Array[ offset ++ ];

            result.LastSRTimestamp  = (uint) payload.Array[ offset ++ ] << 24;
            result.LastSRTimestamp |= (uint) payload.Array[ offset ++ ] << 16;
            result.LastSRTimestamp |= (uint) payload.Array[ offset ++ ] << 8;
            result.LastSRTimestamp |=        payload.Array[ offset ++ ];

            result.DelaySinceLastSR  = (uint) payload.Array[ offset ++ ] << 24;
            result.DelaySinceLastSR |= (uint) payload.Array[ offset ++ ] << 16;
            result.DelaySinceLastSR |= (uint) payload.Array[ offset ++ ] << 8;
            result.DelaySinceLastSR |=        payload.Array[ offset ++ ];

            return true;
        }
    }
}
