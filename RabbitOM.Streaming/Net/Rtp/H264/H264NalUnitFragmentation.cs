using System;
using System.Runtime.Remoting.Messaging;

namespace RabbitOM.Streaming.Net.Rtp.H264
{
    public struct H264NalUnitFragmentation
    {
        public bool StartBit { get; private set; }
        public bool StopBit { get; private set; }
        public H264NalUnitType FragmentedType { get; private set; }
        public ArraySegment<byte> Payload { get; private set; }
        





        public static bool IsStartPacket( ref H264NalUnitFragmentation nalUnit )
            => nalUnit.StartBit && ! nalUnit.StopBit;

        public static bool IsStopPacket( ref H264NalUnitFragmentation nalUnit )
            => ! nalUnit.StartBit && nalUnit.StopBit;

        public static bool IsDataPacket( ref H264NalUnitFragmentation nalUnit )
            => ! nalUnit.StartBit && ! nalUnit.StopBit;
        





        public static bool TryParse( ArraySegment<byte> buffer , out H264NalUnitFragmentation result )
        {
            result = default;

            if ( buffer.Count < 2 )
            {
                return false;
            }
            
            var header = buffer.Array[ buffer.Offset + 1 ];

            result = new H264NalUnitFragmentation();

            result.StartBit       = ( header >> 7 & 0x1 ) == 1;
            result.StopBit        = ( header >> 6 & 0x1 ) == 1;
            result.FragmentedType = (H264NalUnitType) ( header & 0x1F );

            if ( buffer.Count > 2 )
            {
                result.Payload = new ArraySegment<byte>( buffer.Array , buffer.Offset + 2 , buffer.Array.Length - (buffer.Offset + 2) );
            }

            return true;
        }

        public static int ParseHeader( ArraySegment<byte> buffer )
        {
            if ( buffer.Count < 2 )
            {
                throw new ArgumentOutOfRangeException( nameof( buffer ) );
            }

            var header = ( buffer.Array[ buffer.Offset ] << 8 ) | ( buffer.Array[ buffer.Offset + 1 ] );

            return ( ( header >> 9 ) & 0x3F );
        }
    } 
}