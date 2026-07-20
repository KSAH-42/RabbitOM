using System;

namespace RabbitOM.Streaming.RtspV2.Headers
{
    public sealed class BlockSizeRtspHeaderValue
    {
        public ushort Value { get; set; }

        public static bool TryParse( string input , out BlockSizeRtspHeaderValue result )
        {
            result = ushort.TryParse( RtspHeaderValueSanitizer.UnQuotesWithTrim( input ) , out var value ) ? new BlockSizeRtspHeaderValue() { Value = value } : null ;

            return result != null;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
