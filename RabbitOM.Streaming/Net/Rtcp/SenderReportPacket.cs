using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtcp
{
    public sealed class SenderReportPacket : RtcpPacket
    {
        public const int PacketType = 200;

        public const int MimimunSize = 24;
        



        private readonly List<RtcpReportBlock> _reports = new List<RtcpReportBlock>();




        public SenderReportPacket( byte version ) : base ( version ) { }




        public uint SynchronizationSourceId { get; private set; }
        
        public ulong NtpTimeStamp { get; private set; }
        
        public uint RtpTimeStamp { get; private set; }
        
        public uint NumberOfPackets { get; private set; }
        
        public uint NumberOfBytes { get; private set; }
        
        public IReadOnlyList<RtcpReportBlock> Reports { get => _reports; }




        public static bool TryCreateFrom( RtcpMessage message , out SenderReportPacket result )
        {
            result = null;

            if ( message == null || message.Payload.Count < MimimunSize )
            {
                return false;
            }

            var offset = message.Payload.Offset;

            result = new SenderReportPacket( message.Version );

            result.SynchronizationSourceId  = (uint) ( message.Payload.Array[ offset ++ ] << 24 );
            result.SynchronizationSourceId |= (uint) ( message.Payload.Array[ offset ++ ] << 16 );
            result.SynchronizationSourceId |= (uint) ( message.Payload.Array[ offset ++ ] << 8  );
            result.SynchronizationSourceId |=          message.Payload.Array[ offset ++ ];
            
            result.NtpTimeStamp  = (ulong) message.Payload.Array[ offset ++ ] << 56;
            result.NtpTimeStamp |= (ulong) message.Payload.Array[ offset ++ ] << 48;
            result.NtpTimeStamp |= (ulong) message.Payload.Array[ offset ++ ] << 40;
            result.NtpTimeStamp |= (ulong) message.Payload.Array[ offset ++ ] << 32;
            result.NtpTimeStamp |= (ulong) message.Payload.Array[ offset ++ ] << 24;
            result.NtpTimeStamp |= (ulong) message.Payload.Array[ offset ++ ] << 16;
            result.NtpTimeStamp |= (ulong) message.Payload.Array[ offset ++ ] << 8;
            result.NtpTimeStamp |= message.Payload.Array[ offset ++ ] ;

            result.RtpTimeStamp  = (uint) message.Payload.Array[ offset ++ ] << 24;
            result.RtpTimeStamp |= (uint) message.Payload.Array[ offset ++ ] << 16;
            result.RtpTimeStamp |= (uint) message.Payload.Array[ offset ++ ] << 8;
            result.RtpTimeStamp |= message.Payload.Array[ offset ++ ];

            result.NumberOfPackets  = (uint) message.Payload.Array[ offset ++ ] << 24;
            result.NumberOfPackets |= (uint) message.Payload.Array[ offset ++ ] << 16;
            result.NumberOfPackets |= (uint) message.Payload.Array[ offset ++ ] << 8;
            result.NumberOfPackets |= message.Payload.Array[ offset ++ ];

            result.NumberOfBytes  = (uint) message.Payload.Array[ offset ++ ] << 24;
            result.NumberOfBytes |= (uint) message.Payload.Array[ offset ++ ] << 16;
            result.NumberOfBytes |= (uint) message.Payload.Array[ offset ++ ] << 8;
            result.NumberOfBytes |= message.Payload.Array[ offset ++ ];

            if ( message.Payload.Count > MimimunSize )
            {
                while ( ( offset + RtcpReportBlock.Size ) <= message.Payload.Array.Length )
                {
                    if ( RtcpReportBlock.TryParse( new ArraySegment<byte>( message.Payload.Array , offset , RtcpReportBlock.Size ) , out var report ) )
                    {
                        System.Diagnostics.Debug.Assert( report != null );

                        result._reports.Add( report );
                    }

                    offset += RtcpReportBlock.Size;
                }
            }

            return true;
        }
    }
}
