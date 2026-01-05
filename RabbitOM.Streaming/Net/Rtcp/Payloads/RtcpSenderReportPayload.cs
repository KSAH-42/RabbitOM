using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtcp.Payloads
{
    public struct RtcpSenderReportPayload
    {
        public const int MimimunSize = 24;




        public uint SynchronizationSourceId { get; private set; }

        public ulong NtpTimeStamp { get; private set; }

        public uint RtpTimeStamp { get; private set; }

        public uint PacketCount { get; private set; }

        public uint OctetCount { get; private set; }

        public RtcpReportBlock[] Reports { get; private set; }





        public static bool TryParse( in ArraySegment<byte> payload , out RtcpSenderReportPayload result )
        {
            result = default;
            
            if ( payload.Array == null || payload.Count < MimimunSize )
            {
                return false;
            }

            var offset = payload.Offset;

            result.SynchronizationSourceId  = (uint) ( payload.Array[ offset ++ ] << 24 );
            result.SynchronizationSourceId |= (uint) ( payload.Array[ offset ++ ] << 16 );
            result.SynchronizationSourceId |= (uint) ( payload.Array[ offset ++ ] << 8  );
            result.SynchronizationSourceId |=          payload.Array[ offset ++ ];
            
            result.NtpTimeStamp  = (ulong) payload.Array[ offset ++ ] << 56;
            result.NtpTimeStamp |= (ulong) payload.Array[ offset ++ ] << 48;
            result.NtpTimeStamp |= (ulong) payload.Array[ offset ++ ] << 40;
            result.NtpTimeStamp |= (ulong) payload.Array[ offset ++ ] << 32;
            result.NtpTimeStamp |= (ulong) payload.Array[ offset ++ ] << 24;
            result.NtpTimeStamp |= (ulong) payload.Array[ offset ++ ] << 16;
            result.NtpTimeStamp |= (ulong) payload.Array[ offset ++ ] << 8;
            result.NtpTimeStamp |= payload.Array[ offset ++ ] ;

            result.RtpTimeStamp  = (uint) payload.Array[ offset ++ ] << 24;
            result.RtpTimeStamp |= (uint) payload.Array[ offset ++ ] << 16;
            result.RtpTimeStamp |= (uint) payload.Array[ offset ++ ] << 8;
            result.RtpTimeStamp |= payload.Array[ offset ++ ];

            result.PacketCount  = (uint) payload.Array[ offset ++ ] << 24;
            result.PacketCount |= (uint) payload.Array[ offset ++ ] << 16;
            result.PacketCount |= (uint) payload.Array[ offset ++ ] << 8;
            result.PacketCount |= payload.Array[ offset ++ ];

            result.OctetCount  = (uint) payload.Array[ offset ++ ] << 24;
            result.OctetCount |= (uint) payload.Array[ offset ++ ] << 16;
            result.OctetCount |= (uint) payload.Array[ offset ++ ] << 8;
            result.OctetCount |= payload.Array[ offset ++ ];

            if ( payload.Count > MimimunSize )
            {
                var reports = new List<RtcpReportBlock>();
            
                while ( ( offset + RtcpReportBlock.Size ) <= payload.Array.Length )
                {
                    if ( RtcpReportBlock.TryParse( new ArraySegment<byte>( payload.Array , offset , RtcpReportBlock.Size ) , out var block ) )
                    {
                        reports.Add( block );
                    }

                    offset += RtcpReportBlock.Size;
                }

                result.Reports = reports.ToArray();
            }

            return true;
        }
    }
}
