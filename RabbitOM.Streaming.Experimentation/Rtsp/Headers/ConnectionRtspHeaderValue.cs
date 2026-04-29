using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types;
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Compliances;

    public sealed class ConnectionRtspHeaderValue
    {
        private static readonly StringValueNormalizer ValueNormalizer = StringValueNormalizer.TrimWithUnQuoteNormalizer;
        
        public StringCollection Directives { get; } = new StringCollection( ValueNormalizer , IsValidDirective );
        
        public static bool IsValidDirective( string value )
        {
            return ! string.IsNullOrWhiteSpace( value ) && Token.IsValidToken( value );
        }

        public static bool TryParse( string input , out ConnectionRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , "," , out string[] tokens ) )
            {
                var header = new ConnectionRtspHeaderValue();

                foreach ( var token in tokens )
                {
                    header.Directives.TryAdd( token );
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
