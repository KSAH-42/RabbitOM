using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.DataTypes;
    
    public sealed class WWWAuthenticateRtspHeaderValue
    {
        private static readonly StringComparer ValueComparer = StringComparer.OrdinalIgnoreCase;
        


        private string _scheme = string.Empty;        
        private string _realm = string.Empty;        
        private string _nonce = string.Empty;        
        private string _opaque = string.Empty;        
        private string _algorithm = string.Empty;
        private bool? _stale;
        private string _qualityOfProtection = string.Empty;
        private readonly StringParameterRtspHeaderValueCollection _extensions = new StringParameterRtspHeaderValueCollection();

        



        public string Scheme
        {
            get => _scheme;
            set => _scheme = RtspHeaderValueValidator.EnsureWellFormedToken( RtspHeaderValueSanitizer.UnQuotesWithTrim( value ) );
        }
        
        public string Realm
        {
            get => _realm;
            set => _realm = RtspHeaderValueValidator.EnsureWellFormedToken( RtspHeaderValueSanitizer.UnQuotesWithTrim( value ) );
        }
        
        public string Nonce
        {
            get => _nonce;
            set => _nonce = RtspHeaderValueValidator.EnsureWellFormedToken( RtspHeaderValueSanitizer.UnQuotesWithTrim( value ) );
        }
        
        public string Opaque
        {
            get => _opaque;
            set => _opaque = RtspHeaderValueValidator.EnsureWellFormedToken( RtspHeaderValueSanitizer.UnQuotesWithTrim( value ) );
        }
        
        public string Algorithm
        {
            get => _algorithm;
            set => _algorithm = RtspHeaderValueValidator.EnsureWellFormedToken( RtspHeaderValueSanitizer.UnQuotesWithTrim( value ) );
        }
        
        public bool? Stale
        {
            get => _stale;
            set => _stale = value;
        }

        public string QualityOfProtection
        {
            get => _qualityOfProtection;
            set => _qualityOfProtection = RtspHeaderValueValidator.EnsureWellFormedToken( RtspHeaderValueSanitizer.UnQuotesWithTrim( value ) );
        }

        public StringParameterRtspHeaderValueCollection Extensions
        {
            get => _extensions;
        }



        public static bool TryParse( string input , out WWWAuthenticateRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , " " , out string[] tokens ) )
            {
                var scheme = RtspHeaderValueSanitizer.UnQuotesWithTrim( tokens.FirstOrDefault() );
                
                if ( ! RtspHeaderValueValidator.TryEnsureWellFormedToken( scheme ) )
                {
                    return false;
                }

                var header = new WWWAuthenticateRtspHeaderValue() { _scheme = scheme };
                
                if ( RtspHeaderValueParser.TryParse( string.Join( " " , tokens.Skip(1) ) , "," , out tokens ) )
                {
                    foreach ( var token in tokens )
                    {
                        if ( RtspHeaderValueParser.TryParse( token , "=" , out KeyValuePair<string,string> parameter ) )
                        {
                            if ( ValueComparer.Equals( "realm" , parameter.Key ) )
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
                            else if ( ValueComparer.Equals( "algorithm" , parameter.Key ) )
                            {
                                header._algorithm = RtspHeaderValueSanitizer.UnQuotesWithTrim( parameter.Value );
                            }
                            else if ( ValueComparer.Equals( "stale" , parameter.Key ) )
                            {
                                if ( bool.TryParse( RtspHeaderValueSanitizer.UnQuotesWithTrim( parameter.Value ) , out var value ) )
                                {
                                    header._stale = value;
                                }
                            }
                            else if ( ValueComparer.Equals( "qop" , parameter.Key ) )
                            {
                                header._qualityOfProtection = RtspHeaderValueSanitizer.UnQuotesWithTrim( parameter.Value );
                            }
                            else
                            {
                                if ( StringParameterRtspHeaderValue.TryCreate( parameter.Key , parameter.Value , out var extension ) )
                                {
                                    header._extensions.TryAdd( extension );
                                }
                            }
                        }
                    }

                    if ( ! RtspHeaderValueValidator.TryEnsureWellFormedToken( header.Scheme ) || ! RtspHeaderValueValidator.TryEnsureWellFormedToken( header.Realm ) )
                    {
                        return false;
                    }
                    
                    if ( SupportedTypes.IsDigestAuthentication( header.Scheme ) )
                    {
                        if ( ! RtspHeaderValueValidator.TryEnsureWellFormedToken( header.Nonce ) )
                        {
                            return false;
                        }
                    }

                    result = header;
                }
            }

            return result != null;
        }



        
        public override string ToString()
        {
            if ( string.IsNullOrWhiteSpace( Scheme ) )
            {
                return string.Empty;
            }

            var builder = new StringBuilder();

            builder.AppendFormat( "{0} " , Scheme );

            builder.AppendFormat( "realm=\"{0}\", " , Realm );

            if ( ! string.IsNullOrWhiteSpace( Nonce ) )
            {
                builder.AppendFormat( "nonce=\"{0}\", " , Nonce);
            }

            if ( ! string.IsNullOrWhiteSpace( Opaque ) )
            {
                builder.AppendFormat( "opaque=\"{0}\", " , Opaque );
            }

            if ( ! string.IsNullOrWhiteSpace( Algorithm ) )
            {
                builder.AppendFormat( "algorithm=\"{0}\", " , Algorithm );
            }

            if ( Stale.HasValue )
            {
                builder.AppendFormat( "stale=\"{0}\", " , Stale.Value.ToString().ToLower() );
            }

            if ( ! string.IsNullOrWhiteSpace( QualityOfProtection ) )
            {
                builder.AppendFormat( "qop=\"{0}\", " , QualityOfProtection );
            }

            foreach ( var extension in _extensions )
            {
                builder.AppendFormat( "{0}," , extension );
            }

            return builder.ToString().Trim( ' ' , ',' );
        }
    }
}
