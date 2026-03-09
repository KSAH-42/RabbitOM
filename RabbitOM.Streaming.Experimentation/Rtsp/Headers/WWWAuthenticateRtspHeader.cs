using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters;
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Validation;

    public sealed class WWWAuthenticateRtspHeader
    {
        public static readonly string TypeName = "WWW-Authenticate";

        public static readonly StringComparer ValueComparer = StringComparer.OrdinalIgnoreCase;
        public static readonly StringValueAdapter ValueAdapter = StringValueAdapter.TrimWithUnQuoteAdapter;
        public static readonly StringValueValidator ValueValidator = StringValueValidator.TokenValidator;



        private string _scheme = string.Empty;        
        private string _userName = string.Empty;
        private string _realm = string.Empty;        
        private string _nonce = string.Empty;        
        private string _opaque = string.Empty;        
        private string _response = string.Empty;
        private string _algorithm = string.Empty;
        private string _stale = string.Empty;
        private string _qualityOfProtection = string.Empty;
        private readonly StringRtspHashSet _extensions = new StringRtspHashSet();

        



        public string Scheme
        {
            get => _scheme;
            set => _scheme = ValueAdapter.Adapt( value );
        }
        
        public string Realm
        {
            get => _realm;
            set => _realm = ValueAdapter.Adapt( value );
        }
        
        public string Nonce
        {
            get => _nonce;
            set => _nonce = ValueAdapter.Adapt( value );
        }
        
        public string Opaque
        {
            get => _opaque;
            set => _opaque = ValueAdapter.Adapt( value );
        }
        
        public string Algorithm
        {
            get => _algorithm;
            set => _algorithm = ValueAdapter.Adapt( value );
        }
        
        public string Stale
        {
            get => _stale;
            set => _stale = ValueAdapter.Adapt( value );
        }

        public string QualityOfProtection
        {
            get => _qualityOfProtection;
            set => _qualityOfProtection = ValueAdapter.Adapt( value );
        }

        public IReadOnlyCollection<string> Extensions
        {
            get => _extensions;
        }



        public static bool TryParse( string input , out WWWAuthenticateRtspHeader result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( input , " " , out string[] tokens ) )
            {
                var scheme = tokens.FirstOrDefault();
                
                if ( ! ValueValidator.TryValidate( scheme ) )
                {
                    return false;
                }
                
                var header = new WWWAuthenticateRtspHeader() { Scheme = scheme };
                
                if ( RtspHeaderParser.TryParse( string.Join( " " , tokens.Skip(1) ) , "," , out tokens ) )
                {
                    foreach ( var token in tokens )
                    {
                        if ( RtspHeaderParser.TryParse( token , "=" , out KeyValuePair<string,string> parameter ) )
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
                                header.Stale = parameter.Value ;
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

                    if ( ValueValidator.TryValidate( header.Scheme ) )
                    {
                        result = header;
                    }
                }
            }

            return result != null;
        }



        public bool AddExtension( string extension )
        {
            return _extensions.Add( ValueAdapter.Adapt( extension ) );
        }

        public bool RemoveExtension( string extension )
        {
            return _extensions.Remove( ValueAdapter.Adapt( extension ) );
        }

        public void ClearExtensions()
        {
            _extensions.Clear();
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

            if ( ! string.IsNullOrWhiteSpace( Stale ) )
            {
                builder.AppendFormat( "stale=\"{0}\", " , Stale );
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
