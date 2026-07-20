using System;

namespace RabbitOM.Streaming.Rtp.H265.Payloads.Types
{
    public struct H265NalUnitFragment
    {
        public bool ForbiddenBit { get; private set; }

        public bool StartBit { get; private set; }

        public bool StopBit { get; private set; }

        public byte FragmentedType { get; private set; }

        public ArraySegment<byte> Payload { get; private set; }






        public static bool IsStartPacket( H265NalUnitFragment nalUnit )
        {
            return nalUnit.StartBit && ! nalUnit.StopBit;
        }

        public static bool IsStopPacket( H265NalUnitFragment nalUnit )
        {
            return ! nalUnit.StartBit && nalUnit.StopBit;
        }

        public static bool IsDataPacket( H265NalUnitFragment nalUnit )
        {
            return ! nalUnit.StartBit && ! nalUnit.StopBit;
        }





        public static bool TryParse( ArraySegment<byte> buffer , bool donl , out H265NalUnitFragment result )
        {
            result = default;

            if ( buffer.Count < 3 )
            {
                return false;
            }

            result = new H265NalUnitFragment();

            result.ForbiddenBit   =        ( ( buffer.Array[ buffer.Offset + 0 ] >> 7 ) & 0x1  ) == 1;
            result.StartBit       =        ( ( buffer.Array[ buffer.Offset + 2 ] >> 7 ) & 0x1  ) == 1;
            result.StopBit        =        ( ( buffer.Array[ buffer.Offset + 2 ] >> 6 ) & 0x1  ) == 1;
            result.FragmentedType = (byte) ( ( buffer.Array[ buffer.Offset + 2 ]      ) & 0x3F );

            if ( buffer.Count > 3 )
            {
                var count = donl ? 5 : 3;

                if ( buffer.Count - count < 0 )
                {
                    return false;
                }

                result.Payload = new ArraySegment<byte>( buffer.Array , buffer.Offset + count , buffer.Count - count );
            }

            return true;
        }

        public static int ReConstructHeader( ArraySegment<byte> buffer )
        {
            if ( buffer.Count < 3 )
            {
                throw new ArgumentOutOfRangeException( nameof( buffer ) );
            }

            var result = 0x81FF & ( buffer.Array[ buffer.Offset ] << 8 ) | ( buffer.Array[ buffer.Offset + 1 ] );

            return result |= ( (byte) ( buffer.Array[ buffer.Offset + 2 ] & 0x3F ) << 9 );
        }
    }
}