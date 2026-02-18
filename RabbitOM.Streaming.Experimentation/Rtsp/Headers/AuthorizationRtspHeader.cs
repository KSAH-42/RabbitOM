using System;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Formatting;

    public sealed class AuthorizationRtspHeader
    {
        public static readonly string TypeName = "Authorization";




        public string Scheme { get; private set; } = string.Empty;
        
        public string UserName { get; private set; } = string.Empty;

        public string Realm { get; private set; } = string.Empty;
        
        public string Nonce { get; private set; } = string.Empty;
        
        public string Domain { get; private set; } = string.Empty;

        public string Opaque { get; private set; } = string.Empty;
        
        public string Uri { get; private set; } = string.Empty;
        
        public string Response { get; private set; } = string.Empty;

        public string Algorithm { get; private set; } = string.Empty;

        public string CNonce { get; private set; } = string.Empty;

        public string NC { get; private set; } = string.Empty;

        public string QualityOfProtection { get; private set; } = string.Empty;





        public static bool TryParse( string input , out AuthorizationRtspHeader result )
        {
            result = null;

            input = RtspValueNormalizer.Normalize( input );

            if ( StringRtspHeaderParser.TryParse( input , ' ' , out var tokens ) )
            {
                var scheme = tokens.First();
                
                if ( StringRtspHeaderParser.TryParse( input.Replace( scheme , "" ) , ',' , out tokens ) )
                {
                    result = new AuthorizationRtspHeader(); 
                    
                    result.SetScheme( scheme );

                    foreach ( var token in tokens )
                    {
                        if ( StringParameterRtspHeaderParser.TryParse( token , '=' , out var parameter ) )
                        {
                            if ( string.Equals( "username" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                            {
                                result.SetUserName( parameter.Value );
                            }
                            else if ( string.Equals( "realm" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
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
                            else if ( string.Equals( "domain" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                            {
                                result.SetDomain( parameter.Value );
                            }
                            else if ( string.Equals( "uri" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                            {
                                result.SetUri( parameter.Value );
                            }
                            else if ( string.Equals( "response" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                            {
                                result.SetResponse( parameter.Value );
                            }                            
                            else if ( string.Equals( "algorithm" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                            {
                                result.SetAlgorithm( parameter.Value );
                            }
                            else if ( string.Equals( "cnonce" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                            {
                                result.SetCNonce( parameter.Value );
                            }
                            else if ( string.Equals( "nc" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                            {
                                result.SetNC( parameter.Value );
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

        public void SetUserName( string value )
        {
            UserName = RtspValueNormalizer.Normalize( value );
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

        public void SetDomain( string value )
        {
            Domain = RtspValueNormalizer.Normalize( value );
        }

        public void SetUri( string value )
        {
            Uri = RtspValueNormalizer.Normalize( value );
        }

        public void SetResponse( string value )
        {
            Response = RtspValueNormalizer.Normalize( value );
        }

        public void SetAlgorithm( string value )
        {
            Algorithm = RtspValueNormalizer.Normalize( value );
        }

        public void SetCNonce( string value )
        {
            CNonce = RtspValueNormalizer.Normalize( value );
        }

        public void SetNC( string value )
        {
            NC = RtspValueNormalizer.Normalize( value );
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

            builder.AppendFormat( "username=\"{0}\", " , UserName );
            builder.AppendFormat( "realm=\"{0}\", " , Realm );
            builder.AppendFormat( "nonce=\"{0}\", " , Nonce );

            if ( ! string.IsNullOrWhiteSpace( Domain ) )
            {
                builder.AppendFormat( "domain=\"{0}\", " , Domain );
            }

            if ( ! string.IsNullOrWhiteSpace( Opaque ) )
            {
                builder.AppendFormat( "opaque=\"{0}\", " , Opaque );
            }

            builder.AppendFormat( "uri=\"{0}\", " , Uri );
            builder.AppendFormat( "response=\"{0}\", " , Response );

            if ( ! string.IsNullOrWhiteSpace( Algorithm ) )
            {
                builder.AppendFormat( "algorithm=\"{0}\", " , Algorithm );
            }

            if ( ! string.IsNullOrWhiteSpace( CNonce ) )
            {
                builder.AppendFormat( "cnonce=\"{0}\", " , CNonce );
            }

            if ( ! string.IsNullOrWhiteSpace( NC ) )
            {
                builder.AppendFormat( "nc=\"{0}\", " , NC );
            }

            if ( ! string.IsNullOrWhiteSpace( QualityOfProtection ) )
            {
                builder.AppendFormat( "qop=\"{0}\", " , QualityOfProtection );
            }

            return builder.ToString().Trim( ' ' , ',' );
        }
    }
}
