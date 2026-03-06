using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Core;

    public sealed class WWWAuthenticateRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "WWW-Authenticate";

        public static readonly StringRtspHeaderComparer ValueComparer = StringRtspHeaderComparer.IgnoreCaseComparer;
        public static readonly StringRtspHeaderFilter ValueFilter = StringRtspHeaderFilter.UnQuoteFilter;
        public static readonly StringRtspHeaderValidator ValueValidator = StringRtspHeaderValidator.TokenValidator;



        private string _scheme = string.Empty;        
        private string _userName = string.Empty;
        private string _realm = string.Empty;        
        private string _nonce = string.Empty;        
        private string _opaque = string.Empty;        
        private string _response = string.Empty;
        private string _algorithm = string.Empty;
        private string _stale = string.Empty;
        private string _qualityOfProtection = string.Empty;
        private readonly RtspHeaderHashSet _extensions = new RtspHeaderHashSet();

        



        public string Scheme
        {
            get => _scheme;
            set => _scheme = ValueFilter.Filter( value );
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
        
        public string Opaque
        {
            get => _opaque;
            set => _opaque = ValueFilter.Filter( value );
        }
        
        public string Algorithm
        {
            get => _algorithm;
            set => _algorithm = ValueFilter.Filter( value );
        }
        
        public string Stale
        {
            get => _stale;
            set => _stale = ValueFilter.Filter( value );
        }

        public string QualityOfProtection
        {
            get => _qualityOfProtection;
            set => _qualityOfProtection = ValueFilter.Filter( value );
        }

        public IReadOnlyCollection<string> Extensions
        {
            get => _extensions;
        }






        public bool AddExtension( string extension )
        {
            return _extensions.Add( ValueFilter.Filter( extension ) );
        }

        public bool RemoveExtension( string extension )
        {
            return _extensions.Remove( ValueFilter.Filter( extension ) );
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





        public static bool TryParse( string input , out WWWAuthenticateRtspHeader result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( input , " " , out var tokens ) )
            {
                var header = new WWWAuthenticateRtspHeader() { Scheme = tokens.First() };
                
                if ( RtspHeaderParser.TryParse( string.Join( " " , tokens.Skip(1) ) , "," , out tokens ) )
                {
                    foreach ( var token in tokens )
                    {
                        if ( RtspHeaderProperty.TryParse( token , "=" , out var parameter ) )
                        {
                            if ( ValueComparer.Equals( "realm" , parameter.Name ) )
                            {
                                header.Realm = parameter.Value;
                            }
                            else if ( ValueComparer.Equals( "nonce" , parameter.Name ) )
                            {
                                header.Nonce = parameter.Value;
                            }
                            else if ( ValueComparer.Equals( "opaque" , parameter.Name ) )
                            {
                                header.Opaque = parameter.Value;
                            }
                            else if ( ValueComparer.Equals( "algorithm" , parameter.Name ) )
                            {
                                header.Algorithm = parameter.Value;
                            }
                            else if ( ValueComparer.Equals( "stale" , parameter.Name ) )
                            {
                                header.Stale = parameter.Value ;
                            }
                            else if ( ValueComparer.Equals( "qop" , parameter.Name ) )
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
    }
}
