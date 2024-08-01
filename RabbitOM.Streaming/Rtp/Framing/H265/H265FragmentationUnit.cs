using System;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    public struct H265FragmentationUnit
    {
        public bool StartBit { get; set; }
        
        public bool EndBit { get; set; }
        
        public byte Type { get; set; }

        public ArraySegment<byte> Data { get; set; }




        public bool TryValidate()
        {
            return Data.Count >= 1;
        }




        public static bool TryParse( ArraySegment<byte> buffer , out H265FragmentationUnit result )
        {
            result = default;

            if ( buffer.Count <= 1 )
            {
                return false;
            }

            result = new H265FragmentationUnit();

            result.StartBit = ( ( buffer.Array[ buffer.Offset ] >> 7 ) & 0x1 ) == 1;
            result.EndBit   = ( ( buffer.Array[ buffer.Offset ] >> 6 ) & 0x1 ) == 1;

            result.Type     = (byte) ( buffer.Array[ buffer.Offset ] & 0x3F );

            result.Data     = new ArraySegment<byte>( buffer.Array , buffer.Offset + 1 , buffer.Count - 1 );

            return true;
        }
    } 
}