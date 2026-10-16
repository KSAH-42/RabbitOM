using System;

namespace RabbitOM.Net.RtspV2.Headers
{
    using RabbitOM.Net.RtspV2.Headers.DataTypes;
    
    public sealed class RequireRtspHeaderValue
    {
        public RtspHeaderValueCollection<string> Tags { get; } = new RtspHeaderValueCollection<string>( RtspHeaderValueValidator.IsWellFormed );
        
        public static bool TryParse( string input , out RequireRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , "," , out string[] tokens ) )
            {
                var header = new RequireRtspHeaderValue();

                foreach ( var token in tokens )
                {
                    header.Tags.TryAdd( RtspHeaderValueSanitizer.UnQuotesWithTrim( token ) );
                }

                if ( header.Tags.Count > 0 )
                {
                    result = header;
                }
            }

            return result != null;
        }


        public override string ToString()
        {
            return string.Join( ", " , Tags );
        }
    }
}
