using System;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types
{
    public sealed class ProxyInfo 
    { 
        private readonly string[] CommentsSeparators = { "(" , ")" };





        public ProxyInfo( string protocol , string version , string receivedBy )
            : this ( protocol , version , receivedBy , null )
        {
        }

        public ProxyInfo( string protocol , string version , string receivedBy , string comments )
        {
            if ( string.IsNullOrWhiteSpace( protocol ) )
            {
                throw new ArgumentException( protocol );
            }

            if ( string.IsNullOrWhiteSpace( version ) )
            {
                throw new ArgumentException( version );
            }

            if ( string.IsNullOrWhiteSpace( receivedBy ) )
            {
                throw new ArgumentException( receivedBy );
            }

            if ( ! RtspHeaderValueNormalizer.CheckValue( protocol ) )
            {
                throw new ArgumentException( protocol , "the argument called protocol contains bad things");
            }

            if ( ! RtspHeaderValueNormalizer.CheckValue( version ) )
            {
                throw new ArgumentException( version , "the argument called version contains bad things");
            }

            if ( ! RtspHeaderValueNormalizer.CheckValue( receivedBy ) )
            {
                throw new ArgumentException( receivedBy , "the argument called receivedBy contains bad things");
            }

            if ( ! System.Version.TryParse( version , out _ ) )
            {
                throw new ArgumentException( nameof( version ) );
            }

            Protocol = protocol.Trim();
            Version = version.Trim();
            ReceivedBy = receivedBy;
            Comments = RtspHeaderValueNormalizer.Normalize( comments , CommentsSeparators );
        }

        



        public string Protocol { get; }

        public string Version { get; }

        public string ReceivedBy { get; }

        public string Comments { get; }
        

        





        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendFormat( "{0}/{1} {2} " , Protocol , Version , ReceivedBy );

            if ( ! string.IsNullOrWhiteSpace( Comments ) )
            {
                builder.AppendFormat( "({0})" , Comments );
            }

            return builder.ToString().Trim();
        }
        
        
        

        
        
        public static bool TryParse( string input , out ProxyInfo result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( RtspHeaderValueNormalizer.Normalize( input ) , " " , out var tokens ) )
            {
                if ( RtspHeaderParser.TryParse( tokens.FirstOrDefault() , "/" , out var protocolTokens ) )
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

                    result = new ProxyInfo( protocol , version , receivedBy , comments );
                }
            }

            return result != null;
        }
    }
}
