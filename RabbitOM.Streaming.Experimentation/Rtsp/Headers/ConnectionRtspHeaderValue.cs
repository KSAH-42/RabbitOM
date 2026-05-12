using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.DataTypes;
    
    public sealed class ConnectionRtspHeaderValue
    {
        public StringRtspHeaderValueCollection Directives { get; } = new StringRtspHeaderValueCollection();
        
        public static bool TryParse( string input , out ConnectionRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , "," , out string[] tokens ) )
            {
                var header = new ConnectionRtspHeaderValue();

                foreach ( var token in tokens )
                {
                    header.Directives.TryAdd( RtspHeaderValueSanitizer.UnQuotesWithTrim( token ) );
                }

                if ( header.Directives.Count > 0 )
                {
                    result = header;
                }
            }

            return result != null;
        }


        public override string ToString()
        {
            return string.Join( ", " , Directives );
        }
    }
}
