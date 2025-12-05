using System;

namespace RabbitOM.Streaming.Net.Rtsp
{
    /// <summary>
    /// Represent a message header
    /// </summary>
    public sealed class RtspHeaderWWWAuthenticate : RtspHeader
    {
        private RtspAuthenticationType  _type       = RtspAuthenticationType.Unknown;

        private string                  _realm      = string.Empty;

        private string                  _nonce      = string.Empty;

        private string                  _opaque     = string.Empty;

        private RtspDigestAlgorithmType _algorithm  = RtspDigestAlgorithmType.UnDefined;

        private string                  _stale      = string.Empty;






        /// <summary>
        /// Gets the name
        /// </summary>
        public override string Name
        {
            get => RtspHeaderNames.WWWAuthenticate;
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
        /// Gets / Sets the opaque
        /// </summary>
        public string Opaque
        {
            get => _opaque;
            set => _opaque = RtspDataConverter.Trim( value );
        }

        /// <summary>
        /// Gets / Sets the algorithm
        /// </summary>
        public RtspDigestAlgorithmType Algorithm
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
            set => _stale = RtspDataConverter.Trim( value );
        }






        /// <summary>
        /// Try validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool TryValidate()
        {
            if ( _type == RtspAuthenticationType.Basic )
            {
                return !string.IsNullOrWhiteSpace( _realm );
            }

            if ( _type == RtspAuthenticationType.Digest )
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
            if ( _type == RtspAuthenticationType.Unknown )
            {
                return string.Empty;
            }

            var writer = new RtspHeaderWriter()
            {
                Operator      = RtspOperator.Equality ,
                Separator     = RtspSeparator.Comma ,
                IncludeQuotes = true ,
            };

            writer.Write( _type );
            writer.WriteSpace();

            writer.WriteField( RtspHeaderFieldNames.Realm , _realm );

            if ( !string.IsNullOrWhiteSpace( _nonce ) )
            {
                writer.WriteSeparator();
                writer.WriteSpace();
                writer.WriteField( RtspHeaderFieldNames.Nonce , _nonce );
            }

            if ( !string.IsNullOrWhiteSpace( _opaque ) )
            {
                writer.WriteSeparator();
                writer.WriteSpace();
                writer.WriteField( RtspHeaderFieldNames.Algorithm , _opaque );
            }

            if ( _algorithm != RtspDigestAlgorithmType.UnDefined )
            {
                writer.WriteSeparator();
                writer.WriteSpace();
                writer.WriteField( RtspHeaderFieldNames.Algorithm , RtspDataConverter.ConvertToString( _algorithm ) );
            }

            if ( !string.IsNullOrWhiteSpace( _stale ) )
            {
                writer.WriteSeparator();
                writer.WriteSpace();
                writer.WriteField( RtspHeaderFieldNames.Stale , _stale );
            }

            return writer.Output;
        }






        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="value">the header value</param>
        /// <param name="result">the output result</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public static bool TryParse( string value , out RtspHeaderWWWAuthenticate result )
        {
            result = null;

            var parser = new RtspParser( value , RtspSeparator.Comma );

            if ( parser.ParseFirstElement() )
            {
                var type = parser.FirstAuthenticationTypeOrDefault();

                if ( ! parser.RemoveFirstSequence( type.ToString() ) || type == RtspAuthenticationType.Unknown )
                {
                    return false;
                }

                if ( ! parser.ParseHeaders() )
                {
                    return false;
                }

                using ( var reader = new RtspHeaderReader( parser.GetParsedHeaders() ) )
                {
                    reader.Operator = RtspOperator.Equality;
                    reader.IncludeQuotes = true;

                    result = new RtspHeaderWWWAuthenticate()
                    {
                        Type = type
                    };

                    while ( reader.Read() )
                    {
                        if ( ! reader.SplitElementAsField() )
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
                            result.Algorithm = RtspDataConverter.ConvertToEnumDigestAlgorithmType( reader.GetElementValue() );
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
