using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Parsers;

    public sealed class SessionRtspHeader : RtspHeader
    {
        private readonly HashSet<string> _extensions = new HashSet<string>();




        public static string TypeName { get; } = "Session";
        
        public string Identifier { get; private set; } = string.Empty;

        public long? Timeout { get; set; }

        public IReadOnlyCollection<string> Extensions { get => _extensions; }





        public void SetIdentifier( string value )
        {
            Identifier = StringRtspHeaderParser.TrimValue( value , StringRtspHeaderParser.SpaceWithQuotesChars );
        }

        public void SetTimeout( string value )
        {
            Timeout = LongRtspHeaderParser.TryParse( value , out var result ) ? new long?( result ) : null;
        }

        public bool AddExtension( string value )
        {
            var text = StringRtspHeaderParser.TrimValue( value , StringRtspHeaderParser.SpaceWithQuotesChars );

            if ( string.IsNullOrWhiteSpace( text ) )
            {
                return false;
            }

            return _extensions.Add( text );
        }

        public bool RemoveExtension( string value )
        {
            return _extensions.Remove( StringRtspHeaderParser.TrimValue( value , StringRtspHeaderParser.SpaceWithQuotesChars ) );
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

            if ( StringRtspHeaderParser.TryParse( input , ";" , out var tokens ) )
            {
                var identifer = tokens.FirstOrDefault( token => ! token.Contains( "=" ) && token.Any( x => char.IsLetterOrDigit(x) ) );

                if ( string.IsNullOrWhiteSpace( identifer ) )
                {
                    return false;
                }
                
                var header = new SessionRtspHeader();

                header.SetIdentifier( identifer );

                foreach( var token in tokens )
                {
                    if ( RtspHeaderProperty.TryParse( token , "=" , out var parameter ) )
                    {
                        if ( StringComparer.OrdinalIgnoreCase.Equals( "timeout" , parameter.Name ) )
                        {
                            if ( ! header.Timeout.HasValue )
                            {
                                header.SetTimeout( parameter.Value ); 
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

                if ( string.IsNullOrWhiteSpace( header.Identifier ) )
                {
                    return false;
                }

                result = header;
            }

            return result != null;
        }
    }
}
