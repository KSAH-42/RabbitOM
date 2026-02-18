using System;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Formatting;

    public sealed class WWWAuthenticateRtspHeader
    {
        public static readonly string TypeName = "WWW-Authenticate";





        public string Scheme { get; private set; } = string.Empty;
        
        public string Realm { get; private set; } = string.Empty;
        
        public string Nonce { get; private set; } = string.Empty;
        
        public string Opaque { get; private set; } = string.Empty;
        
        public string Algorithm { get; private set; } = string.Empty;
        
        public string Stale { get; private set; } = string.Empty;

        public string QualityOfProtection { get; private set; } = string.Empty;







        public static bool TryParse( string input , out WWWAuthenticateRtspHeader result )
        {
            result = null;

            input = RtspValueNormalizer.Normalize( input );

            if ( StringRtspHeaderParser.TryParse( input , ' ' , out var tokens ) )
            {
                var scheme = tokens.First();
                
                if ( StringRtspHeaderParser.TryParse( input.Replace( scheme , "" ) , ',' , out tokens ) )
                {
                    result = new WWWAuthenticateRtspHeader(); 
                    
                    result.SetScheme( scheme );

                    foreach ( var token in tokens )
                    {
                        if ( StringParameterRtspHeaderParser.TryParse( token , '=' , out var parameter ) )
                        {
                            if ( string.Equals( "realm" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                            {
                                result.SetRealm( parameter.Value );
                            }
                            else if ( string.Equals( "nonce" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                            {
                                result.SetNonce( parameter.Value );
                            }
                            else if ( string.Equals( "opaque" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                            {
                                result.SetOpaque( parameter.Value );
                            }
                            else if ( string.Equals( "algorithm" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                            {
                                result.SetAlgorithm( parameter.Value );
                            }
                            else if ( string.Equals( "stale" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                            {
                                result.SetStale( parameter.Value );
                            }
                            else if ( string.Equals( "qop" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                            {
                                result.SetQualityOfProtection( parameter.Value );
                            }
                        }
                    }
                }
            }

            return result != null;
        }






        public void SetScheme( string value )
        {
            Scheme = RtspValueNormalizer.Normalize( value );
        }

        public void SetRealm( string value )
        {
            Realm = RtspValueNormalizer.Normalize( value );
        }

        public void SetNonce( string value )
        {
            Nonce = RtspValueNormalizer.Normalize( value );
        }

        public void SetOpaque( string value )
        {
            Opaque = RtspValueNormalizer.Normalize( value );
        }

        public void SetAlgorithm( string value )
        {
            Algorithm = RtspValueNormalizer.Normalize( value );
        }

        public void SetStale( string value )
        {
            Stale = RtspValueNormalizer.Normalize( value );
        }

        public void SetQualityOfProtection( string value )
        {
            QualityOfProtection = RtspValueNormalizer.Normalize( value );
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

            return builder.ToString().Trim( ' ' , ',' );
        }
    }
}
