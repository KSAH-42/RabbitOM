using System;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    /// <summary>
    /// Represent a rtsp header
    /// </summary>
    public sealed class AuthorizationRtspHeader : RtspHeader
    {
        /// <summary>
        /// The type name
        /// </summary>
        public const string TypeName = "Authorization";






        private string _type      = string.Empty;
        private string _username  = string.Empty;
        private string _realm     = string.Empty;
        private string _nonce     = string.Empty;
        private string _domain    = string.Empty;
        private string _opaque    = string.Empty;
        private string _uri       = string.Empty;
        private string _response  = string.Empty;
        private string _algorithm = string.Empty;
        private string _qpop      = string.Empty;
        private string _cnonce    = string.Empty;






        /// <summary>
        /// Gets / Sets the type
        /// </summary>
        public string Type
        {
            get => _type;
            set => _type = StringRtspNormalizer.Normalize( value );
        }

        /// <summary>
        /// Gets / Sets the user name
        /// </summary>
        public string UserName
        {
            get => _username;
            set => _username = StringRtspNormalizer.Normalize( value );
        }

        /// <summary>
        /// Gets / Sets the realm
        /// </summary>
        public string Realm
        {
            get => _realm;
            set => _realm = StringRtspNormalizer.Normalize( value );
        }

        /// <summary>
        /// Gets / Sets the nonce
        /// </summary>
        public string Nonce
        {
            get => _nonce;
            set => _nonce = StringRtspNormalizer.Normalize( value );
        }

        /// <summary>
        /// Gets / Sets the domain
        /// </summary>
        public string Domain
        {
            get => _domain;
            set => _domain = StringRtspNormalizer.Normalize( value );
        }

        /// <summary>
        /// Gets / Sets the opaque
        /// </summary>
        public string Opaque
        {
            get => _opaque;
            set => _opaque = StringRtspNormalizer.Normalize( value );
        }

        /// <summary>
        /// Gets / Sets the uri
        /// </summary>
        public string Uri
        {
            get => _uri;
            set => _uri = StringRtspNormalizer.Normalize( value );
        }

        /// <summary>
        /// Gets / Sets the response
        /// </summary>
        public string Response
        {
            get => _response;
            set => _response = StringRtspNormalizer.Normalize( value );
        }
        
        /// <summary>
        /// Gets / Sets the algorithm
        /// </summary>
        public string Algorithm
        {
            get => _algorithm;
            set => _algorithm = StringRtspNormalizer.Normalize( value );
        }

        /// <summary>
        /// Gets / Sets the qpop
        /// </summary>
        public string QPop
        {
            get => _qpop;
            set => _qpop = StringRtspNormalizer.Normalize( value );
        }

        /// <summary>
        /// Gets / Sets the cnonce
        /// </summary>
        public string CNonce
        {
            get => _cnonce;
            set => _cnonce = StringRtspNormalizer.Normalize( value );
        }







        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="input">the input</param>
        /// <param name="result">the result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryParse( string input , out AuthorizationRtspHeader result )
        {
            result = null;

            var text = StringRtspNormalizer.Normalize( input );

            if ( RtspHeaderParser.TryParse( text , " " , out var tokens ) )
            {
                var type = tokens.FirstOrDefault();

                if ( string.IsNullOrEmpty( type ) )
                {
                    return false;
                }

                if ( RtspHeaderParser.TryParse( text.Replace( type , "" ) , "," , out tokens ) )
                {
                    result = new AuthorizationRtspHeader()
                    {
                        Type = type
                    };

                    foreach ( var token in tokens )
                    {
                        if ( RtspHeaderParser.TryParse( token , "=" , out var fields ) )
                        {
                            var fieldName  = fields.ElementAtOrDefault( 0 ) ?? string.Empty;
                            var fieldValue = fields.ElementAtOrDefault( 1 ) ?? string.Empty;
                    
                            if ( fieldName.Equals( "username" , StringComparison.OrdinalIgnoreCase ) )
                            {
                                result.UserName = fieldValue;
                            }
                            else if ( fieldName.Equals( "realm" , StringComparison.OrdinalIgnoreCase ) )
                            {
                                result.Realm = fieldValue;
                            }
                            else if ( fieldName.Equals( "nonce" , StringComparison.OrdinalIgnoreCase ) )
                            {
                                result.Nonce = fieldValue;
                            }
                            else if ( fieldName.Equals( "domain" , StringComparison.OrdinalIgnoreCase ) )
                            {
                                result.Domain = fieldValue;
                            }
                            else if ( fieldName.Equals( "opaque" , StringComparison.OrdinalIgnoreCase ) )
                            {
                                result.Opaque = fieldValue;
                            }
                            else if ( fieldName.Equals( "uri" , StringComparison.OrdinalIgnoreCase ) )
                            {
                                result.Uri = fieldValue;
                            }
                            else if ( fieldName.Equals( "response" , StringComparison.OrdinalIgnoreCase ) )
                            {
                                result.Response = fieldValue;
                            }
                            else if ( fieldName.Equals( "algorithm" , StringComparison.OrdinalIgnoreCase ) )
                            {
                                result.Algorithm = fieldValue;
                            }
                            else if ( fieldName.Equals( "qpop" , StringComparison.OrdinalIgnoreCase ) )
                            {
                                result.QPop = fieldValue;
                            }
                            else if ( fieldName.Equals( "cnonce" , StringComparison.OrdinalIgnoreCase ) )
                            {
                                result.CNonce = fieldValue;
                            }
                        }
                    }
                }
            }

            return result != null;
        }







        /// <summary>
        /// Try to validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool TryValidate()
        {
            if ( RtspAuthenticationTypes.IsBasicAuthentication( _type ) )
            {
                return StringRtspValidator.TryValidate( _response );
            }

            return RtspAuthenticationTypes.IsDigestAuthentication( _type ) ? 
                   StringRtspValidator.TryValidate( _username )
                || StringRtspValidator.TryValidate( _realm )
                || StringRtspValidator.TryValidate( _nonce )
                || StringRtspValidator.TryValidate( _response )
                || StringRtspValidator.TryValidateUri( _uri )
                 : false
                 ;
        }

        /// <summary>
        /// Format to string
        /// </summary>
        /// <returns>returns a string</returns>
        public override string ToString()
        {
            if ( string.IsNullOrWhiteSpace( _type ) )
            {
                return string.Empty;
            }

            var builder = new StringBuilder();

            builder.AppendFormat( "{0} " , _type );

            if ( RtspAuthenticationTypes.IsBasicAuthentication( _type ) )
            {
                builder.Append( _response );
            }
            else if ( RtspAuthenticationTypes.IsDigestAuthentication( _type ) )
            {
                builder.AppendFormat( "username=\"{0}\", " , _username );
                builder.AppendFormat( "realm=\"{0}\", " , _realm );
                builder.AppendFormat( "nonce=\"{0}\", " , _nonce );

                if ( ! string.IsNullOrWhiteSpace( _domain ) )
                {
                    builder.AppendFormat( "domain=\"{0}\", " , _domain );
                }

                if ( ! string.IsNullOrWhiteSpace( _opaque ) )
                {
                    builder.AppendFormat( "opaque=\"{0}\", " , _opaque );
                }

                builder.AppendFormat( "uri=\"{0}\", " , _uri );

                builder.AppendFormat( "response=\"{0}\" " , _response );

                if ( ! string.IsNullOrWhiteSpace( _algorithm ) )
                {
                    builder.AppendFormat( "algorithm=\"{0}\", " , _algorithm );
                }

                if ( ! string.IsNullOrWhiteSpace( _qpop ) )
                {
                    builder.AppendFormat( "qpop=\"{0}\", " , _qpop );
                }

                if ( ! string.IsNullOrWhiteSpace( _cnonce ) )
                {
                    builder.AppendFormat( "cnonce=\"{0}\", " , _cnonce );
                }
            }

            while ( builder[ builder.Length - 1 ] == ',' || builder[ builder.Length - 1 ] == ' ' )
            {
                builder.Remove( builder.Length - 1 , 1 );
            }

            return builder.ToString();
        }
    }
}
