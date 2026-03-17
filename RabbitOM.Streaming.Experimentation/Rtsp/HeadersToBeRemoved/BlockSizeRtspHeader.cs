using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.HeadersToBeRemoved
{
    using RabbitOM.Streaming.Experimentation.Rtsp.HeadersToBeRemoved.Adapters;

    public sealed class BlockSizeRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "Blocksize";        

        public ushort Value { get; set; }

        public static bool TryParse( string input , out BlockSizeRtspHeader result )
        {
            result = ushort.TryParse( StringValueAdapter.TrimWithUnQuoteAdapter.Adapt( input ) , out var value ) ? new BlockSizeRtspHeader() { Value = value } : null ;

            return result != null;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
