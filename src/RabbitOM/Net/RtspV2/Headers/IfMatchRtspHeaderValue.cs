using System;
using System.Linq;

namespace RabbitOM.Net.RtspV2.Headers
{
    using RabbitOM.Net.RtspV2.Headers.DataTypes;

    public sealed class IfMatchRtspHeaderValue
    {
        public RtspHeaderValueCollection<string> ETags { get; } = new RtspHeaderValueCollection<string>( RtspHeaderValueValidator.IsWellFormed );

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
