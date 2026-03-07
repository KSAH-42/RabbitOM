using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters;

    public sealed class BlockSizeRtspHeader
    {
        public static readonly string TypeName = "Blocksize";        

        public long Value { get; set; }

        public static bool TryParse( string input , out BlockSizeRtspHeader result )
        {
            result = long.TryParse( StringValueAdapter.TrimWithUnQuoteAdapter.Adapt( input ) , out long value ) ? new BlockSizeRtspHeader() { Value = value } : null ;

            return result != null;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
