using System;
using System.Globalization;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters;

    public sealed class FloatRtspHeaderValue
    {
        public float Value { get; set; }

        public static bool TryParse( string input , out FloatRtspHeaderValue result )
        {
            result = float.TryParse( StringValueAdapter.TrimWithUnQuoteAdapter.Adapt( input ).Replace( "," , "." ) , NumberStyles.Float , CultureInfo.InvariantCulture , out var value ) ? new FloatRtspHeaderValue() { Value = value } : null ;

            return result != null;
        }

        public override string ToString()
        {
            return Value.ToString( "G2" , CultureInfo.InvariantCulture );
        }
    }
}
