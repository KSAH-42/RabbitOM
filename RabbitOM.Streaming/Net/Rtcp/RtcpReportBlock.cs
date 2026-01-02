using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtcp
{
    /// <summary>
    /// Represent an rtcp report block
    /// </summary>
    public struct RtcpReportBlock
    {
        private const int Size = 24;



        /// <summary>
        /// Gets the syncrhonization source
        /// </summary>
        public uint SynchronizationSource { get; private set; }

        /// <summary>
        /// Gets the fraction lost
        /// </summary>
        public byte FractionLost { get; private set; }

        /// <summary>
        /// Gets the cummulative packets lost
        /// </summary>
        public uint CummulativePacketsLost { get; private set; }

        /// <summary>
        /// Gets the extented highest sequence number
        /// </summary>
        public uint ExtendedHighestSequence { get; private set; }

        /// <summary>
        /// Gets the interarrival jitter
        /// </summary>
        public uint InterarrivalJitter { get; private set; }

        /// <summary>
        /// Gets the last SR (send report) timestamp
        /// </summary>
        public uint LastSRTimestamp { get; private set; }

        /// <summary>
        /// Gets the delay since last SR (send report)
        /// </summary>
        public uint DelaySinceLastSR { get; private set; }




        /// <summary>
        /// Parse all report block from a payload
        /// </summary>
        /// <param name="payload">the payload</param>
        /// <returns>returns a collection of reports</returns>
        /// <exception cref="NotImplementedException"/>
        public static IList<RtcpReportBlock> Parse( in ArraySegment<byte> payload )
        {
            if ( payload.Count == 0 )
            {
                return new List<RtcpReportBlock>();
            }

            var reports = new List<RtcpReportBlock>();
            
            var offset  = payload.Offset;
            
            while ( (offset + Size) <= payload.Array.Length )
            {
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

                reports.Add( report );
            }

            return reports;
        }
    }
}
