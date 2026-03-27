using System;
using System.Globalization;

namespace RabbitOM.Streaming.Experimentation.RtspToBeRemoved.Headers
{
    using RabbitOM.Streaming.Experimentation.RtspToBeRemoved.Headers.Normalizers;

    public sealed class SpeedRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "Speed";
        
        public double Value { get; set; }

        public override string ToString()
        {
            return Value.ToString( "F1" , CultureInfo.InvariantCulture );
        }

        public static bool TryParse( string input , out SpeedRtspHeaderValue result )
        {
            result = double.TryParse( StringValueNormalizer.TrimWithUnQuoteNormalizer.Normalize( input ).Replace( "," , "." ) , NumberStyles.Float , CultureInfo.InvariantCulture  , out var value ) ? new SpeedRtspHeaderValue() { Value = value } : null ;

            return result != null;
        }
    }
}
