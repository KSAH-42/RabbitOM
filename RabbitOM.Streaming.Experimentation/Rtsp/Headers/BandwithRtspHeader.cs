using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class BandwithRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "Bandwith";
       
        
        public long Value { get; set; }
       
        
        public override string ToString()
        {
            return Value.ToString();
        }

                
        public static bool TryParse( string input , out BandwithRtspHeader result )
        {
            result = RtspHeaderParser.TryParse( RtspHeaderParser.Formatter.Filter( input ) , out long value )
                ? new BandwithRtspHeader() { Value = value }
                : null
                ;

            return result != null;
        }
    }
}
