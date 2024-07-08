using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a message header
    /// </summary>
    public sealed class RTSPHeaderWWWAuthenticate : RTSPHeader
    {
        private RTSPAuthenticationType  _type       = RTSPAuthenticationType.Unknown;

        private string                  _realm      = string.Empty;

        private string                  _nonce      = string.Empty;

        private string                  _opaque     = string.Empty;

        private RTSPDigestAlgorithmType _algorithm  = RTSPDigestAlgorithmType.UnDefined;

        private string                  _stale      = string.Empty;



        /// <summary>
        /// Gets the name
        /// </summary>
        public override string Name
        {
            get => RTSPHeaderNames.WWWAuthenticate;
        }

        /// <summary>
        /// Gets / Sets the type
        /// </summary>
        public RTSPAuthenticationType Type
        {
            get => _type;
            set => _type = value;
        }

        /// <summary>
        /// Gets / Sets the realm value
        /// </summary>
        public string Realm
        {
            get => _realm;
            set => _realm = RTSPDataConverter.Trim( value );
        }

        /// <summary>
        /// Gets / Sets the nonce value
        /// </summary>
        public string Nonce
        {
            get => _nonce;
            set => _nonce = RTSPDataConverter.Trim( value );
        }

        /// <summary>
        /// Gets / Sets the opaque
        /// </summary>
        public string Opaque
        {
            get => _opaque;
            set => _opaque = RTSPDataConverter.Trim( value );
        }

        /// <summary>
        /// Gets / Sets the algorithm
        /// </summary>
        public RTSPDigestAlgorithmType Algorithm
        {
            get => _algorithm;
            set => _algorithm = value;
        }

        /// <summary>
        /// Gets / Sets the stale value
        /// </summary>
        public string Stale
        {
            get => _stale;
            set => _stale = RTSPDataConverter.Trim( value );
        }



        /// <summary>
        /// Validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool TryValidate()
        {
            if ( _type == RTSPAuthenticationType.Basic )
            {
                return !string.IsNullOrWhiteSpace( _realm );
            }

            if ( _type == RTSPAuthenticationType.Digest )
            {
                return !string.IsNullOrWhiteSpace( _realm )
                    && !string.IsNullOrWhiteSpace( _nonce );
            }

            return false;
        }

        /// <summary>
        /// Returns an empty string
        /// </summary>
        /// <returns>returns a string value</returns>
        public override string ToString()
        {
            if ( _type == RTSPAuthenticationType.Unknown )
            {
                return string.Empty;
            }

            var writer = new RTSPHeaderWriter()
            {
                Operator      = RTSPOperator.Equality ,
                Separator     = RTSPSeparator.Comma ,
                IncludeQuotes = true ,
            };

            writer.Write( _type );
            writer.WriteSpace();

            writer.WriteField( RTSPHeaderFieldNames.Realm , _realm );

            if ( !string.IsNullOrWhiteSpace( _nonce ) )
            {
                writer.WriteSeparator();
                writer.WriteSpace();
                writer.WriteField( RTSPHeaderFieldNames.Nonce , _nonce );
            }

            if ( !string.IsNullOrWhiteSpace( _opaque ) )
            {
                writer.WriteSeparator();
                writer.WriteSpace();
                writer.WriteField( RTSPHeaderFieldNames.Algorithm , _opaque );
            }

            if ( _algorithm != RTSPDigestAlgorithmType.UnDefined )
            {
                writer.WriteSeparator();
                writer.WriteSpace();
                writer.WriteField( RTSPHeaderFieldNames.Algorithm , RTSPDataConverter.ConvertToString( _algorithm ) );
            }

            if ( !string.IsNullOrWhiteSpace( _stale ) )
            {
                writer.WriteSeparator();
                writer.WriteSpace();
                writer.WriteField( RTSPHeaderFieldNames.Stale , _stale );
            }

            return writer.Output;
        }

        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="value">the header value</param>
        /// <param name="result">the output result</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public static bool TryParse( string value , out RTSPHeaderWWWAuthenticate result )
        {
            result = null;

            var parser = new RTSPParser( value , RTSPSeparator.Comma );

            if ( parser.ParseFirstElement() )
            {
                var type = parser.FirstAuthenticationTypeOrDefault();

                if ( !parser.RemoveFirstSequence( type.ToString() ) || type == RTSPAuthenticationType.Unknown )
                {
                    return false;
                }

                if ( !parser.ParseHeaders() )
                {
                    return false;
                }

                using ( var reader = new RTSPHeaderReader( parser.GetParsedHeaders() ) )
                {
                    reader.Operator = RTSPOperator.Equality;
                    reader.IncludeQuotes = true;

                    result = new RTSPHeaderWWWAuthenticate()
                    {
                        Type = type
                    };

                    while ( reader.Read() )
                    {
                        if ( !reader.SplitElementAsField() )
                        {
                            continue;
                        }

                        if ( reader.IsRealmElementType )
                        {
                            result.Realm = reader.GetElementValue();
                        }

                        if ( reader.IsNonceElementType )
                        {
                            result.Nonce = reader.GetElementValue();
                        }

                        if ( reader.IsOpaqueElementType )
                        {
                            result.Opaque = reader.GetElementValue();
                        }

                        if ( reader.IsAlgorithmElementType )
                        {
                            result.Algorithm = RTSPDataConverter.ConvertToEnumDigestAlgorithmType( reader.GetElementValue() );
                        }

                        if ( reader.IsStaleElementType )
                        {
                            result.Stale = reader.GetElementValue();
                        }
                    }

                    return true;
                }
            }

            return false;
        }
    }
}
