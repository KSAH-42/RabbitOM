using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.Framing.HEVC
{
    public sealed class HEVCPacket
    {
        public int Header { get; private set; }
        public bool HeaderForbiddenBit { get; private set; }
        public byte HeaderLayerId { get; private set; }
        public byte HeaderTemporalId { get; private set; }
        public HEVCPacketType HeaderType { get; private set; }
        public ArraySegment<byte> Payload { get; private set; }
        




        public bool TryValidate()
        {
            return true;
        }

        public IList<ArraySegment<byte>> GetAggregationUnits()
        {
            var results = new List<ArraySegment<byte>>( 100 );

            for ( var index = 0 ; index < Payload.Count - 2 ; )
            {
                var size = Payload.Array[ Payload.Offset + index++ ] * 0x100 | Payload.Array[ Payload.Offset + index ];

                var delta = Payload.Count - index++;

                if ( 0 < size && size < delta )
                {
                    results.Add( new ArraySegment<byte>( Payload.Array , index , size ) );

                    index += size;
                }
            }

            return results;
        }




        public static bool TryParse( ArraySegment<byte> buffer , out HEVCPacket result )
        {
            result = null;

            if ( buffer.Count <= 2 )
            {
                return false;
            }

            // https://datatracker.ietf.org/doc/html/rfc7798#section-1.1.4

            var header = ( buffer.Array[ buffer.Offset ] << 8 ) | ( buffer.Array[ buffer.Offset + 1 ] );

            result = new HEVCPacket();

            result.Header              = header;
            result.HeaderTemporalId    = (byte)           ( ( header      ) & 0x07 );
            result.HeaderLayerId       = (byte)           ( ( header >> 3 ) & 0x1F );
            result.HeaderLayerId      |= (byte)           ( ( header >> 8 ) & 0x01 );
            result.HeaderType          = (HEVCPacketType) ( ( header >> 9 ) & 0x3F );
            result.HeaderForbiddenBit  = (byte)           ( ( header >> 15) & 0x1  ) == 1;

            result.Payload = new ArraySegment<byte>(  buffer.Array , buffer.Offset + 2 , buffer.Array.Length - (buffer.Offset + 2) );
            
            return true;
        }
    } 
}