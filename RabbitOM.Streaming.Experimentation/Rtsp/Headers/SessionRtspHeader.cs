using System;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class SessionRtspHeader : RtspHeader 
    {
        public const string TypeName = "Session";
        

        private string _identifier = string.Empty;

        private long? _timeout;



        public string Identifier
        { 
            get => _identifier;
            set => _identifier = StringRtspNormalizer.Normalize( value );
        }


        public long? Timeout
        {
            get => _timeout;
            set => _timeout = value;
        }



        public static bool TryParse( string input , out SessionRtspHeader result )
        {
            result = null;

            if ( ! RtspHeaderParser.TryParse( StringRtspNormalizer.Normalize( input ) , ";" , out var tokens ) )
            {
                return false;
            }

            var identifier = tokens.ElementAtOrDefault( 0 );

            if ( string.IsNullOrWhiteSpace( identifier ) || ! identifier.Any( x => char.IsLetterOrDigit( x ) ) )
            {
                return false;
            }

            result = new SessionRtspHeader() { Identifier = identifier };

            foreach( var token in tokens.Skip( 1 ) )
            {
                if ( RtspHeaderParser.TryParse( token , "=" , out var parameters ) )
                {
                    var parameterName = parameters.ElementAtOrDefault( 0 );
                    var parameterValue= StringRtspNormalizer.Normalize( parameters.ElementAtOrDefault( 1 ) );

                    if ( string.Equals( "timeout" , parameterName  , StringComparison.OrdinalIgnoreCase ) )
                    {
                        if ( long.TryParse( parameterValue , out var timeout ) )
                        {
                            result.Timeout = timeout;
                        }
                    }
                }
            }

            return true;
        }



        public override bool TryValidate()
        {
            if ( _timeout.HasValue && _timeout.Value <= 0 )
            {
                return false;
            }

            return StringRtspValidator.TryValidate( _identifier ) && _identifier.Any( x => char.IsLetterOrDigit( x ) );
        }

        public override string ToString()
        {
            if ( string.IsNullOrWhiteSpace( _identifier ) )
            {
                return string.Empty;
            }

            var builder = new StringBuilder();

            builder.Append( _identifier );

            if ( _timeout.HasValue )
            {
                builder.AppendFormat( ";timeout={0}" , _timeout );
            }

            return builder.ToString();
        }
    }
}
