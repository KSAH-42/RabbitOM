using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types;
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Compliances;

    public sealed class WWWAuthenticateRtspHeaderValue
    {
        private static readonly StringComparer ValueComparer = StringComparer.OrdinalIgnoreCase;
        private static readonly StringValueNormalizer ValueNormalizer = StringValueNormalizer.TrimWithUnQuoteNormalizer;
        private static readonly StringValueValidator ValueValidator = StringValueValidator.DefaultValidator;
       


        private string _scheme = string.Empty;        
        private string _realm = string.Empty;        
        private string _nonce = string.Empty;        
        private string _opaque = string.Empty;        
        private string _algorithm = string.Empty;
        private bool? _stale;
        private string _qualityOfProtection = string.Empty;
        private readonly StringCollection _extensions = new StringCollection( ValueNormalizer , ValueValidator.TryValidate );

        



        public string Scheme
        {
            get => _scheme;
            set => _scheme = ValueNormalizer.Normalize( value );
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
        
        public string Opaque
        {
            get => _opaque;
            set => _opaque = ValueNormalizer.Normalize( value );
        }
        
        public string Algorithm
        {
            get => _algorithm;
            set => _algorithm = ValueNormalizer.Normalize( value );
        }
        
        public bool? Stale
        {
            get => _stale;
            set => _stale = value;
        }

        public string QualityOfProtection
        {
            get => _qualityOfProtection;
            set => _qualityOfProtection = ValueNormalizer.Normalize( value );
        }

        public StringCollection Extensions
        {
            get => _extensions;
        }



        

        public static bool TryParse( string input , out WWWAuthenticateRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , " " , out string[] tokens ) )
            {
                var scheme = tokens.FirstOrDefault();
                
                if ( ! ValueValidator.TryValidate( scheme ) )
                {
                    return false;
                }

                var header = new WWWAuthenticateRtspHeaderValue() { Scheme = scheme };
                
                if ( RtspHeaderValueParser.TryParse( string.Join( " " , tokens.Skip(1) ) , "," , out tokens ) )
                {
                    foreach ( var token in tokens )
                    {
                        if ( RtspHeaderValueParser.TryParse( token , "=" , out KeyValuePair<string,string> parameter ) )
                        {
                            if ( ValueComparer.Equals( "realm" , parameter.Key ) )
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
                            else if ( ValueComparer.Equals( "algorithm" , parameter.Key ) )
                            {
                                header.Algorithm = parameter.Value;
                            }
                            else if ( ValueComparer.Equals( "stale" , parameter.Key ) )
                            {
                                if ( bool.TryParse( ValueNormalizer.Normalize( parameter.Value ) , out var value ) )
                                {
                                    header.Stale = value;
                                }
                            }
                            else if ( ValueComparer.Equals( "qop" , parameter.Key ) )
                            {
                                header.QualityOfProtection = parameter.Value;
                            }
                            else
                            {
                                header.Extensions.TryAdd( token );
                            }
                        }
                    }

                    if ( ! ValueValidator.TryValidate( header.Scheme ) || ! ValueValidator.TryValidate( header.Realm ) )
                    {
                        return false;
                    }
                    
                    if ( AuthenticationTypes.IsDigestAuthentication( header.Scheme ) )
                    {
                        if ( ! ValueValidator.TryValidate( header.Nonce ) )
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
