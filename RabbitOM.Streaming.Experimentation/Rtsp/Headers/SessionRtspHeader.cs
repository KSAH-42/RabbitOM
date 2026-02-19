using System;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Formatting;
    using System.Threading;

    public sealed class SessionRtspHeader 
    {
        public static readonly string TypeName = "Session";
        
        public string Identifier { get; private set; } = string.Empty;

        public long? Timeout { get; set; }

        public static bool TryParse( string input , out SessionRtspHeader result )
        {
            result = null;

            if ( StringRtspHeaderParser.TryParse( RtspValueNormalizer.Normalize( input ) , ';' , out var tokens ) )
            {
                var identifer = tokens.FirstOrDefault( token => ! token.Contains( '=' ) && token.Any( x => char.IsLetterOrDigit(x) ) );

                if ( string.IsNullOrWhiteSpace( identifer ) )
                {
                    return false;
                }
                
                var header = new SessionRtspHeader();

                header.SetIdentifier( identifer );

                foreach( var token in tokens )
                {
                    if ( StringParameterRtspHeaderParser.TryParse( token , '=' , out var parameter ) )
                    {
                        if ( string.Equals( "timeout" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                        {
                            if ( long.TryParse( RtspValueNormalizer.Normalize( parameter.Value ) , out var timeout ) )
                            {
                                header.Timeout = timeout;
                                break;
                            }
                        }
                    }
                }

                if ( string.IsNullOrWhiteSpace( header.Identifier ) )
                {
                    return false;
                }

                result = header;
            }

            return result != null;
        }

        public void SetIdentifier( string value )
        {
            Identifier = RtspValueNormalizer.Normalize( value );
        }

        public override string ToString()
        {
            if ( string.IsNullOrWhiteSpace( Identifier ) )
            {
                return string.Empty;
            }

            if ( Timeout.HasValue )
            {
                return $"{Identifier};timeout={Timeout}";
            }

            return Identifier;
        }
    }
}
