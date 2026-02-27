using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
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
            result = long.TryParse( StringRtspHeaderNormalizer.Normalize( input ) , out var value )
                ? new BlockSizeRtspHeader() { Value = value }
                : null
                ;

            return result != null;
        }
    }
}
