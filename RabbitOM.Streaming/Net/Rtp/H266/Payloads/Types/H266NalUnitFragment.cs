using System;

namespace RabbitOM.Streaming.Net.Rtp.H266.Payloads.Types
{
    public struct H266NalUnitFragment
    {
        public bool ForbiddenBit { get; private set; }

        public bool StartBit { get; private set; }

        public bool StopBit { get; private set; }

        public byte FragmentationType { get; private set; }

        public ArraySegment<byte> Payload { get; private set; }






        public static bool IsStartPacket( in H266NalUnitFragment nalu )
            => nalu.StartBit && ! nalu.StopBit;

        public static bool IsStopPacket( in H266NalUnitFragment nalu )
            => ! nalu.StartBit && nalu.StopBit;

        public static bool IsDataPacket( in H266NalUnitFragment nalu )
            => ! nalu.StartBit && ! nalu.StopBit;


        



        public static bool TryParse( in ArraySegment<byte> buffer , bool donl , out H266NalUnitFragment result )
        {
            result = default;

            if ( buffer.Array == null || buffer.Count < 3 )
            {
                return false;
            }

            result = new H266NalUnitFragment();

            result.ForbiddenBit = ( ( buffer.Array[ buffer.Offset     ] >> 7 ) & 0x1 ) == 1;
            result.StartBit     = ( ( buffer.Array[ buffer.Offset + 2 ] >> 7 ) & 0x1 ) == 1;
            result.StopBit      = ( ( buffer.Array[ buffer.Offset + 2 ] >> 6 ) & 0x1 ) == 1;

            result.FragmentationType = (byte) ( buffer.Array[ buffer.Offset + 2 ] & 0x1F );

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

        public static int ReContructHeader( in ArraySegment<byte> buffer )
        {
            if ( buffer.Array == null || buffer.Count < 3 )
            {
                return 0;
            }

            var result = 0xFF07 & ( buffer.Array[ buffer.Offset ] << 8 ) | ( buffer.Array[ buffer.Offset + 1 ] );

            return result |= ( (byte) ( buffer.Array[ buffer.Offset + 2 ] & 0x1F ) << 3 );
        }
    }           
}
