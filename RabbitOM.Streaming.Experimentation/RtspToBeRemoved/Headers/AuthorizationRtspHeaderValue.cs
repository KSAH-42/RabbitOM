using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp2BeRemoved.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp2BeRemoved.Headers.Normalizers;
    
    public sealed class AuthorizationRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "Authorization";

        private static readonly StringComparer ValueComparer = StringComparer.OrdinalIgnoreCase;
        private static readonly StringValueNormalizer ValueNormalizer = StringValueNormalizer.TrimWithUnQuoteNormalizer;

        private string _scheme = string.Empty;        
        private string _userName = string.Empty;
        private string _realm = string.Empty;        
        private string _nonce = string.Empty;        
        private string _domain = string.Empty;        
        private string _uri = string.Empty;        
        private string _opaque = string.Empty;        
        private string _response = string.Empty;
        private string _algorithm = string.Empty;
        private string _qualityOfProtection = string.Empty;
        private string _nonceCount = string.Empty;
        private string _clientNonce = string.Empty;                
        private readonly HashSet<string> _extensions = new HashSet<string>( StringComparer.OrdinalIgnoreCase );

        

        public string Scheme
        {
            get => _scheme;
            set => _scheme = ValueNormalizer.Normalize( value );
        }
        
        public string UserName
        {
            get => _userName;
            set => _userName = ValueNormalizer.Normalize( value );
        }

        public string Realm
        {
            get => _realm;
            set => _realm = ValueNormalizer.Normalize( value );
        }
        
        public string Nonce
        {
            get => _nonce;
            set => _nonce = ValueNormalizer.Normalize( value );
        }
        
        public string Domain
        {
            get => _domain;
            set => _domain = ValueNormalizer.Normalize( value );
        }
        
        public string Uri
        {
            get => _uri;
            set => _uri = ValueNormalizer.Normalize( value );
        }
        
        public string Opaque
        {
            get => _opaque;
            set => _opaque = ValueNormalizer.Normalize( value );
        }
        
        public string Response
        {
            get => _response;
            set => _response = ValueNormalizer.Normalize( value );
        }

        public string Algorithm
        {
            get => _algorithm;
            set => _algorithm = ValueNormalizer.Normalize( value );
        }

        public string QualityOfProtection
        {
            get => _qualityOfProtection;
            set => _qualityOfProtection = ValueNormalizer.Normalize( value );
        }

        public string NonceCount
        {
            get => _nonceCount;
            set => _nonceCount = ValueNormalizer.Normalize( value );
        }

        public string ClientNonce
        {
            get => _clientNonce;
            set => _clientNonce = ValueNormalizer.Normalize( value );
        }
                
        public IReadOnlyCollection<string> Extensions
        { 
            get => _extensions; 
        }

        

        public static bool TryParse( string input , out AuthorizationRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( input , " " , out string[] tokens ) )
            {
                var header = new AuthorizationRtspHeaderValue() { Scheme = tokens.FirstOrDefault() };
                
                if ( RtspHeaderParser.TryParse( string.Join( " " , tokens.Skip( 1 ) ) , "," , out tokens ) )
                {
                    foreach ( var token in tokens )
                    {
                        if ( RtspHeaderParser.TryParse( token , "=" , out KeyValuePair<string,string> parameter ) )
                        {
                            if ( ValueComparer.Equals( "username" , parameter.Key ) )
                            {
                                header.UserName = parameter.Value;
                            }
                            else if ( ValueComparer.Equals( "realm" , parameter.Key ) )
                            {
                                header.Realm = parameter.Value;
                            }
                            else if ( ValueComparer.Equals( "nonce" , parameter.Key ) )
                            {
                                header.Nonce = parameter.Value;
                            }
                            else if ( ValueComparer.Equals( "opaque" , parameter.Key ) )
                            {
                                header.Opaque = parameter.Value;
                            }
                            else if ( ValueComparer.Equals( "domain" , parameter.Key ) )
                            {
                                header.Domain = parameter.Value;
                            }
                            else if ( ValueComparer.Equals( "uri" , parameter.Key ) )
                            {
                                header.Uri = parameter.Value;
                            }
                            else if ( ValueComparer.Equals( "response" , parameter.Key ) )
                            {
                                header.Response = parameter.Value;
                            }                            
                            else if ( ValueComparer.Equals( "algorithm" , parameter.Key ) )
                            {
                                header.Algorithm = parameter.Value;
                            }
                            else if ( ValueComparer.Equals( "cnonce" , parameter.Key ) )
                            {
                                header.ClientNonce = parameter.Value;
                            }
                            else if ( ValueComparer.Equals( "nc" , parameter.Key ) )
                            {
                                header.NonceCount = parameter.Value;
                            }
                            else if ( ValueComparer.Equals( "qop" , parameter.Key ) )
                            {
                                header.QualityOfProtection = parameter.Value;
                            }
                            else
                            {
                                header.AddExtension( token );
                            }
                        }
                    }

                    if ( RtspHeaderProtocolValidator.IsValidToken( header.Scheme ) && RtspHeaderProtocolValidator.IsValidToken( header.UserName ) )
                    {
                        result = header;
                    }
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
                if ( RtspHeaderParser.TryParse( extension , "=" , out KeyValuePair<string,string> parameter ) )
                {
                    builder.AppendFormat( "{0}=\"{1}\", " , parameter.Key , parameter.Value );
                }
                else
                {
                    builder.AppendFormat( "{0}, ", extension );
                }
            }

            return builder.ToString().Trim( ' ' , ',' );
        }
    }
}
