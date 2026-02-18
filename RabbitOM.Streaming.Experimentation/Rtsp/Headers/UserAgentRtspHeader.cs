using System;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Formatting;
    using System.Linq;

    public sealed class UserAgentRtspHeader
    {
        public static readonly string TypeName = "User-Agent";
        



        public string Product { get; private set; } = string.Empty;

        public string Version { get; private set; } = string.Empty;
        
        public string Comments { get; private set; } = string.Empty;
        




        public static bool TryParse( string input , out UserAgentRtspHeader result )
        {
            result = null;

            input = RtspValueNormalizer.Normalize( input );

            if ( string.IsNullOrWhiteSpace( input ) )
            {
                return false;
            }

            void ParseToken( UserAgentRtspHeader header , string token )
            {
                if ( token.StartsWith( "(") && token.EndsWith( ")" ) )
                {
                    if ( string.IsNullOrWhiteSpace( header.Comments ) )
                    {
                        header.SetComments( token );
                    }
                }
                else if ( System.Version.TryParse( token.StartsWith( "V") || token.StartsWith("v") ? token.Remove( 0 , 1 ) : token , out var _ ) )
                {
                    if ( string.IsNullOrWhiteSpace( header.Version ) )
                    {
                        header.SetVersion( token );
                    }
                }
                else
                {
                    if ( string.IsNullOrWhiteSpace( header.Product ) )
                    {
                        header.SetProduct( token );
                    }
                }
            };

            result = new UserAgentRtspHeader();

            if ( StringRtspHeaderParser.TryParse( input , new char[] { ' ' , '/' } , out var tokens ) )
            {
                foreach ( var token in tokens )
                {
                    ParseToken( result , token );
                }
            }
            else
            {
                if ( StringRtspHeaderParser.TryParse( input , '/' , out var productTokens ) )
                {
                    result.SetProduct( productTokens.ElementAtOrDefault( 0 ) );
                    result.SetVersion( productTokens.ElementAtOrDefault( 1 ) );
                }
                else
                {
                    ParseToken( result , input );
                }
            }

            return true;
        }

        




        public void SetProduct( string value )
        {
            Product = RtspValueNormalizer.Normalize( value );
        }

        public void SetVersion( string value )
        {
            Version = RtspValueNormalizer.Normalize( value );
        }

        public void SetComments( string value )
        {
            Comments = RtspValueNormalizer.Normalize( value , "(" , ")" );
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            if ( ! string.IsNullOrWhiteSpace( Product ) )
            {
                builder.Append( Product );
            }

            if ( ! string.IsNullOrWhiteSpace( Version ) )
            {
                if ( builder.Length > 0 )
                {
                    builder.Append( '/' );
                }

                builder.Append( Version );
            }

            if ( ! string.IsNullOrWhiteSpace( Comments ) )
            {
                if ( builder.Length > 0 )
                {
                    builder.Append( ' ' );
                }

                builder.AppendFormat( "({0})" , Comments );
            }

            return builder.ToString();
        }
    }
}
