using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
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
            Scheme = RtspHeaderParser.Formatter.Filter( value );
        }

        public void SetUserName( string value )
        {
            UserName = RtspHeaderParser.Formatter.Filter( value );
        }

        public void SetRealm( string value )
        {
            Realm = RtspHeaderParser.Formatter.Filter( value );
        }

        public void SetNonce( string value )
        {
            Nonce = RtspHeaderParser.Formatter.Filter( value );
        }

        public void SetOpaque( string value )
        {
            Opaque = RtspHeaderParser.Formatter.Filter( value );
        }

        public void SetDomain( string value )
        {
            Domain = RtspHeaderParser.Formatter.Filter( value );
        }

        public void SetUri( string value )
        {
            Uri = RtspHeaderParser.Formatter.Filter( value );
        }

        public void SetResponse( string value )
        {
            Response = RtspHeaderParser.Formatter.Filter( value );
        }

        public void SetAlgorithm( string value )
        {
            Algorithm = RtspHeaderParser.Formatter.Filter( value );
        }

        public void SetClientNonce( string value )
        {
            ClientNonce = RtspHeaderParser.Formatter.Filter( value );
        }

        public void SetNonceCount( string value )
        {
            NonceCount = RtspHeaderParser.Formatter.Filter( value );
        }

        public void SetQualityOfProtection( string value )
        {
            QualityOfProtection = RtspHeaderParser.Formatter.Filter( value );
        }

        public bool AddExtension( string value )
        {
            var extension = RtspHeaderParser.Formatter.Filter( value );

            if ( string.IsNullOrWhiteSpace( extension ) )
            {
                return false;
            }

            return _extensions.Add( extension );
        }

        public bool RemoveExtension( string value )
        {
            return _extensions.Remove( RtspHeaderParser.Formatter.Filter( value ) );
        }

        public void ClearExtensions()
        {
            _extensions.Clear();
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendFormat( "{0} " , Scheme );
            builder.AppendFormat( "username={0}, " , RtspHeaderParser.Formatter.Quote( UserName ) );
            builder.AppendFormat( "realm={0}, " , RtspHeaderParser.Formatter.Quote( Realm ) );
            builder.AppendFormat( "nonce={0}, " , RtspHeaderParser.Formatter.Quote( Nonce ) );

            if ( ! string.IsNullOrWhiteSpace( Domain ) )
            {
                builder.AppendFormat( "domain={0}, " , RtspHeaderParser.Formatter.Quote( Domain ) );
            }

            if ( ! string.IsNullOrWhiteSpace( Opaque ) )
            {
                builder.AppendFormat( "opaque={0}, " , RtspHeaderParser.Formatter.Quote( Opaque ) );
            }

            builder.AppendFormat( "uri={0}, " , RtspHeaderParser.Formatter.Quote( Uri ) );
            builder.AppendFormat( "response={0}, " , RtspHeaderParser.Formatter.Quote( Response ) );

            if ( ! string.IsNullOrWhiteSpace( Algorithm ) )
            {
                builder.AppendFormat( "algorithm={0}, " , RtspHeaderParser.Formatter.Quote( Algorithm ) );
            }

            if ( ! string.IsNullOrWhiteSpace( ClientNonce ) )
            {
                builder.AppendFormat( "cnonce={0}, " , RtspHeaderParser.Formatter.Quote( ClientNonce ) );
            }

            if ( ! string.IsNullOrWhiteSpace( NonceCount ) )
            {
                builder.AppendFormat( "nc={0}, " , RtspHeaderParser.Formatter.Quote( NonceCount ) );
            }

            if ( ! string.IsNullOrWhiteSpace( QualityOfProtection ) )
            {
                builder.AppendFormat( "qop={0}, " , RtspHeaderParser.Formatter.Quote( QualityOfProtection ) );
            }

            foreach ( var extension in _extensions )
            {
                if ( RtspHeaderProperty.TryParse( extension , "=" , out var parameter ) )
                {
                    builder.AppendFormat( "{0}={1}, " , parameter.Name , RtspHeaderParser.Formatter.Quote( parameter.Value ) );
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

            if ( RtspHeaderParser.TryParse( RtspHeaderParser.Formatter.Filter( input ) , " " , out var tokens ) )
            {
                var scheme = tokens.FirstOrDefault();
                
                if ( RtspHeaderParser.TryParse( string.Join( " " , tokens.Skip( 1 ) ) , "," , out tokens ) )
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
