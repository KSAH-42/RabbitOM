using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Parsers;

    public sealed class AuthorizationRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "Authorization";
        



        
        
        private readonly HashSet<string> _extensions = new HashSet<string>( StringComparer.OrdinalIgnoreCase );
        



        
        
        public string Scheme { get; private set; } = string.Empty;
        
        public string UserName { get; private set; } = string.Empty;

        public string Realm { get; private set; } = string.Empty;
        
        public string Nonce { get; private set; } = string.Empty;
        
        public string Domain { get; private set; } = string.Empty;
        
        public string Uri { get; private set; } = string.Empty;
        
        public string Opaque { get; private set; } = string.Empty;
        
        public string Response { get; private set; } = string.Empty;

        public string Algorithm { get; private set; } = string.Empty;

        public string QualityOfProtection { get; private set; } = string.Empty;

        public string NonceCount { get; private set; } = string.Empty;

        public string ClientNonce { get; private set; } = string.Empty;
                
        public IReadOnlyCollection<string> Extensions { get => _extensions; }
        



        
        
        public void SetScheme( string value )
        {
            Scheme = StringRtspHeaderParser.TrimValue( value , StringRtspHeaderParser.SpaceWithQuotesChars );
        }

        public void SetUserName( string value )
        {
            UserName = StringRtspHeaderParser.TrimValue( value , StringRtspHeaderParser.SpaceWithQuotesChars );
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

        public void SetDomain( string value )
        {
            Domain = StringRtspHeaderParser.TrimValue( value , StringRtspHeaderParser.SpaceWithQuotesChars );
        }

        public void SetUri( string value )
        {
            Uri = StringRtspHeaderParser.TrimValue( value , StringRtspHeaderParser.SpaceWithQuotesChars );
        }

        public void SetResponse( string value )
        {
            Response = StringRtspHeaderParser.TrimValue( value , StringRtspHeaderParser.SpaceWithQuotesChars );
        }

        public void SetAlgorithm( string value )
        {
            Algorithm = StringRtspHeaderParser.TrimValue( value , StringRtspHeaderParser.SpaceWithQuotesChars );
        }

        public void SetClientNonce( string value )
        {
            ClientNonce = StringRtspHeaderParser.TrimValue( value , StringRtspHeaderParser.SpaceWithQuotesChars );
        }

        public void SetNonceCount( string value )
        {
            NonceCount = StringRtspHeaderParser.TrimValue( value , StringRtspHeaderParser.SpaceWithQuotesChars );
        }

        public void SetQualityOfProtection( string value )
        {
            QualityOfProtection = StringRtspHeaderParser.TrimValue( value , StringRtspHeaderParser.SpaceWithQuotesChars );
        }

        public bool AddExtension( string value )
        {
            var extension = StringRtspHeaderParser.TrimValue( value , StringRtspHeaderParser.SpaceWithQuotesChars );

            if ( string.IsNullOrWhiteSpace( extension ) )
            {
                return false;
            }

            return _extensions.Add( extension );
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

            builder.AppendFormat( "{0} " , Scheme );
            builder.AppendFormat( "username={0}, " , StringRtspHeaderParser.Quote( UserName ) );
            builder.AppendFormat( "realm={0}, " , StringRtspHeaderParser.Quote( Realm ) );
            builder.AppendFormat( "nonce={0}, " , StringRtspHeaderParser.Quote( Nonce ) );

            if ( ! string.IsNullOrWhiteSpace( Domain ) )
            {
                builder.AppendFormat( "domain={0}, " , StringRtspHeaderParser.Quote( Domain ) );
            }

            if ( ! string.IsNullOrWhiteSpace( Opaque ) )
            {
                builder.AppendFormat( "opaque={0}, " , StringRtspHeaderParser.Quote( Opaque ) );
            }

            builder.AppendFormat( "uri={0}, " , StringRtspHeaderParser.Quote( Uri ) );
            builder.AppendFormat( "response={0}, " , StringRtspHeaderParser.Quote( Response ) );

            if ( ! string.IsNullOrWhiteSpace( Algorithm ) )
            {
                builder.AppendFormat( "algorithm={0}, " , StringRtspHeaderParser.Quote( Algorithm ) );
            }

            if ( ! string.IsNullOrWhiteSpace( ClientNonce ) )
            {
                builder.AppendFormat( "cnonce={0}, " , StringRtspHeaderParser.Quote( ClientNonce ) );
            }

            if ( ! string.IsNullOrWhiteSpace( NonceCount ) )
            {
                builder.AppendFormat( "nc={0}, " , StringRtspHeaderParser.Quote( NonceCount ) );
            }

            if ( ! string.IsNullOrWhiteSpace( QualityOfProtection ) )
            {
                builder.AppendFormat( "qop={0}, " , StringRtspHeaderParser.Quote( QualityOfProtection ) );
            }

            foreach ( var extension in _extensions )
            {
                if ( RtspHeaderProperty.TryParse( extension , "=" , out var parameter ) )
                {
                    builder.AppendFormat( "{0}={1}, " , parameter.Name , StringRtspHeaderParser.Quote( parameter.Value ) );
                }
                else
                {
                    builder.AppendFormat( "{0}, ", extension );
                }
            }

            return builder.ToString().Trim( ' ' , ',' );
        }
        



        
        
        public static bool TryParse( string input , out AuthorizationRtspHeader result )
        {
            result = null;

            if ( StringRtspHeaderParser.TryParse( input , " " , out var tokens ) )
            {
                var scheme = tokens.FirstOrDefault();
                
                if ( StringRtspHeaderParser.TryParse( string.Join( " " , tokens.Skip( 1 ) ) , "," , out tokens ) )
                {
                    result = new AuthorizationRtspHeader(); 
                    result.SetScheme( scheme );

                    var comparer = StringComparer.OrdinalIgnoreCase;

                    foreach ( var token in tokens )
                    {
                        if ( RtspHeaderProperty.TryParse( token , "=" , out var parameter ) )
                        {
                            if ( comparer.Equals( "username" , parameter.Name ) )
                            {
                                result.SetUserName( parameter.Value );
                            }
                            else if ( comparer.Equals( "realm" , parameter.Name ) )
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
                            else if ( comparer.Equals( "domain" , parameter.Name ) )
                            {
                                result.SetDomain( parameter.Value );
                            }
                            else if ( comparer.Equals( "uri" , parameter.Name ) )
                            {
                                result.SetUri( parameter.Value );
                            }
                            else if ( comparer.Equals( "response" , parameter.Name ) )
                            {
                                result.SetResponse( parameter.Value );
                            }                            
                            else if ( comparer.Equals( "algorithm" , parameter.Name ) )
                            {
                                result.SetAlgorithm( parameter.Value );
                            }
                            else if ( comparer.Equals( "cnonce" , parameter.Name ) )
                            {
                                result.SetClientNonce( parameter.Value );
                            }
                            else if ( comparer.Equals( "nc" , parameter.Name ) )
                            {
                                result.SetNonceCount( parameter.Value );
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
