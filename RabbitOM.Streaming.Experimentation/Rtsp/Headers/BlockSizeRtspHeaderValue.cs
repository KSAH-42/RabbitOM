using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class BlockSizeRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "Blocksize";
        
        
        
        
        
        public long Value { get; set; }


        
        
        
        
        public override string ToString()
        {
            return Value.ToString();
        }



        
        public static bool TryParse( string input , out BlockSizeRtspHeaderValue result )
        {
            result = long.TryParse( StringRtspHeaderNormalizer.Normalize( input ) , out var value )
                ? new BlockSizeRtspHeaderValue() { Value = value }
                : null
                ;

            return result != null;
        }
    }
}
