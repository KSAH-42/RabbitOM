using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters;
   
    public sealed class AuthenticateRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "WWW-Authenticate";

        public static readonly StringComparer ValueComparer = StringComparer.OrdinalIgnoreCase;
        public static readonly StringValueAdapter ValueAdapter = StringValueAdapter.TrimWithUnQuoteAdapter;
       


        private string _scheme = string.Empty;        
        private string _realm = string.Empty;        
        private string _nonce = string.Empty;        
        private string _opaque = string.Empty;        
        private string _algorithm = string.Empty;
        private bool? _stale;
        private string _qualityOfProtection = string.Empty;
        private readonly HashSet<string> _extensions = new HashSet<string>( StringComparer.OrdinalIgnoreCase );

        



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
        
        public bool? Stale
        {
            get => _stale;
            set => _stale = value;
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



        public static bool TryParse( string input , out AuthenticateRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( input , " " , out string[] tokens ) )
            {
                var scheme = tokens.FirstOrDefault();
                
                if ( ! RtspHeaderProtocolValidator.IsValidToken( scheme ) )
                {
                    return false;
                }

                var header = new AuthenticateRtspHeaderValue() { Scheme = scheme };
                
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
                                if ( bool.TryParse( ValueAdapter.Adapt( parameter.Value ) , out var value ) )
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
                                header.AddExtension( token );
                            }
                        }
                    }

                    if ( ! RtspHeaderProtocolValidator.IsValidToken( header.Scheme ) || ! RtspHeaderProtocolValidator.IsValidToken( header.Realm ) )
                    {
                        return false;
                    }
                    
                    if ( RtspAuthenticationTypes.IsDigestAuthentication( header.Scheme ) )
                    {
                        if ( ! RtspHeaderProtocolValidator.IsValidToken( header.Nonce ) )
                        {
                            return false;
                        }
                    }

                    result = header;
                }
            }

            return result != null;
        }



        public bool AddExtension( string extension )
        {
            if ( RtspHeaderProtocolValidator.IsValid( extension = ValueAdapter.Adapt( extension ) ) )
            {
                return _extensions.Add( extension );
            }
            
            return false;
        }

        public bool RemoveExtension( string extension )
        {
            return _extensions.Remove( ValueAdapter.Adapt( extension ) );
        }

        public void RemoveExtensions()
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
