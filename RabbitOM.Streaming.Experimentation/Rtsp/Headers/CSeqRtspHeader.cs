using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class CSeqRtspHeader : RtspHeader 
    {
        public const string TypeName = "CSeq";
        



        public int Value
        {
            get;
            set;
        }
        



        public static bool TryParse( string input , out CSeqRtspHeader result )
        {
            result = null;

            if ( int.TryParse( StringRtspNormalizer.Normalize( input ) , out var value ) )
            {
                result = new CSeqRtspHeader() { Value = value };
            }

            return result != null;
        }
        



        public override bool TryValidate()
        {
            return Value >= 0;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
