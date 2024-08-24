using System;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    public struct FragmentationUnit
    {
        public bool Indicator { get; set; }

        public bool StartBit { get; set; }
        
        public bool EndBit { get; set; }
        
        public NalUnitType Type { get; set; }

        public ArraySegment<byte> Data { get; set; }




        public bool TryValidate()
        {
            return Data.Count >= 1;
        }


        
        // TODO: to be tested

        public static bool TryParse( ArraySegment<byte> buffer , out FragmentationUnit result )
        {
            result = default;

            if ( buffer.Count <= 2 )
            {
                return false;
            }

            result = new FragmentationUnit();

            result.Indicator =   ( buffer.Array[ buffer.Offset ] & 0x01 ) == 1;
            result.StartBit  = ( ( buffer.Array[ buffer.Offset + 1 ] >> 7 ) & 0x1 ) == 1;
            result.EndBit    = ( ( buffer.Array[ buffer.Offset + 1 ] >> 6 ) & 0x1 ) == 1;

            result.Type     = (NalUnitType) ( buffer.Array[ buffer.Offset + 1 ] & 0x3F );

            result.Data     = new ArraySegment<byte>( buffer.Array , buffer.Offset + 2 , buffer.Count - 1 );

            return true;
        }
    } 
}