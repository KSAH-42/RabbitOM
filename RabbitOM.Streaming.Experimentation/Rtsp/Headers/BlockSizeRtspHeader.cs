using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Parsers;

    public sealed class BlockSizeRtspHeader : RtspHeader
    {
        public static string TypeName { get; } = "Blocksize";
        

        public long Value { get; set; }

        
        public override string ToString()
        {
            return Value.ToString();
        }

        
        public static bool TryParse( string input , out BlockSizeRtspHeader result )
        {
            result = LongRtspHeaderParser.TryParse( input , out long value ) ? new BlockSizeRtspHeader() { Value = value } : null ;

            return result != null;
        }
    }
}
