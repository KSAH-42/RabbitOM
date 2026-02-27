using System;
using System.Globalization;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class ScaleRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "Scale";



        
        public float Value { get; set; }




        public override string ToString()
        {
            return Value.ToString( "G2" , CultureInfo.InvariantCulture );
        }





        public static bool TryParse( string input , out ScaleRtspHeaderValue result )
        {
            result = float.TryParse( StringRtspHeaderNormalizer.Normalize( input ) , NumberStyles.Any , CultureInfo.InvariantCulture , out var value )
                ? new ScaleRtspHeaderValue() { Value = value }
                : null
                ;

            return result != null;
        }
    }
}
