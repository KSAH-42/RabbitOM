using System;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    public sealed class H265NalUnit
    {
        private static int DefaultMinimumLength = 5;
   



        private H265NalUnit() { }




        public bool ForbiddenBit { get; private set; }
        public byte Type { get; private set; }
        public byte LayerId { get; private set; }
        public byte TID { get; private set; }
        public ArraySegment<byte> Payload { get; private set; } 
        public byte[] Prefix { get; private set; }






        public bool TryValidate()
        {
            return Payload != null && Payload.Count >= 1;
        }








        // TODO: add parsing tests
        // TODO: add tests for protocol violations
        // Time complexity O(N)   

        // TODO: Not actually tested

        public static bool TryParse( ArraySegment<byte> buffer , out H265NalUnit result )
        {
            result = default;

            if ( buffer.Count < DefaultMinimumLength )
            {
                return false;
            }

            /*
                +----------------------------------+
                | Start Code Prefix (3 or 4 bytes) |
                +----------------------------------+
             */

            var prefix = StartPrefix.StartsWith( buffer , StartPrefix.StartPrefixS4 ) ? StartPrefix.StartPrefixS4
                       : StartPrefix.StartsWith( buffer , StartPrefix.StartPrefixS3 ) ? StartPrefix.StartPrefixS3
                       : StartPrefix.Null
                       ;

            int index = prefix.Values.Length;

            /*
                +---------------------------------------------
                | NAL Unit Header (Variable size)             |
                +---------------------------------------------+
                | Forbidden Zero Bit | Type | Layer Id | TID  |
                +---------------------------------------------+
             */

            result = new H265NalUnit()
            {
                Prefix = prefix.Values,
            };

            result.ForbiddenBit  = (byte) ( ( buffer.Array[ buffer.Offset + index ] >> 7 ) & 0x1  ) == 1;
            result.Type          = (byte) ( ( buffer.Array[ buffer.Offset + index ] >> 1 ) & 0x3F );
            result.LayerId       = (byte) ( ( buffer.Array[ buffer.Offset + index ] << 7 ) & 0x80 );
            result.LayerId      |= (byte) ( ( buffer.Array[ buffer.Offset + index ] >> 3 ) & 0x1F );
            result.TID           = (byte) ( ( buffer.Array[ buffer.Offset + index ]      ) & 0x03 );

            result.Payload = new ArraySegment<byte>(  buffer.Array , buffer.Offset + ++ index , buffer.Count - index );
            
            return true;
        }
    } 
}