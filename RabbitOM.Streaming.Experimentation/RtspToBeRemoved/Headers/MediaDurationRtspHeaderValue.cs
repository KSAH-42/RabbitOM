using RabbitOM.Streaming.Experimentation.RtspToBeRemoved.Headers.Normalizers;
using System;
using System.Globalization;

namespace RabbitOM.Streaming.Experimentation.RtspToBeRemoved.Headers
{
    public sealed class MediaDurationRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "Media-Duration";

        public double Value { get; set; }

        public static bool TryParse( string input , out MediaDurationRtspHeaderValue result )
        {
            result = double.TryParse( StringValueNormalizer.TrimWithUnQuoteNormalizer.Normalize( input ).Replace( "," , "." ) , NumberStyles.Float , CultureInfo.InvariantCulture , out var value ) ? new MediaDurationRtspHeaderValue() { Value = value } : null;

            return result != null;
        }

        public override string ToString()
        {
            return Value.ToString( "F3" , CultureInfo.InvariantCulture );
        }
    }
}
