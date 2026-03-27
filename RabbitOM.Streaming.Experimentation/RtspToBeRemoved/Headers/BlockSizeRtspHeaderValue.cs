using System;

namespace RabbitOM.Streaming.Experimentation.RtspToBeRemoved.Headers
{
    using RabbitOM.Streaming.Experimentation.RtspToBeRemoved.Headers.Normalizers;

    public sealed class BlockSizeRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "Blocksize";        

        public ushort Value { get; set; }

        public static bool TryParse( string input , out BlockSizeRtspHeaderValue result )
        {
            result = ushort.TryParse( StringValueNormalizer.TrimWithUnQuoteNormalizer.Normalize( input ) , out var value ) ? new BlockSizeRtspHeaderValue() { Value = value } : null ;

            return result != null;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
