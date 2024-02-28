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

        private static readonly H265StartPrefix StartPrefixA = new H265StartPrefix( new byte[] { 0 , 0 , 1 } );
        private static readonly H265StartPrefix StartPrefixB = new H265StartPrefix( new byte[] { 0 , 0 , 0 , 1 } );

        private H265NalUnit() { }

        public bool ForbiddenBit { get; private set; }
        public byte NRI { get; private set; }
        public byte Type { get; private set; }
        public byte LayerId { get; private set; }
        public byte TID { get; private set; }
        public bool IsReserved { get; private set; }
        public bool IsSingle { get; private set; }
        public bool IsSTAP_A { get; private set; }
        public bool IsSTAP_B { get; private set; }
        public bool IsMTAP_A { get; private set; }
        public bool IsMTAP_B { get; private set; }
        public bool IsFU_A { get; private set; }
        public bool IsFU_B { get; private set; }
        public bool IsCodedSliceNIDR { get; private set; }
        public bool IsCodedSlicePartitionA { get; private set; }
        public bool IsCodedSlicePartitionB { get; private set; }
        public bool IsCodedSlicePartitionC { get; private set; }
        public bool IsCodedSliceIDR { get; private set; }
        public bool IsSEI { get; private set; }
        public bool IsSPS { get; private set; }
        public bool IsPPS { get; private set; }
        public bool IsAccessDelimiter { get; private set; }
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

            int index = H265StartPrefix.StartsWith( buffer , StartPrefixA ) ? StartPrefixA.Values.Length
                      : H265StartPrefix.StartsWith( buffer , StartPrefixB ) ? StartPrefixB.Values.Length
                      : -1;

            if ( index < 0 )
            {
                return false;
            }

            /*
                +------------------------------------------+
                | NAL Unit Header (Variable size)          |
                +------------------------------------------+
                | Forbidden Zero Bit | NRI | NAL Unit Type |
                +------------------------------------------+
             */

            result = new H265NalUnit();

            result.ForbiddenBit           = (buffer[ ++index ] & 0x1) == 1;
            result.NRI                    = (byte) ( ( buffer[ index ] >> 1 ) & 0x2  );
            result.Type                   = (byte) ( ( buffer[ index ] >> 1 ) & 0x3F );
           
            throw new NotImplementedException();
        }
    } 
}