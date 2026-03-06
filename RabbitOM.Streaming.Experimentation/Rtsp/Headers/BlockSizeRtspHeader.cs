using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Core;

    public sealed class BlockSizeRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "Blocksize";
        

        public long Value { get; set; }

        
        public override string ToString()
        {
            return Value.ToString();
        }

        
        public static bool TryParse( string input , out BlockSizeRtspHeader result )
        {
            result = RtspHeaderParser.TryParse( input , out long value ) ? new BlockSizeRtspHeader() { Value = value } : null ;

            return result != null;
        }
    }
}
