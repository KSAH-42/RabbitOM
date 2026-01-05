using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtcp.Payloads
{
    public struct RtcpReceiverReportPayload
    {
        public uint SynchronizationSourceId { get; private set; }

        public RtcpReportBlock[] Reports { get; private set; }

        public static bool TryParse( in ArraySegment<byte> payload , out RtcpReceiverReportPayload result )
        {
            result = default ;

            if ( payload.Array == null || payload.Count < 4 )
            {
                return false;
            }

            var offset = payload.Offset;

            result.SynchronizationSourceId |= (uint) ( payload.Array[ offset ++ ] << 24 );
            result.SynchronizationSourceId |= (uint) ( payload.Array[ offset ++ ] << 16 );
            result.SynchronizationSourceId |= (uint) ( payload.Array[ offset ++ ] << 8  );
            result.SynchronizationSourceId |=          payload.Array[ offset ++ ];

            var blocks = new List<RtcpReportBlock>();
            
            while ( ( offset + RtcpReportBlock.Size ) <= payload.Array.Length )
            {
                if ( RtcpReportBlock.TryParse( new ArraySegment<byte>( payload.Array , offset , 24 ) , out var block ) )
                {
                    blocks.Add( block );
                }

                offset += RtcpReportBlock.Size;
            }

            result.Reports = blocks.ToArray();

            return result.Reports.Length > 0;
        }
    }
}
