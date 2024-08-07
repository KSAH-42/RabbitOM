﻿using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    public sealed class H265NalUnit
    {
        public bool ForbiddenBit { get; set; }
        
        public NalUnitType Type { get; set; }
        
        public byte LayerId { get; set; }
        
        public byte TemporalId { get; set; }
        
        public ArraySegment<byte> Data { get; set; }





        public bool TryValidate()
        {
            return Type == NalUnitType.UNDEFINED || Type >= NalUnitType.INVALID;
        }

        public IEnumerable<ArraySegment<byte>> SplitData()
        {
            var results = new Queue<ArraySegment<byte>>();

            if ( Data.Count > 2 )
            {
                int index = 0;

                while ( index < Data.Count )
                {
                    int size = Data.Array[ Data.Offset + index ++ ] * 0x100 | Data.Array[ Data.Offset +  index ++ ];

                    if ( 0 < size && size < Data.Count - 2 )
                    {
                        results.Enqueue( new ArraySegment<byte>( Data.Array , index , size ) );
                    }

                    index += size;
                }
            }
            
            return results;
        }






        public static bool TryParse( ArraySegment<byte> buffer , out H265NalUnit result )
        {
            result = null;

            if ( buffer.Count <= 2 )
            {
                return false;
            }

            int header = ( buffer.Array[ buffer.Offset ] << 8 ) | ( buffer.Array[ buffer.Offset + 1 ] );

            result = new H265NalUnit();

            result.TemporalId    = (byte)        ( ( header      ) & 0x07 );
            result.LayerId       = (byte)        ( ( header >> 3 ) & 0x1F );
            result.LayerId      |= (byte)        ( ( header >> 8 ) & 0x01 );
            result.Type          = (NalUnitType) ( ( header >> 9 ) & 0x3F );
            result.ForbiddenBit  = (byte)        ( ( header >> 15) & 0x1  ) == 1;

            result.Data = new ArraySegment<byte>(  buffer.Array , buffer.Offset + 2 , buffer.Count - 2 );
            
            return true;
        }
    } 
}