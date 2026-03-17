using RabbitOM.Streaming.Experimentation.Rtsp.HeadersToBeRemoved.Adapters;
using System;
using System.Globalization;

namespace RabbitOM.Streaming.Experimentation.Rtsp.HeadersToBeRemoved
{
    public sealed class MediaDurationRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "Media-Duration";

        public double Value { get; set; }

        public static bool TryParse( string input , out MediaDurationRtspHeader result )
        {
            result = double.TryParse( StringValueAdapter.TrimWithUnQuoteAdapter.Adapt( input ).Replace( "," , "." ) , NumberStyles.Float , CultureInfo.InvariantCulture , out var value ) ? new MediaDurationRtspHeader() { Value = value } : null;

            return result != null;
        }

        public override string ToString()
        {
            return Value.ToString( "F3" , CultureInfo.InvariantCulture );
        }
    }
}
