using System;

namespace RabbitOM.Streaming.Net.Rtp.H264.Packets.Entities
{
    public struct H264NalUnitFragmentA
    {
        public bool ForbiddenBit { get; private set; }
        public byte Nri { get; private set; }
        public bool StartBit { get; private set; }
        public bool StopBit { get; private set; }
        public byte FragmentedType { get; private set; }
        public ArraySegment<byte> Payload { get; private set; }
        



        public static bool IsStartPacket( in H264NalUnitFragmentA nalUnit )
            => nalUnit.StartBit && ! nalUnit.StopBit;

        public static bool IsStopPacket( in H264NalUnitFragmentA nalUnit )
            => ! nalUnit.StartBit && nalUnit.StopBit;

        public static bool IsDataPacket( in H264NalUnitFragmentA nalUnit )
            => ! nalUnit.StartBit && ! nalUnit.StopBit;





        public static bool TryParse( in ArraySegment<byte> buffer , out H264NalUnitFragmentA result )
        {
            result = default;

            if ( buffer.Count < 2 )
            {
                return false;
            }
            
            var header = buffer.Array[ buffer.Offset + 1 ];

            result = new H264NalUnitFragmentA();

            result.ForbiddenBit   = (byte) ( ( buffer.Array[ buffer.Offset ] >> 7 ) & 0x01 ) == 1;
            result.Nri            = (byte) ( ( buffer.Array[ buffer.Offset ] >> 5 ) & 0x03 );
            result.StartBit       =        ( buffer.Array[ buffer.Offset + 1 ] >> 7 & 0x1 ) == 1;
            result.StopBit        =        ( buffer.Array[ buffer.Offset + 1 ] >> 6 & 0x1 ) == 1;
            result.FragmentedType = (byte) ( buffer.Array[ buffer.Offset + 1 ] & 0x1F );

            if ( buffer.Count > 2 )
            {
                result.Payload = new ArraySegment<byte>( buffer.Array , buffer.Offset + 2 , buffer.Count - 2 );
            }

            return true;
        }

        public static byte ReConstructHeader( in ArraySegment<byte> buffer )
        {
            if ( buffer.Count < 2 )
            {
                throw new ArgumentOutOfRangeException( nameof( buffer ) );
            }

            byte result = 0;

            result += (byte) (buffer.Array[ buffer.Offset ] & 0x80);
            result += (byte) (buffer.Array[ buffer.Offset ] & 0x60);
            result += (byte) (buffer.Array[ buffer.Offset + 1 ] & 0x1F);

            return result;
        }
    } 
}