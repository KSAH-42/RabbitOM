using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.RtspToBeRemoved.Headers
{
    using RabbitOM.Streaming.Experimentation.RtspToBeRemoved.Headers.Normalizers;
   
    public sealed class SessionRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "Session";

        private static readonly StringComparer ValueComparer = StringComparer.OrdinalIgnoreCase;
        private static readonly StringValueNormalizer ValueNormalizer = StringValueNormalizer.TrimWithUnQuoteNormalizer;
        

        private string _identifier = string.Empty;
        private long? _timeout;
        private readonly HashSet<string> _extensions = new HashSet<string>( StringComparer.OrdinalIgnoreCase );

        

        public string Identifier
        {
            get => _identifier;
            set => _identifier = ValueNormalizer.Normalize( value );
        }

        public long? Timeout
        {
            get => _timeout;
            set => _timeout = value;
        }

        public IReadOnlyCollection<string> Extensions
        {
            get => _extensions;
        }


        public static bool TryParse( string input , out SessionRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( input , ";" , out string[] tokens ) )
            {
                var identifer = tokens.FirstOrDefault( token => ! token.Contains( "=" ) && token.Any( x => char.IsLetterOrDigit(x) ) );

                if ( string.IsNullOrWhiteSpace( identifer ) )
                {
                    return false;
                }
                
                var header = new SessionRtspHeaderValue() { Identifier = identifer };
                
                foreach( var token in tokens )
                {
                    if ( RtspHeaderParser.TryParse( token , "=" , out KeyValuePair<string,string> parameter ) )
                    {
                        if ( ValueComparer.Equals( "timeout" , parameter.Key ) )
                        {
                            if ( long.TryParse( ValueNormalizer.Normalize( parameter.Value ) , out long value ) )
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
                            header.AddExtension( token );
                        }
                    }
                }

                if ( RtspHeaderProtocolValidator.IsValidToken( header.Identifier ) )
                {
                    result = header;
                }
            }

            return result != null;
        }


        public bool AddExtension( string value )
        {
            if ( RtspHeaderProtocolValidator.IsValid( value = ValueNormalizer.Normalize( value ) ) )
            {
                return _extensions.Add( value );
            }

            return false;
        }

        public bool RemoveExtension( string value )
        {
            return _extensions.Remove( ValueNormalizer.Normalize( value ) );
        }

        public void RemoveExtensions()
        {
            _extensions.Clear();
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
