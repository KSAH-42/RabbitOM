/*
 EXPERIMENTATION of the next implementation of the rtp layer

              O P T I M I Z A T I O N S

 Reduce copy by using array segment to remove Buffer.Copy or similar methods

*/

using System;

namespace RabbitOM.Net.Rtp.H265
{
    public sealed class H265NalUnit
    {
        private static int DefaultMinimuLength = 4;

        private static readonly StartPrefix StartPrefixA = new StartPrefix( new byte[] { 0 , 0 , 1 } );
        private static readonly StartPrefix StartPrefixB = new StartPrefix( new byte[] { 0 , 0 , 0 , 1 } );

        private H265NalUnit() { }

        public bool ForbiddenBit { get; private set; }
        public byte Type { get; private set; }
        public byte LayerId { get; private set; }
        public byte TID { get; private set; }
        public byte[] Payload { get; private set; } 


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

            if ( buffer == null || buffer.Length < DefaultMinimuLength )
            {
                return false;
            }

            /*
                +----------------------------------+
                | Start Code Prefix (3 or 4 bytes) |
                +----------------------------------+
             */

            int index = StartPrefix.StartsWith( buffer , StartPrefixA ) ? StartPrefixA.Values.Length
                      : StartPrefix.StartsWith( buffer , StartPrefixB ) ? StartPrefixB.Values.Length
                      : -1;

            if ( index < 0 )
            {
                return false;
            }

            /*
                +---------------------------------------------
                | NAL Unit Header (Variable size)             |
                +---------------------------------------------+
                | Forbidden Zero Bit | Type | Layer Id | TID  |
                +---------------------------------------------+
             */

            result = new H265NalUnit();

            result.ForbiddenBit           = (byte) ( ( buffer[ index ] >> 7 ) & 0x1 ) == 1;
            result.Type                   = (byte) ( ( buffer[ index ] >> 1 ) & 0x3F );
            result.LayerId                = (byte) ( ( buffer[ index ] << 7 ) & 0x80 );
            result.LayerId               |= (byte) ( ( buffer[ index ] >> 3 ) & 0x1F );
            // result.TID = ...

            throw new NotImplementedException();
        }
    } 
}