using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters;

    public sealed class BlockSizeRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "Blocksize";        

        public ushort Value { get; set; }

        public static bool TryParse( string input , out BlockSizeRtspHeaderValue result )
        {
            result = ushort.TryParse( StringValueAdapter.TrimWithUnQuoteAdapter.Adapt( input ) , out var value ) ? new BlockSizeRtspHeaderValue() { Value = value } : null ;

            return result != null;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
