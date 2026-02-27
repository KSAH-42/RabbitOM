using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class SpeedRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "Speed";



        
        public long Value { get; set; }




        public override string ToString()
        {
            return Value.ToString();
        }





        public static bool TryParse( string input , out BandwithRtspHeaderValue result )
        {
            result = long.TryParse( StringRtspHeaderNormalizer.Normalize( input ) , out var value )
                ? new BandwithRtspHeaderValue() { Value = value }
                : null
                ;

            return result != null;
        }
    }
}
