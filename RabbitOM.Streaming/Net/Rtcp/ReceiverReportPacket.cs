using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtcp
{
    public sealed class ReceiverReportPacket : RtcpPacket
    {
        public const int PacketType = 201;

        public const int MinimumSize = 4;



        private readonly List<RtcpReportBlock> _reports = new List<RtcpReportBlock>();



        public ReceiverReportPacket( byte version ) : base ( version ) { }



        public uint SynchronizationSourceId { get; private set; }
        
        public IReadOnlyList<RtcpReportBlock> Reports { get => _reports; }





        public static bool TryParse( RtcpMessage message , out ReceiverReportPacket result )
        {
            result = default ;

            if ( message == null || message.Payload.Count < MinimumSize )
            {
                return false;
            }

            var offset = message.Payload.Offset;

            result = new ReceiverReportPacket( message.Version );

            result.SynchronizationSourceId  = (uint) ( message.Payload.Array[ offset ++ ] << 24 );
            result.SynchronizationSourceId |= (uint) ( message.Payload.Array[ offset ++ ] << 16 );
            result.SynchronizationSourceId |= (uint) ( message.Payload.Array[ offset ++ ] << 8  );
            result.SynchronizationSourceId |=          message.Payload.Array[ offset ++ ];

            while ( ( offset + RtcpReportBlock.Size ) <= message.Payload.Array.Length )
            {
                if ( RtcpReportBlock.TryParse( new ArraySegment<byte>( message.Payload.Array , offset , RtcpReportBlock.Size ) , out var report ) )
                {
                    System.Diagnostics.Debug.Assert( report != null );

                    result._reports.Add( report );
                }

                offset += RtcpReportBlock.Size;
            }

            return true;
        }
    }
}
