using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.RtspV2.Headers
{
    using RabbitOM.Streaming.RtspV2.Headers.DataTypes;

    public sealed class AuthorizationRtspHeaderValue
    {
        private static readonly StringComparer ValueComparer = StringComparer.OrdinalIgnoreCase;

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
        private readonly StringRtspHeaderValueCollection _extensions = new StringRtspHeaderValueCollection();





        public string Scheme
        {
            get => _scheme;
            set => _scheme = EnsureValue( value );
        }

        public string UserName
        {
            get => _userName;
            set => _userName = EnsureValue( value );
        }

        public string Realm
        {
            get => _realm;
            set => _realm = EnsureValue( value );
        }

        public string Nonce
        {
            get => _nonce;
            set => _nonce = EnsureValue( value );
        }

        public string Domain
        {
            get => _domain;
            set => _domain = EnsureValue( value );
        }

        public string Uri
        {
            get => _uri;
            set => _uri = EnsureValue( value );
        }

        public string Opaque
        {
            get => _opaque;
            set => _opaque = EnsureValue( value );
        }

        public string Response
        {
            get => _response;
            set => _response = EnsureValue( value );
        }

        public string Algorithm
        {
            get => _algorithm;
            set => _algorithm = EnsureValue( value );
        }

        public string NonceCount
        {
            get => _nonceCount;
            set => _nonceCount = EnsureValue( value );
        }

        public string ClientNonce
        {
            get => _clientNonce;
            set => _clientNonce = EnsureValue( value );
        }

        public string QualityOfProtection
        {
            get => _qualityOfProtection;
            set => _qualityOfProtection = EnsureValue( value );
        }

        public StringRtspHeaderValueCollection Extensions
        {
            get => _extensions;
        }





        private static string EnsureValue( string value )
        {
            return RtspHeaderValueValidator.EnsureWellFormed( RtspHeaderValueSanitizer.UnQuotesWithTrim( value ) );
        }

        private static bool IsWellFormedValue( string value )
        {
            return RtspHeaderValueValidator.IsWellFormed( RtspHeaderValueSanitizer.UnQuotesWithTrim( value ) );
        }

        public static bool TryParse( string input , out AuthorizationRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , " " , out string[] tokens ) )
            {
                var header = new AuthorizationRtspHeaderValue()
                {
                    _scheme = RtspHeaderValueSanitizer.UnQuotesWithTrim( tokens.FirstOrDefault() ) 
                };

                if ( RtspHeaderValueParser.TryParse( string.Join( " " , tokens.Skip( 1 ) ) , "," , out tokens ) )
                {
                    foreach ( var token in tokens )
                    {
                        if ( RtspHeaderValueParser.TryParse( token , "=" , out KeyValuePair<string,string> parameter ) )
                        {
                            if ( ValueComparer.Equals( "username" , parameter.Key ) )
                            {
                                header._userName = RtspHeaderValueSanitizer.UnQuotesWithTrim( parameter.Value );
                            }
                            else if ( ValueComparer.Equals( "realm" , parameter.Key ) )
                            {
                                header._realm = RtspHeaderValueSanitizer.UnQuotesWithTrim( parameter.Value );
                            }
                            else if ( ValueComparer.Equals( "nonce" , parameter.Key ) )
                            {
                                header._nonce = RtspHeaderValueSanitizer.UnQuotesWithTrim( parameter.Value );
                            }
                            else if ( ValueComparer.Equals( "opaque" , parameter.Key ) )
                            {
                                header._opaque = RtspHeaderValueSanitizer.UnQuotesWithTrim( parameter.Value );
                            }
                            else if ( ValueComparer.Equals( "domain" , parameter.Key ) )
                            {
                                header._domain = RtspHeaderValueSanitizer.UnQuotesWithTrim( parameter.Value );
                            }
                            else if ( ValueComparer.Equals( "uri" , parameter.Key ) )
                            {
                                header._uri = RtspHeaderValueSanitizer.UnQuotesWithTrim( parameter.Value );
                            }
                            else if ( ValueComparer.Equals( "response" , parameter.Key ) )
                            {
                                header._response = RtspHeaderValueSanitizer.UnQuotesWithTrim( parameter.Value );
                            }                            
                            else if ( ValueComparer.Equals( "algorithm" , parameter.Key ) )
                            {
                                header._algorithm = RtspHeaderValueSanitizer.UnQuotesWithTrim( parameter.Value );
                            }
                            else if ( ValueComparer.Equals( "cnonce" , parameter.Key ) )
                            {
                                header._clientNonce = RtspHeaderValueSanitizer.UnQuotesWithTrim( parameter.Value );
                            }
                            else if ( ValueComparer.Equals( "nc" , parameter.Key ) )
                            {
                                header._nonceCount = RtspHeaderValueSanitizer.UnQuotesWithTrim( parameter.Value );
                            }
                            else if ( ValueComparer.Equals( "qop" , parameter.Key ) )
                            {
                                header._qualityOfProtection = RtspHeaderValueSanitizer.UnQuotesWithTrim( parameter.Value );
                            }
                            else
                            {
                                header._extensions.TryAdd( RtspHeaderValueSanitizer.UnQuotesWithTrim( token ) );
                            }
                        }
                    }

                    if ( IsWellFormedValue( header._scheme ) )
                    {
                        result = header;
                    }
                }
            }

            return result != null;
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
                if ( RtspHeaderValueParser.TryParse( extension , "=" , out KeyValuePair<string,string> parameter ) )
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
