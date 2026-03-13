using System;
using System.Globalization;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters;

    public sealed class SpeedRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "Speed";
        
        public double Value { get; set; }

        public override string ToString()
        {
            return Value.ToString( "F1" , CultureInfo.InvariantCulture );
        }

        public static bool TryParse( string input , out SpeedRtspHeader result )
        {
            result = double.TryParse( StringValueAdapter.TrimWithUnQuoteAdapter.Adapt( input ).Replace( "," , "." ) , NumberStyles.Float , CultureInfo.InvariantCulture  , out double value ) ? new SpeedRtspHeader() { Value = value } : null ;

            return result != null;
        }
    }
}
