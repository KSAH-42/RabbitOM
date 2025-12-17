using System;

namespace RabbitOM.Streaming.Net.Rtp.H265.Headers
{
    public struct NalUnitFragmentation
    {
        public bool StartBit { get; private set; }
        public bool StopBit { get; private set; }
        public NalUnitType FragmentedType { get; private set; }
        public ArraySegment<byte> Payload { get; private set; }
        





        public static bool IsStartPacket( ref NalUnitFragmentation nalUnit )
            => nalUnit.StartBit && ! nalUnit.StopBit;

        public static bool IsStopPacket( ref NalUnitFragmentation nalUnit )
            => ! nalUnit.StartBit && nalUnit.StopBit;

        public static bool IsDataPacket( ref NalUnitFragmentation nalUnit )
            => ! nalUnit.StartBit && ! nalUnit.StopBit;
        





        // https://datatracker.ietf.org/doc/html/rfc7798#section-4.4.3
        // header - 1
        //  +---------------+---------------+
        //  |7|6|5|4|3|2|1|0|7|6|5|4|3|2|1|0|
        //  +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
        //  |F|   Type    |  LayerId  | TID |
        //  +-------------+-----------------+

        // header
        //  +---------------+
        //  |7|6|5|4|3|2|1|0|
        //  +-+-+-+-+-+-+-+-+
        //  |S|E|  FuType   |
        //  +---------------+

        //  [payload........]
        public static bool TryParse( ArraySegment<byte> buffer , out NalUnitFragmentation result )
        {
            result = default;

            if ( buffer.Count < 3 )
            {
                return false;
            }
            
            var header = buffer.Array[ buffer.Offset + 2 ];

            result = new NalUnitFragmentation();

            result.StartBit       = ( header >> 7 & 0x1 ) == 1;
            result.StopBit        = ( header >> 6 & 0x1 ) == 1;
            result.FragmentedType = (NalUnitType) ( header & 0x3F );
    
            if ( buffer.Count > 3 )
            {
                result.Payload = new ArraySegment<byte>( buffer.Array , buffer.Offset + 3 , buffer.Array.Length - (buffer.Offset + 3) );
            }

            return true;
        }

        public static int ParseHeader( ArraySegment<byte> buffer )
        {
            if ( buffer.Count < 3 )
            {
                throw new ArgumentNullException( nameof( buffer ) );
            }

            var result = 0x81FF & ( buffer.Array[ buffer.Offset ] << 8 ) | ( buffer.Array[ buffer.Offset + 1 ] );

            return result |= ( (byte) ( buffer.Array[ buffer.Offset + 2 ] & 0x3F ) << 9 );
        }
    } 
}