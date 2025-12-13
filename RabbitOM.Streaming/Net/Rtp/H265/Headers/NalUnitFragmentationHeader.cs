using System;

namespace RabbitOM.Streaming.Net.Rtp.H265.Headers
{
    public struct NalUnitFragmentationHeader
    {
        public static readonly NalUnitFragmentationHeader Empty = new NalUnitFragmentationHeader();






        public bool StartBit { get; private set; }
        public bool StopBit { get; private set; }
        public byte FragmentedType { get; private set; }
        public ArraySegment<byte> Payload { get; private set; }
        





        public static bool IsStartPacket( ref NalUnitFragmentationHeader packet )
            => packet.StartBit && ! packet.StopBit;

        public static bool IsStopPacket( ref NalUnitFragmentationHeader packet )
            => ! packet.StartBit && packet.StopBit;

        public static bool IsIntermediaryPacket( ref NalUnitFragmentationHeader packet )
            => ! packet.StartBit && ! packet.StopBit;
        





        // https://datatracker.ietf.org/doc/html/rfc7798#section-4.4.3
        public static bool TryParse( ArraySegment<byte> buffer , out NalUnitFragmentationHeader result )
        {
            result = default;

            if ( buffer.Count <= 2 )
            {
                return false;
            }
            
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

            var header = buffer.Array[ buffer.Offset + 2 ];

            result = new NalUnitFragmentationHeader();

            result.StartBit       = ( header >> 7 & 0x1 ) == 1;
            result.StopBit        = ( header >> 6 & 0x1 ) == 1;
            result.FragmentedType = (byte) ( header & 0x3F );

            if ( buffer.Count > 3 )
            {
                result.Payload = new ArraySegment<byte>( buffer.Array , buffer.Offset + 3 , buffer.Array.Length - (buffer.Offset + 3) );
            }

            return true;
        }
    } 
}