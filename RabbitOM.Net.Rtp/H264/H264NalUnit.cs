/*
 EXPERIMENTATION of the next implementation of the rtp layer

              O P T I M I Z A T I O N S

 For next implementation, optimize can to reduce copy 
 by using array segment to remove Buffer.Copy or similar methods

*/

using System;

namespace RabbitOM.Net.Rtsp.Tests
{
    public sealed class H264NalUnit
    {
        private static int DefaultMinimuLength = 4;
        private static byte[] StartPrefix_001 = new byte[] { 0 , 0 , 1 };
        private static byte[] StartPrefix_0001 = new byte[] { 0 , 0 , 0 , 1 };

        public bool ForbiddenBit { get; private set; }
        public byte NRI { get; private set; }
        public byte Type { get; private set; }
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
            => Payload != null && Payload.Length >= 1;

        // Time complexity O(N) 
        // TODO: add parsing tests
        // TODO: add tests for protocol violations

        public static bool TryParse( byte[] buffer , out H264NalUnit result )
        {
            result = default;

            if ( buffer == null || buffer.Length < DefaultMinimuLength )
                return false;

            /*
                +----------------------------------+
                | Start Code Prefix (3 or 4 bytes) |
                +----------------------------------+
             */

            int offset = Sum( buffer , StartPrefix_0001.Length ) == 1 ? 3
                       : Sum( buffer , StartPrefix_001 .Length ) == 1 ? 2 
                       : -1;

            if ( offset < 0 )
                return false;

            /*
                +------------------------------------------+
                | NAL Unit Header (Variable size)          |
                +------------------------------------------+
                | Forbidden Zero Bit | NRI | NAL Unit Type |
                +------------------------------------------+
             */

            result = new H264NalUnit();

            result.ForbiddenBit           = (buffer[ ++offset ] & 0x1) == 1;
            result.NRI                    = (byte) ( ( buffer[ offset ] >> 1 ) & 0x2 );
            result.Type                   = (byte) ( ( buffer[ offset ] >> 1 ) & 0x1F );

            result.IsReserved            |= result.Type == 0;
            result.IsSingle               = result.Type >= 1 && result.Type <= 23;
            result.IsSTAP_A               = result.Type == 24;
            result.IsSTAP_B               = result.Type == 25;
            result.IsMTAP_A               = result.Type == 26;
            result.IsMTAP_B               = result.Type == 27;
            result.IsFU_A                 = result.Type == 28;
            result.IsFU_B                 = result.Type == 29;
            result.IsReserved            |= result.Type == 30;
            result.IsReserved            |= result.Type == 31;

            result.IsCodedSliceNIDR       = result.Type == 1;
            result.IsCodedSlicePartitionA = result.Type == 2;
            result.IsCodedSlicePartitionB = result.Type == 3;
            result.IsCodedSlicePartitionC = result.Type == 4;
            result.IsCodedSliceIDR        = result.Type == 5;
            result.IsCodedSliceIDR        = result.Type == 6;
            result.IsSPS                  = result.Type == 7;
            result.IsPPS                  = result.Type == 8;
            result.IsAccessDelimiter      = result.Type == 9;

            /*
                +----------------------------------+
                | Raw Byte Sequence Payload        |
                +----------------------------------+
             */

            result.Payload = new byte[ buffer.Length - ++ offset ];

            Buffer.BlockCopy( buffer , offset , result.Payload , 0 , result.Payload.Length );

            return true;
        }

        public static ulong Sum( byte[] buffer , int count )
        {
            ulong sum = 0;

            count = count > buffer.Length ? buffer.Length : count ;

            while ( -- count >= 0 )
                sum += buffer[ count ];

            return sum;
        }
    } 
}