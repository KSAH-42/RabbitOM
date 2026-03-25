using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Normalizers;

    public sealed class CSeqRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "CSeq";

        public CSeqRtspHeaderValue() { }
        public CSeqRtspHeaderValue(uint value ) => Value = value;

        public uint Value { get; set; }

        public static implicit operator CSeqRtspHeaderValue( uint value )
        {
            return new CSeqRtspHeaderValue( value );
        }

        public static bool TryParse( string input , out CSeqRtspHeaderValue result )
        {
            result = uint.TryParse( StringValueNormalizer.TrimWithUnQuoteNormalizer.Normalize( input ) , out var value ) ? new CSeqRtspHeaderValue() { Value = value } : null;
            
            return result != null;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
