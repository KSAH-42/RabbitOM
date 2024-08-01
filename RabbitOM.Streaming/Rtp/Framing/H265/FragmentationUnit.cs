using System;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    public struct FragmentationUnit
    {
        public static readonly FragmentationUnit Empty = new FragmentationUnit();




        public bool StartBit { get; private set; }
        
        public bool EndBit { get; private set; }
        
        public byte Type { get; private set; }

        public ArraySegment<byte> Data { get; private set; }




        public bool TryValidate()
        {
            return Data.Count >= 1;
        }


        

        public static bool TryParse( ArraySegment<byte> buffer , out FragmentationUnit result )
        {
            result = default;

            if ( buffer.Count <= 1 )
            {
                return false;
            }

            result = new FragmentationUnit();

            result.StartBit = ( ( buffer.Array[ buffer.Offset ] >> 7 ) & 0x1 ) == 1;
            result.EndBit   = ( ( buffer.Array[ buffer.Offset ] >> 6 ) & 0x1 ) == 1;

            result.Type     = (byte) ( buffer.Array[ buffer.Offset ] & 0x3F );

            result.Data     = new ArraySegment<byte>( buffer.Array , buffer.Offset + 1 , buffer.Count - 1 );

            return true;
        }
    } 
}