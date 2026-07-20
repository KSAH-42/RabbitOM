using System;

namespace RabbitOM.Streaming.RtspV2.Headers
{
    using RabbitOM.Streaming.RtspV2.Headers.DataTypes;
    
    public sealed class ConnectionRtspHeaderValue
    {
        public StringRtspHeaderValueCollection Values { get; } = new StringRtspHeaderValueCollection();
        
        public static bool TryParse( string input , out ConnectionRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , "," , out string[] tokens ) )
            {
                var header = new ConnectionRtspHeaderValue();

                foreach ( var token in tokens )
                {
                    header.Values.TryAdd( RtspHeaderValueSanitizer.UnQuotesWithTrim( token ) );
                }

                if ( header.Values.Count > 0 )
                {
                    result = header;
                }
            }

            return result != null;
        }


        public override string ToString()
        {
            return string.Join( ", " , Values );
        }
    }
}
