using System;

namespace RabbitOM.Streaming.Net.Rtp.H264.Headers
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
        





        public static bool TryParse( ArraySegment<byte> buffer , out NalUnitFragmentation result )
        {
            result = default;

            if ( buffer.Count < 2 )
            {
                return false;
            }
            
            var header = buffer.Array[ buffer.Offset + 1 ];

            result = new NalUnitFragmentation();

            result.StartBit       = ( header >> 7 & 0x1 ) == 1;
            result.StopBit        = ( header >> 6 & 0x1 ) == 1;
            result.FragmentedType = (NalUnitType) ( header & 0x1F );
    
            if ( buffer.Count > 2 )
            {
                result.Payload = new ArraySegment<byte>( buffer.Array , buffer.Offset + 2 , buffer.Array.Length - (buffer.Offset + 2) );
            }

            return true;
        }

        public static int ParseHeader( ArraySegment<byte> buffer )
        {
            throw new NotImplementedException();
        }
    } 
}