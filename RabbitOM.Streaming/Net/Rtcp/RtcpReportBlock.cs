using System;

namespace RabbitOM.Streaming.Net.Rtcp
{
    public struct RtcpReportBlock
    {
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

            if ( payload.Array == null || payload.Count < 24 )
            {
                return false;
            }
            
            var offset = payload.Offset;
            
            var report = new RtcpReportBlock();

            report.SynchronizationSource |= (uint) payload.Array[ offset ++ ] << 24;
            report.SynchronizationSource |= (uint) payload.Array[ offset ++ ] << 16;
            report.SynchronizationSource |= (uint) payload.Array[ offset ++ ] << 8;
            report.SynchronizationSource |=        payload.Array[ offset ++ ];

            report.FractionLost = payload.Array[ offset ++ ];

            report.CummulativePacketsLost |= (uint) payload.Array[ offset ++ ] << 16;
            report.CummulativePacketsLost |= (uint) payload.Array[ offset ++ ] << 8;
            report.CummulativePacketsLost |=        payload.Array[ offset ++ ];

            report.ExtendedHighestSequence |= (uint) payload.Array[ offset ++ ] << 24;
            report.ExtendedHighestSequence |= (uint) payload.Array[ offset ++ ] << 16;
            report.ExtendedHighestSequence |= (uint) payload.Array[ offset ++ ] << 8;
            report.ExtendedHighestSequence |=        payload.Array[ offset ++ ];

            report.InterarrivalJitter |= (uint) payload.Array[ offset ++ ] << 24;
            report.InterarrivalJitter |= (uint) payload.Array[ offset ++ ] << 16;
            report.InterarrivalJitter |= (uint) payload.Array[ offset ++ ] << 8;
            report.InterarrivalJitter |=        payload.Array[ offset ++ ];

            report.LastSRTimestamp |= (uint) payload.Array[ offset ++ ] << 24;
            report.LastSRTimestamp |= (uint) payload.Array[ offset ++ ] << 16;
            report.LastSRTimestamp |= (uint) payload.Array[ offset ++ ] << 8;
            report.LastSRTimestamp |=        payload.Array[ offset ++ ];

            report.DelaySinceLastSR |= (uint) payload.Array[ offset ++ ] << 24;
            report.DelaySinceLastSR |= (uint) payload.Array[ offset ++ ] << 16;
            report.DelaySinceLastSR |= (uint) payload.Array[ offset ++ ] << 8;
            report.DelaySinceLastSR |=        payload.Array[ offset ++ ];

            return true;
        }
    }
}
