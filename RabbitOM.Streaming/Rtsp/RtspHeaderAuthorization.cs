using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent a message header
    /// </summary>
    public sealed class RtspHeaderAuthorization : RtspHeader
    {
        private RtspAuthenticationType  _type      = RtspAuthenticationType.Unknown;

        private string                  _userName  = string.Empty;

        private string                  _realm     = string.Empty;

        private string                  _nonce     = string.Empty;

        private string                  _domain    = string.Empty;

        private string                  _opaque    = string.Empty;

        private string                  _uri       = string.Empty;

        private string                  _response  = string.Empty;



        /// <summary>
        /// Gets the name
        /// </summary>
        public override string Name
        {
            get => RtspHeaderNames.Authorization;
        }

        /// <summary>
        /// Gets / Sets the type
        /// </summary>
        public RtspAuthenticationType Type
        {
            get => _type;
            set => _type = value;
        }

        /// <summary>
        /// Gets / Sets the user name
        /// </summary>
        public string UserName
        {
            get => _userName;
            set => _userName = RtspDataConverter.Trim( value );
        }

        /// <summary>
        /// Gets / Sets the realm value
        /// </summary>
        public string Realm
        {
            get => _realm;
            set => _realm = RtspDataConverter.Trim( value );
        }

        /// <summary>
        /// Gets / Sets the nonce value
        /// </summary>
        public string Nonce
        {
            get => _nonce;
            set => _nonce = RtspDataConverter.Trim( value );
        }

        /// <summary>
        /// Gets / Sets the domain
        /// </summary>
        public string Domain
        {
            get => _domain;
            set => _domain = RtspDataConverter.Trim( value );
        }

        /// <summary>
        /// Gets / Sets the opaque
        /// </summary>
        public string Opaque
        {
            get => _opaque;
            set => _opaque = RtspDataConverter.Trim( value );
        }

        /// <summary>
        /// Gets / Sets the uri
        /// </summary>
        public string Uri
        {
            get => _uri;
            set => _uri = RtspDataConverter.Trim( value );
        }

        /// <summary>
        /// Gets / Sets the response
        /// </summary>
        public string Response
        {
            get => _response;
            set => _response = RtspDataConverter.Trim( value );
        }



        /// <summary>
        /// Validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool TryValidate()
        {
            if ( _type == RtspAuthenticationType.Basic )
            {
                return !string.IsNullOrWhiteSpace( _response );
            }

            if ( _type == RtspAuthenticationType.Digest )
            {
                return !string.IsNullOrWhiteSpace( _userName )
                    || !string.IsNullOrWhiteSpace( _realm )
                    || !string.IsNullOrWhiteSpace( _nonce )
                    || !string.IsNullOrWhiteSpace( _uri )
                    || !string.IsNullOrWhiteSpace( _response );
            }

            return false;
        }

        /// <summary>
        /// Returns an empty string
        /// </summary>
        /// <returns>returns a string value</returns>
        public override string ToString()
        {
            var writer = new RtspHeaderWriter()
            {
                Separator     = RtspSeparator.Comma ,
                Operator      = RtspOperator.Equality,
                IncludeQuotes = true
            };

            writer.Write( _type.ToString() );
            writer.WriteSpace();

            if ( _type == RtspAuthenticationType.Basic )
            {
                writer.Write( _response );

                return writer.Output;
            }

            if ( _type == RtspAuthenticationType.Digest )
            {
                writer.WriteField( RtspHeaderFieldNames.UserName , _userName );
                writer.WriteSeparator();
                writer.WriteSpace();

                writer.WriteField( RtspHeaderFieldNames.Realm , _realm );
                writer.WriteSeparator();
                writer.WriteSpace();

                writer.WriteField( RtspHeaderFieldNames.Nonce , _nonce );
                writer.WriteSeparator();
                writer.WriteSpace();

                if ( !string.IsNullOrWhiteSpace( _domain ) )
                {
                    writer.WriteField( RtspHeaderFieldNames.Domain , _domain );
                    writer.WriteSeparator();
                    writer.WriteSpace();
                }

                if ( !string.IsNullOrWhiteSpace( _opaque ) )
                {
                    writer.WriteField( RtspHeaderFieldNames.Opaque , _opaque );
                    writer.WriteSeparator();
                    writer.WriteSpace();
                }

                writer.WriteField( RtspHeaderFieldNames.Uri , _uri );
                writer.WriteSeparator();
                writer.WriteSpace();

                writer.WriteField( RtspHeaderFieldNames.Response , _response );

                return writer.Output;
            }

            return string.Empty;
        }

        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="value">the header value</param>
        /// <param name="result">the output result</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public static bool TryParse( string value , out RtspHeaderAuthorization result )
        {
            result = null;

            var parser = new RtspParser( value , RtspSeparator.Comma );

            if ( parser.ParseFirstElement() )
            {
                var type = parser.FirstAuthenticationTypeOrDefault();

                if ( !parser.RemoveFirstSequence( type.ToString() ) )
                {
                    return false;
                }

                if ( type == RtspAuthenticationType.Basic )
                {
                    return TryParseAsBasic( parser.Text , out result );
                }

                if ( !parser.ParseHeaders() )
                {
                    return false;
                }

                if ( type == RtspAuthenticationType.Digest )
                {
                    return TryParseAsDigest( parser.GetParsedHeaders() , out result );
                }
            }

            return false;
        }

        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="value">the input value</param>
        /// <param name="result">the output result</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        private static bool TryParseAsBasic( string value , out RtspHeaderAuthorization result )
        {
            result = null;

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            result = new RtspHeaderAuthorization()
            {
                Type = RtspAuthenticationType.Basic ,
                Response = value ,
            };

            return true;
        }

        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="headers">the collection of headers</param>
        /// <param name="result">the output result</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        private static bool TryParseAsDigest( IEnumerable<string> headers , out RtspHeaderAuthorization result )
        {
            result = null;

            if ( headers == null )
            {
                return false;
            }

            using ( var reader = new RtspHeaderReader( headers ) )
            {
                reader.Operator = RtspOperator.Equality;
                reader.IncludeQuotes = true;

                result = new RtspHeaderAuthorization()
                {
                    Type = RtspAuthenticationType.Digest
                };

                while ( reader.Read() )
                {
                    if ( !reader.SplitElementAsField() )
                    {
                        continue;
                    }

                    if ( reader.IsUserNameElementType )
                    {
                        result.UserName = reader.GetElementValue();
                    }

                    if ( reader.IsClientPortElementType )
                    {
                        result.UserName = reader.GetElementValue();
                    }

                    if ( reader.IsRealmElementType )
                    {
                        result.Realm = reader.GetElementValue();
                    }

                    if ( reader.IsNonceElementType )
                    {
                        result.Nonce = reader.GetElementValue();
                    }

                    if ( reader.IsUriElementType )
                    {
                        result.Uri = reader.GetElementValue();
                    }

                    if ( reader.IsResponseElementType )
                    {
                        result.Response = reader.GetElementValue();
                    }

                    if ( reader.IsDomainElementType )
                    {
                        result.Domain = reader.GetElementValue();
                    }

                    if ( reader.IsOpaqueElementType )
                    {
                        result.Opaque = reader.GetElementValue();
                    }
                }

                return true;
            }
        }
    }
}
