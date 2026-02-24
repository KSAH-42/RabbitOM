using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class SpeedRtspHeader
    {
        public static readonly string TypeName = "Speed";



        
        public long Value { get; set; }




        public override string ToString()
        {
            return Value.ToString();
        }





        public static bool TryParse( string input , out BandwithRtspHeader result )
        {
            result = long.TryParse( RtspHeaderValueNormalizer.Normalize( input ) , out var value )
                ? new BandwithRtspHeader() { Value = value }
                : null
                ;

            return result != null;
        }
    }
}
