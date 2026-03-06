using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Core;

    public sealed class AuthorizationRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "Authorization";

        public static readonly StringRtspHeaderComparer ValueComparer = StringRtspHeaderComparer.IgnoreCaseComparer;
        public static readonly StringRtspHeaderFilter ValueFilter = StringRtspHeaderFilter.UnQuoteFilter;


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
        private readonly StringRtspHashSet _extensions = new StringRtspHashSet();

        

        public string Scheme
        {
            get => _scheme;
            set => _scheme = ValueFilter.Filter( value );
        }
        
        public string UserName
        {
            get => _userName;
            set => _userName = ValueFilter.Filter( value );
        }

        public string Realm
        {
            get => _realm;
            set => _realm = ValueFilter.Filter( value );
        }
        
        
        public string Nonce
        {
            get => _nonce;
            set => _nonce = ValueFilter.Filter( value );
        }
        
        public string Domain
        {
            get => _domain;
            set => _domain = ValueFilter.Filter( value );
        }
        
        public string Uri
        {
            get => _uri;
            set => _uri = ValueFilter.Filter( value );
        }
        
        public string Opaque
        {
            get => _opaque;
            set => _opaque = ValueFilter.Filter( value );
        }
        
        public string Response
        {
            get => _response;
            set => _response = ValueFilter.Filter( value );
        }

        public string Algorithm
        {
            get => _algorithm;
            set => _algorithm = ValueFilter.Filter( value );
        }

        public string QualityOfProtection
        {
            get => _qualityOfProtection;
            set => _qualityOfProtection = ValueFilter.Filter( value );
        }

        public string NonceCount
        {
            get => _nonceCount;
            set => _nonceCount = ValueFilter.Filter( value );
        }

        public string ClientNonce
        {
            get => _clientNonce;
            set => _clientNonce = ValueFilter.Filter( value );
        }
                
        public IReadOnlyCollection<string> Extensions
        { 
            get => _extensions; 
        }
        



        
        
        public bool AddExtension( string value )
        {
            return _extensions.Add( ValueFilter.Filter( value ) );
        }

        public bool RemoveExtension( string value )
        {
            return _extensions.Remove( ValueFilter.Filter( value ) );
        }

        public void ClearExtensions()
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
                if ( RtspHeaderProperty.TryParse( extension , "=" , out var parameter ) )
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

            if ( RtspHeaderParser.TryParse( input , " " , out var tokens ) )
            {
                var scheme = tokens.FirstOrDefault();
                
                if ( RtspHeaderParser.TryParse( string.Join( " " , tokens.Skip( 1 ) ) , "," , out tokens ) )
                {
                    result = new AuthorizationRtspHeader() { Scheme = scheme };

                    foreach ( var token in tokens )
                    {
                        if ( RtspHeaderProperty.TryParse( token , "=" , out var parameter ) )
                        {
                            if ( ValueComparer.Equals( "username" , parameter.Name ) )
                            {
                                result.UserName = parameter.Value;
                            }
                            else if ( ValueComparer.Equals( "realm" , parameter.Name ) )
                            {
                                result.Realm = parameter.Value;
                            }
                            else if ( ValueComparer.Equals( "nonce" , parameter.Name ) )
                            {
                                result.Nonce = parameter.Value;
                            }
                            else if ( ValueComparer.Equals( "opaque" , parameter.Name ) )
                            {
                                result.Opaque = parameter.Value;
                            }
                            else if ( ValueComparer.Equals( "domain" , parameter.Name ) )
                            {
                                result.Domain = parameter.Value;
                            }
                            else if ( ValueComparer.Equals( "uri" , parameter.Name ) )
                            {
                                result.Uri = parameter.Value;
                            }
                            else if ( ValueComparer.Equals( "response" , parameter.Name ) )
                            {
                                result.Response = parameter.Value;
                            }                            
                            else if ( ValueComparer.Equals( "algorithm" , parameter.Name ) )
                            {
                                result.Algorithm = parameter.Value;
                            }
                            else if ( ValueComparer.Equals( "cnonce" , parameter.Name ) )
                            {
                                result.ClientNonce = parameter.Value;
                            }
                            else if ( ValueComparer.Equals( "nc" , parameter.Name ) )
                            {
                                result.NonceCount = parameter.Value;
                            }
                            else if ( ValueComparer.Equals( "qop" , parameter.Name ) )
                            {
                                result.QualityOfProtection = parameter.Value;
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
