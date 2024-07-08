using System;

namespace RabbitOM.Net.Rtp.H265
{
    public sealed class H265NalUnit
    {
        private static int DefaultMinimumLength = 5;
   



        private H265NalUnit() { }




        public bool ForbiddenBit { get; private set; }
        public byte Type { get; private set; }
        public byte LayerId { get; private set; }
        public byte TID { get; private set; }
        public byte[] Payload { get; private set; } 
        public byte[] Prefix { get; private set; }






        public bool TryValidate()
        {
            return Payload != null && Payload.Length >= 1;
        }








        // TODO: add parsing tests
        // TODO: add tests for protocol violations
        // Time complexity O(N)   

        public static bool TryParse( byte[] buffer , out H265NalUnit result )
        {
            result = default;

            if ( buffer == null || buffer.Length < DefaultMinimumLength )
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

            result.ForbiddenBit  = (byte) ( ( buffer[ index ] >> 7 ) & 0x1  ) == 1;
            result.Type          = (byte) ( ( buffer[ index ] >> 1 ) & 0x3F );
            result.LayerId       = (byte) ( ( buffer[ index ] << 7 ) & 0x80 );
            result.LayerId      |= (byte) ( ( buffer[ index ] >> 3 ) & 0x1F );
            result.TID           = (byte) ( ( buffer[ index ]      ) & 0x03 );

            result.Payload = new byte[ buffer.Length - ++ index ];

            Buffer.BlockCopy( buffer , index , result.Payload , 0 , result.Payload.Length );

            return true;
        }
    } 
}