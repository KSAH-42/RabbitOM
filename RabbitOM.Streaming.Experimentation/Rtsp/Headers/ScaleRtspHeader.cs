using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters;
using System;
using System.Globalization;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class ScaleRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "Scale";
        
        public float Value { get; set; }

        public static bool TryParse( string input , out ScaleRtspHeader result )
        {
            result = float.TryParse( StringValueAdapter.TrimWithUnQuoteAdapter.Adapt( input ).Replace( "," , "." ) , NumberStyles.Float , CultureInfo.InvariantCulture , out var value ) ? new ScaleRtspHeader() { Value = value } : null ;

            return result != null;
        }

        public override string ToString()
        {
            return Value.ToString( "G2" , CultureInfo.InvariantCulture );
        }
    }
}
