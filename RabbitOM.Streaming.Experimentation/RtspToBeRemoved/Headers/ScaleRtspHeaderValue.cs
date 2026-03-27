using RabbitOM.Streaming.Experimentation.RtspToBeRemoved.Headers.Normalizers;
using System;
using System.Globalization;

namespace RabbitOM.Streaming.Experimentation.RtspToBeRemoved.Headers
{
    public sealed class ScaleRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "Scale";
        
        public float Value { get; set; }

        public static bool TryParse( string input , out ScaleRtspHeaderValue result )
        {
            result = float.TryParse( StringValueNormalizer.TrimWithUnQuoteNormalizer.Normalize( input ).Replace( "," , "." ) , NumberStyles.Float , CultureInfo.InvariantCulture , out var value ) ? new ScaleRtspHeaderValue() { Value = value } : null ;

            return result != null;
        }

        public override string ToString()
        {
            return Value.ToString( "G2" , CultureInfo.InvariantCulture );
        }
    }
}
