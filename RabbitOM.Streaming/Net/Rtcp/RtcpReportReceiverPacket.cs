using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtcp
{
    public struct RtcpReportReceiverPacket
    {
        public RtcpReportBlock[] ReportBlocks { get; private set; }

        public static bool TryParse( in ArraySegment<byte> payload , out RtcpReportReceiverPacket result )
        {
            result = default ;

            if ( payload.Array == null || payload.Count == 0 )
            {
                return false;
            }
            
            var offset = payload.Offset;

            var blocks = new List<RtcpReportBlock>();
            
            while ( (offset + 24) <= payload.Array.Length )
            {
                if ( RtcpReportBlock.TryParse( new ArraySegment<byte>( payload.Array , offset , 24 ) , out var block ) )
                {
                    blocks.Add( block );
                }

                offset += 24;
            }

            result = new RtcpReportReceiverPacket() { ReportBlocks = blocks.ToArray() };

            return result.ReportBlocks.Length > 0;
        }
    }
}
