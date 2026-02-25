using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types;

    public sealed class AuthorizationRtspHeader
    {
        public static readonly string TypeName = "Authorization";





        private readonly HashSet<string> _extensions = new HashSet<string>( StringComparer.OrdinalIgnoreCase );




        public string Scheme { get; private set; } = string.Empty;
        
        public string UserName { get; private set; } = string.Empty;

        public string Realm { get; private set; } = string.Empty;
        
        public string Nonce { get; private set; } = string.Empty;
        
        public string Domain { get; private set; } = string.Empty;

        public string Opaque { get; private set; } = string.Empty;
        
        public string Uri { get; private set; } = string.Empty;
        
        public string Response { get; private set; } = string.Empty;

        public string Algorithm { get; private set; } = string.Empty;

        public string ClientNonce { get; private set; } = string.Empty;

        public string NonceCount { get; private set; } = string.Empty;

        public string QualityOfProtection { get; private set; } = string.Empty;

        public IReadOnlyCollection<string> Extensions { get => _extensions; }




        public void SetScheme( string value )
        {
            Scheme = RtspHeaderValueNormalizer.Normalize( value );
        }

        public void SetUserName( string value )
        {
            UserName = RtspHeaderValueNormalizer.Normalize( value );
        }

        public void SetRealm( string value )
        {
            Realm = RtspHeaderValueNormalizer.Normalize( value );
        }

        public void SetNonce( string value )
        {
            Nonce = RtspHeaderValueNormalizer.Normalize( value );
        }

        public void SetOpaque( string value )
        {
            Opaque = RtspHeaderValueNormalizer.Normalize( value );
        }

        public void SetDomain( string value )
        {
            Domain = RtspHeaderValueNormalizer.Normalize( value );
        }

        public void SetUri( string value )
        {
            Uri = RtspHeaderValueNormalizer.Normalize( value );
        }

        public void SetResponse( string value )
        {
            Response = RtspHeaderValueNormalizer.Normalize( value );
        }

        public void SetAlgorithm( string value )
        {
            Algorithm = RtspHeaderValueNormalizer.Normalize( value );
        }

        public void SetClientNonce( string value )
        {
            ClientNonce = RtspHeaderValueNormalizer.Normalize( value );
        }

        public void SetNonceCount( string value )
        {
            NonceCount = RtspHeaderValueNormalizer.Normalize( value );
        }

        public void SetQualityOfProtection( string value )
        {
            QualityOfProtection = RtspHeaderValueNormalizer.Normalize( value );
        }

        public bool AddExtension( string value )
        {
            var extension = RtspHeaderValueNormalizer.Normalize( value );

            if ( string.IsNullOrWhiteSpace( extension ) )
            {
                return false;
            }

            return _extensions.Add( extension );
        }

        public bool RemoveExtension( string value )
        {
            return _extensions.Remove( RtspHeaderValueNormalizer.Normalize( value ) );
        }

        public void RemoveExtensions()
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

            builder.AppendFormat( "username=\"{0}\", " , UserName );
            builder.AppendFormat( "realm=\"{0}\", " , Realm );
            builder.AppendFormat( "nonce=\"{0}\", " , Nonce );

            if ( ! string.IsNullOrWhiteSpace( Domain ) )
            {
                builder.AppendFormat( "domain=\"{0}\", " , Domain );
            }

            if ( ! string.IsNullOrWhiteSpace( Opaque ) )
            {
                builder.AppendFormat( "opaque=\"{0}\", " , Opaque );
            }

            builder.AppendFormat( "uri=\"{0}\", " , Uri );
            builder.AppendFormat( "response=\"{0}\", " , Response );

            if ( ! string.IsNullOrWhiteSpace( Algorithm ) )
            {
                builder.AppendFormat( "algorithm=\"{0}\", " , Algorithm );
            }

            if ( ! string.IsNullOrWhiteSpace( ClientNonce ) )
            {
                builder.AppendFormat( "cnonce=\"{0}\", " , ClientNonce );
            }

            if ( ! string.IsNullOrWhiteSpace( NonceCount ) )
            {
                builder.AppendFormat( "nc=\"{0}\", " , NonceCount );
            }

            if ( ! string.IsNullOrWhiteSpace( QualityOfProtection ) )
            {
                builder.AppendFormat( "qop=\"{0}\", " , QualityOfProtection );
            }

            foreach ( var extension in _extensions )
            {
                if ( StringParameter.TryParse( extension , "=" , out var parameter ) )
                {
                    builder.AppendFormat( "{0}=\"{1}\", " , parameter.Name , parameter.Value );
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

            if ( RtspHeaderParser.TryParse( RtspHeaderValueNormalizer.Normalize( input ) , " " , out var tokens ) )
            {
                var scheme = tokens.FirstOrDefault();
                
                if ( RtspHeaderParser.TryParse( string.Join( " " , tokens.Skip( 1 ) ) , "," , out tokens ) )
                {
                    result = new AuthorizationRtspHeader(); 
                    
                    result.SetScheme( scheme );

                    foreach ( var token in tokens )
                    {
                        if ( StringParameter.TryParse( token , "=" , out var parameter ) )
                        {
                            if ( string.Equals( "username" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                            {
                                result.SetUserName( parameter.Value );
                            }
                            else if ( string.Equals( "realm" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                            {
                                result.SetRealm( parameter.Value );
                            }
                            else if ( string.Equals( "nonce" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                            {
                                result.SetNonce( parameter.Value );
                            }
                            else if ( string.Equals( "opaque" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                            {
                                result.SetOpaque( parameter.Value );
                            }
                            else if ( string.Equals( "domain" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                            {
                                result.SetDomain( parameter.Value );
                            }
                            else if ( string.Equals( "uri" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                            {
                                result.SetUri( parameter.Value );
                            }
                            else if ( string.Equals( "response" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                            {
                                result.SetResponse( parameter.Value );
                            }                            
                            else if ( string.Equals( "algorithm" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                            {
                                result.SetAlgorithm( parameter.Value );
                            }
                            else if ( string.Equals( "cnonce" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                            {
                                result.SetClientNonce( parameter.Value );
                            }
                            else if ( string.Equals( "nc" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                            {
                                result.SetNonceCount( parameter.Value );
                            }
                            else if ( string.Equals( "qop" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
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
