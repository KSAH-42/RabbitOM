using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class BandwidthRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "Bandwidth";
       
        
        public long Value { get; set; }
       
        
        public override string ToString()
        {
            return Value.ToString();
        }

                
        public static bool TryParse( string input , out BandwidthRtspHeader result )
        {
            result = RtspHeaderParser.TryParse( RtspHeaderParser.Formatter.Filter( input ) , out long value )
                ? new BandwidthRtspHeader() { Value = value }
                : null
                ;

            return result != null;
        }
    }
}
