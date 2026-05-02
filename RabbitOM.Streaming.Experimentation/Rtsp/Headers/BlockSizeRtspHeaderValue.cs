using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types.Compliances;

    public sealed class BlockSizeRtspHeaderValue
    {
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
