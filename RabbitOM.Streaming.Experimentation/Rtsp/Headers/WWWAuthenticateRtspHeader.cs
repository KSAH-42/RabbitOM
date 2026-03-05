using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Parsers;

    public sealed class WWWAuthenticateRtspHeader : RtspHeader
    {
        private readonly HashSet<string> _extensions = new HashSet<string>( StringComparer.OrdinalIgnoreCase );




        public static string TypeName { get; } = "WWW-Authenticate";



        public string Scheme { get; private set; } = string.Empty;
        
        public string Realm { get; private set; } = string.Empty;
        
        public string Nonce { get; private set; } = string.Empty;
        
        public string Opaque { get; private set; } = string.Empty;
        
        public string Algorithm { get; private set; } = string.Empty;
        
        public string Stale { get; private set; } = string.Empty;

        public string QualityOfProtection { get; private set; } = string.Empty;

        public IReadOnlyCollection<string> Extensions { get => _extensions; }






        public void SetScheme( string value )
        {
            Scheme = StringRtspHeaderParser.TrimValue( value , StringRtspHeaderParser.SpaceWithQuotesChars );
        }

        public void SetRealm( string value )
        {
            Realm = StringRtspHeaderParser.TrimValue( value , StringRtspHeaderParser.SpaceWithQuotesChars );
        }

        public void SetNonce( string value )
        {
            Nonce = StringRtspHeaderParser.TrimValue( value , StringRtspHeaderParser.SpaceWithQuotesChars );
        }

        public void SetOpaque( string value )
        {
            Opaque = StringRtspHeaderParser.TrimValue( value , StringRtspHeaderParser.SpaceWithQuotesChars );
        }

        public void SetAlgorithm( string value )
        {
            Algorithm = StringRtspHeaderParser.TrimValue( value , StringRtspHeaderParser.SpaceWithQuotesChars );
        }

        public void SetStale( string value )
        {
            Stale = StringRtspHeaderParser.TrimValue( value , StringRtspHeaderParser.SpaceWithQuotesChars );
        }

        public void SetQualityOfProtection( string value )
        {
            QualityOfProtection = StringRtspHeaderParser.TrimValue( value , StringRtspHeaderParser.SpaceWithQuotesChars );
        }

        public bool AddExtension( string extension )
        {
            var value = StringRtspHeaderParser.TrimValue( extension , StringRtspHeaderParser.SpaceWithQuotesChars );

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            return _extensions.Add( value );
        }

        public bool RemoveExtension( string extension )
        {
            return _extensions.Remove( StringRtspHeaderParser.TrimValue( extension , StringRtspHeaderParser.SpaceWithQuotesChars ) );
        }

        public void ClearExtensions()
        {
            _extensions.Clear();
        }

        public override string ToString()
        {
            if ( string.IsNullOrWhiteSpace( Scheme ) )
            {
                return string.Empty;
            }

            var builder = new StringBuilder();

            builder.AppendFormat( "{0} " , Scheme );

            builder.AppendFormat( "realm=\"{0}\", " , Realm );

            if ( ! string.IsNullOrWhiteSpace( Nonce ) )
            {
                builder.AppendFormat( "nonce=\"{0}\", " , Nonce);
            }

            if ( ! string.IsNullOrWhiteSpace( Opaque ) )
            {
                builder.AppendFormat( "opaque=\"{0}\", " , Opaque );
            }

            if ( ! string.IsNullOrWhiteSpace( Algorithm ) )
            {
                builder.AppendFormat( "algorithm=\"{0}\", " , Algorithm );
            }

            if ( ! string.IsNullOrWhiteSpace( Stale ) )
            {
                builder.AppendFormat( "stale=\"{0}\", " , Stale );
            }

            if ( ! string.IsNullOrWhiteSpace( QualityOfProtection ) )
            {
                builder.AppendFormat( "qop=\"{0}\", " , QualityOfProtection );
            }

            foreach ( var extension in _extensions )
            {
                builder.AppendFormat( "{0}," , extension );
            }

            return builder.ToString().Trim( ' ' , ',' );
        }








        public static bool TryParse( string input , out WWWAuthenticateRtspHeader result )
        {
            result = null;

            if ( StringRtspHeaderParser.TryParse( input , " " , out var tokens ) )
            {
                var scheme = tokens.First();
                
                if ( StringRtspHeaderParser.TryParse( string.Join( " " , tokens.Skip(1) ) , "," , out tokens ) )
                {
                    result = new WWWAuthenticateRtspHeader(); 
                    result.SetScheme( scheme );

                    var comparer = StringComparer.OrdinalIgnoreCase;

                    foreach ( var token in tokens )
                    {
                        if ( RtspHeaderProperty.TryParse( token , "=" , out var parameter ) )
                        {
                            if ( comparer.Equals( "realm" , parameter.Name ) )
                            {
                                result.SetRealm( parameter.Value );
                            }
                            else if ( comparer.Equals( "nonce" , parameter.Name ) )
                            {
                                result.SetNonce( parameter.Value );
                            }
                            else if ( comparer.Equals( "opaque" , parameter.Name ) )
                            {
                                result.SetOpaque( parameter.Value );
                            }
                            else if ( comparer.Equals( "algorithm" , parameter.Name ) )
                            {
                                result.SetAlgorithm( parameter.Value );
                            }
                            else if ( comparer.Equals( "stale" , parameter.Name ) )
                            {
                                result.SetStale( parameter.Value );
                            }
                            else if ( comparer.Equals( "qop" , parameter.Name ) )
                            {
                                result.SetQualityOfProtection( parameter.Value );
                            }
                            else
                            {
                                result.AddExtension( token );
                            }
                        }
                    }
                }
            }

            return result != null;
        }
    }
}
