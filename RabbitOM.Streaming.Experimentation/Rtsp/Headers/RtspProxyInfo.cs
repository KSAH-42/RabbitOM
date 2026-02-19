using System;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Formatting;

    public sealed class RtspProxyInfo 
    { 
        public static readonly RtspProxyInfo Empty = new RtspProxyInfo( string.Empty , string.Empty , string.Empty , string.Empty );






        private RtspProxyInfo( string protocol , string version , string receivedBy , string comments )
        {
            Protocol = protocol ?? string.Empty;
            Version = version ?? string.Empty;
            ReceivedBy = receivedBy ?? string.Empty;
            Comments = comments ?? string.Empty;
        }





        public string Protocol { get; }

        public string Version { get; }

        public string ReceivedBy { get; }

        public string Comments { get; }
        

        



        public static RtspProxyInfo NewProxy( string protocol , string version , string receivedBy )
        {
            return NewProxy( protocol , version , receivedBy );
        }

        public static RtspProxyInfo NewProxy( string protocol , string version , string receivedBy , string comments )
        {
            return new RtspProxyInfo( RtspValueNormalizer.Normalize( protocol ) ,
                RtspValueNormalizer.Normalize( version ) , 
                RtspValueNormalizer.Normalize( receivedBy ) ,
                RtspValueNormalizer.Normalize( comments , "(" , ")" ) 
                );
        }






        public static bool TryParse( string input , out RtspProxyInfo result )
        {
            result = null;

            if ( StringRtspHeaderParser.TryParse( RtspValueNormalizer.Normalize( input ) , ' ' , out var tokens ) )
            {
                if ( StringRtspHeaderParser.TryParse( tokens.FirstOrDefault() , '/' , out var protocolTokens ) )
                {
                    var protocol = protocolTokens.ElementAtOrDefault( 0 );
            
                    if ( string.IsNullOrWhiteSpace( protocol ) )
                    {
                        return false;
                    }
            
                    var version = protocolTokens.ElementAtOrDefault( 1 );

                    if ( ! System.Version.TryParse( version , out var _ ) )
                    {
                        return false;
                    }

                    var receivedBy = tokens
                        .Skip( 1 )
                        .FirstOrDefault( token => ! token.StartsWith( "(" ) && ! token.EndsWith( ")" ) )
                        ;

                    if ( string.IsNullOrWhiteSpace( receivedBy ) )
                    {
                        return false;
                    }

                    var comments = tokens
                        .Skip( 1 )
                        .FirstOrDefault( token => token.StartsWith( "(" ) && token.EndsWith( ")" ) )
                        ;

                    result = RtspProxyInfo.NewProxy( protocol , version , receivedBy , comments );
                }
            }

            return result != null;
        }






        
        public override string ToString()
        {
            var builder = new StringBuilder();

            if ( ! string.IsNullOrWhiteSpace( Protocol ) )
            {
                builder.Append( Protocol );
            }

            if ( ! string.IsNullOrWhiteSpace( Version ) )
            {
                if ( builder.Length > 0 )
                {
                    builder.Append( "/" );
                }

                builder.Append( Version );
            }

            if ( ! string.IsNullOrWhiteSpace( ReceivedBy ) )
            {
                if ( builder.Length > 0 )
                {
                    builder.Append( " " );
                }

                builder.Append( ReceivedBy );
            }

            if ( ! string.IsNullOrWhiteSpace( Comments ) )
            {
                if ( builder.Length > 0 )
                {
                    builder.Append( " " );
                }

                builder.AppendFormat( "({0})" , Comments );
            }

            return builder.ToString();
        }
    }
}
