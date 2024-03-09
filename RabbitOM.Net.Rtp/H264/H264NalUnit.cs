using System;
using System.Text;

namespace RabbitOM.Net.Rtp.H264
{
    public sealed class H264NalUnit
    {
        private static int DefaultMinimunLength = 4;

        
        private H264NalUnit() { }



        public bool ForbiddenBit { get; private set; }
        public byte Nri { get; private set; }
        public byte Type { get; private set; }
        public bool IsUnDefinedNri { get; private set; }
        public bool IsReserved { get; private set; }
        public bool IsSingle { get; private set; }
        public bool IsSTAP_A { get; private set; }
        public bool IsSTAP_B { get; private set; }
        public bool IsMTAP_A { get; private set; }
        public bool IsMTAP_B { get; private set; }

        public bool IsFU   { get; private set; }
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
        public bool IsSlice { get; private set; }
        public bool IsIntraFrame { get; private set; }
        public bool IsPredictiveFrame { get; private set; }
        public byte[] Prefix { get; private set; }
        public ArraySegment<byte> Buffer { get; private set; }
        public H264NalUnitPayload Payload { get; private set; }




        public bool TryValidate()
        {
            return Buffer.Array != null && Buffer.Count > 0;
        }

        public bool CanSkip()
        {
            return ForbiddenBit || IsUnDefinedNri;
        }
        
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append( $"Type:{Type} " );
            builder.Append( $"IsSlice:{IsSlice} " );
            builder.Append( $"IsFU_A:{IsFU_A} " );
            builder.Append( $"IsFU_B:{IsFU_B} " );
            builder.Append( $"IsPPS:{IsPPS} " );
            builder.Append( $"IsSPS:{IsSPS} " );

            return builder.ToString();
        }





        public static bool TryParse( byte[] buffer , out H264NalUnit result )
        {
            result = default;

            if ( buffer == null || buffer.Length < DefaultMinimunLength )
            {
                return false;
            }

            // this statement can be remove, because the payload rtp for nalunit must not include start code prefix
            // It's unnecessary normally...
            var prefix = StartPrefix.StartsWith( buffer , StartPrefix.StartPrefixS4 ) ? StartPrefix.StartPrefixS4
                       : StartPrefix.StartsWith( buffer , StartPrefix.StartPrefixS3 ) ? StartPrefix.StartPrefixS3
                       : StartPrefix.Null
                       ;

            int offset = prefix.Values.Length;
            
            result = new H264NalUnit()
            {
                Prefix       = prefix.Values,
                ForbiddenBit = (byte) ( buffer[ offset ] >> 7 & 0x1  ) == 1,
                Nri          = (byte) ( buffer[ offset ] >> 5 & 0x3  ),
                Type         = (byte) ( buffer[ offset ]      & 0x1F ),
            };

            result.IsUnDefinedNri         = result.Nri  == 0;

            // Is full nal unit ?
            result.IsSingle               = result.Type >= 1 && result.Type <= 23;
            result.IsCodedSliceNIDR       = result.Type == 1;
            result.IsCodedSlicePartitionA = result.Type == 2;
            result.IsCodedSlicePartitionB = result.Type == 3;
            result.IsCodedSlicePartitionC = result.Type == 4;
            result.IsCodedSliceIDR        = result.Type == 5;
            result.IsSEI                  = result.Type == 6;
            result.IsSPS                  = result.Type == 7;
            result.IsPPS                  = result.Type == 8;
            result.IsAccessDelimiter      = result.Type == 9;
            result.IsSTAP_A               = result.Type == 24;
            result.IsSTAP_B               = result.Type == 25;
            result.IsMTAP_A               = result.Type == 26;
            result.IsMTAP_B               = result.Type == 27;
            result.IsFU_A                 = result.Type == 28;
            result.IsFU_B                 = result.Type == 29;
            result.IsFU                   = result.Type == 28;
            result.IsReserved             = result.Type == 30;
            result.IsReserved            |= result.Type == 31;
            result.IsReserved            |= result.Type == 0;

            // Is fragmented nal unit ?
            result.IsFU                   = result.IsFU_A;
            result.IsFU                  |= result.IsFU_B;

            // Is a slice ?
            result.IsSlice                = result.IsCodedSliceIDR;
            result.IsSlice               |= result.IsCodedSliceNIDR;
            result.IsSlice               |= result.IsCodedSlicePartitionA;
            result.IsSlice               |= result.IsCodedSlicePartitionB;
            result.IsSlice               |= result.IsCodedSlicePartitionC;

            // Is a I-Frame ?
            result.IsIntraFrame           = result.IsCodedSlicePartitionA;
            result.IsIntraFrame          |= result.IsSEI;
            result.IsIntraFrame          |= result.IsSPS;
            result.IsIntraFrame          |= result.IsPPS;

            // Is a P-Frame ?
            result.IsPredictiveFrame      = result.Type == 0;
            result.IsPredictiveFrame     |= result.IsCodedSliceIDR;
            result.IsPredictiveFrame     |= result.IsCodedSlicePartitionB;
            result.IsPredictiveFrame     |= result.IsCodedSlicePartitionC;

            result.Buffer = new ArraySegment<byte>( buffer , ++ offset , buffer.Length - offset );

            result.Payload = new H264NalUnitPayload( result );

            return true;
        }
    }
}
