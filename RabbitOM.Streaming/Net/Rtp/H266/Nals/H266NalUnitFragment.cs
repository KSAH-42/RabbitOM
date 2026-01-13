using System;

namespace RabbitOM.Streaming.Net.Rtp.H266.Nals
{
    public struct H266NalUnitFragment
    {
        public bool StartBit { get; private set; }

        public bool StopBit { get; private set; }

        public H266NalUnitType FragmentationType { get; private set; }

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

            var header = buffer.Array[ buffer.Offset + 2 ];

            result = new H266NalUnitFragment();

            result.FragmentationType = (H266NalUnitType) ( header & 0x1F );

            result.StartBit = ( ( header >> 7 ) & 0x1 ) == 1;
            result.StopBit  = ( ( header >> 6 ) & 0x1 ) == 1;

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

        public static int ParseHeader( in ArraySegment<byte> buffer )
        {
            throw new NotImplementedException();
        }
    }           
}
