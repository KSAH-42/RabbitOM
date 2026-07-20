using System;

namespace RabbitOM.Streaming.RtspV2.Headers
{
    using RabbitOM.Streaming.RtspV2.Headers.DataTypes;
    
    public sealed class RequireRtspHeaderValue
    {
        public StringRtspHeaderValueCollection Tags { get; } = new StringRtspHeaderValueCollection();
        
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
