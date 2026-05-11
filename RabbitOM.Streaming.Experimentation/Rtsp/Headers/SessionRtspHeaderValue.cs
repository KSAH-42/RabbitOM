using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types;
   
    public sealed class SessionRtspHeaderValue
    {
        private static readonly StringComparer ValueComparer = StringComparer.OrdinalIgnoreCase;
        

        private string _identifier = string.Empty;
        private long? _timeout;
        private readonly StringRtspHeaderValueCollection _extensions = new StringRtspHeaderValueCollection();

        

        public string Identifier
        {
            get => _identifier;
            set => _identifier = RtspHeaderValueValidator.EnsureWellFormedToken( RtspHeaderValueSanitizer.UnQuotesWithTrim( value ) );
        }

        public long? Timeout
        {
            get => _timeout;
            set => _timeout = value;
        }

        public StringRtspHeaderValueCollection Extensions
        {
            get => _extensions;
        }


        
        public static bool TryParse( string input , out SessionRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , ";" , out string[] tokens ) )
            {
                var identifer = tokens.FirstOrDefault( RtspHeaderValueValidator.TryEnsureWellFormedToken );

                if ( ! string.IsNullOrWhiteSpace( identifer ) )
                {
                    return false;
                }
                
                var header = new SessionRtspHeaderValue() { _identifier = RtspHeaderValueSanitizer.UnQuotesWithTrim( identifer ) };
                
                foreach( var token in tokens )
                {
                    if ( RtspHeaderValueParser.TryParse( token , "=" , out KeyValuePair<string,string> parameter ) )
                    {
                        if ( ValueComparer.Equals( "timeout" , parameter.Key ) )
                        {
                            if ( long.TryParse( RtspHeaderValueSanitizer.UnQuotesWithTrim( parameter.Value ) , out long value ) )
                            {
                                header.Timeout = value;
                            }
                        }
                    }
                    else
                    {
                        if ( string.IsNullOrWhiteSpace( header.Identifier ) )
                        {
                            header.Identifier = token ;
                        }
                        else
                        {
                            header.Extensions.TryAdd( token );
                        }
                    }
                }

                if ( RtspHeaderValueValidator.TryEnsureWellFormedToken( header.Identifier ) )
                {
                    result = header;
                }
            }

            return result != null;
        }


        
        
        public override string ToString()
        {
            var builder = new StringBuilder();

            if ( ! string.IsNullOrWhiteSpace( Identifier ) )
            {
                builder.AppendFormat( "{0};" , Identifier );
            }

            if ( Timeout.HasValue )
            {
                builder.AppendFormat( "timeout={0};" , Timeout );
            }

            foreach ( var extension in _extensions )
            {
                builder.AppendFormat( "{0};" , extension );
            }

            return builder.ToString().Trim( ' ' , ';' );
        }
    }
}
