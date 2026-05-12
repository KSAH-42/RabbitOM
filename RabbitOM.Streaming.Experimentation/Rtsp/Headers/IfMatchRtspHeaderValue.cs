using System;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.DataTypes;
    
    public sealed class IfMatchRtspHeaderValue
    {
        public StringRtspHeaderValueCollection ETags { get; } = new StringRtspHeaderValueCollection();

        public static bool TryParse( string input , out IfMatchRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , "," , out string[] tokens ) )
            {
                var header = new IfMatchRtspHeaderValue();

                foreach ( var token in tokens )
                {
                    header.ETags.TryAdd( RtspHeaderValueSanitizer.UnQuotesWithTrim( token ) );
                }
            
                if ( header.ETags.Count > 0 )
                {
                    result = header;
                }
            }

            return result != null;
        }

        public override string ToString()
        {
            return string.Join( ", " , ETags.Select( element => $"\"{element}\"" ) );
        }
    }
}
