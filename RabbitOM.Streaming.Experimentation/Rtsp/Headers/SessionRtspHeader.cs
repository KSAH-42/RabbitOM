using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters;
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Validation;

    public sealed class SessionRtspHeader
    {
        public static readonly string TypeName = "Session";

        public static readonly StringComparer ValueComparer = StringComparer.OrdinalIgnoreCase;
        public static readonly StringValueAdapter ValueAdapter = StringValueAdapter.TrimWithUnQuoteAdapter;
        public static readonly StringValueValidator ValueValidator = StringValueValidator.TokenValidator;
        

        private string _identifier = string.Empty;
        private long? _timeout;
        private readonly StringRtspHashSet _extensions = new StringRtspHashSet();

        

        public string Identifier
        {
            get => _identifier;
            set => _identifier = ValueAdapter.Adapt( value );
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





        public bool AddExtension( string value )
        {
            return _extensions.Add( ValueAdapter.Adapt( value ) );
        }

        public bool RemoveExtension( string value )
        {
            return _extensions.Remove( ValueAdapter.Adapt( value ) );
        }

        public void ClearExtensions()
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






        public static bool TryParse( string input , out SessionRtspHeader result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( input , ";" , out string[] tokens ) )
            {
                var identifer = tokens.FirstOrDefault( token => ! token.Contains( "=" ) && token.Any( x => char.IsLetterOrDigit(x) ) );

                if ( string.IsNullOrWhiteSpace( identifer ) )
                {
                    return false;
                }
                
                var header = new SessionRtspHeader();

                header.Identifier = identifer;
                
                foreach( var token in tokens )
                {
                    if ( RtspHeaderParser.TryParse( token , "=" , out KeyValuePair<string,string> parameter ) )
                    {
                        if ( ValueComparer.Equals( "timeout" , parameter.Key ) )
                        {
                            if ( long.TryParse( ValueAdapter.Adapt( parameter.Value ) , out long value ) )
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

                if ( ValueValidator.TryValidate( header.Identifier ) )
                {
                    result = header;
                }
            }

            return result != null;
        }
    }
}
