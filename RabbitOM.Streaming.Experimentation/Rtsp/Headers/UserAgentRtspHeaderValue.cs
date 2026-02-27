using System;
using System.Text;
using System.Text.RegularExpressions;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class UserAgentRtspHeaderValue : RtspHeaderValue
    {
        private static readonly string[] CommentsSeparators = { "(" , ")" };

        private static readonly string RegularExpression = @"(?:(?<product>[A-Za-z0-9\-\._]+)\s*(?:/\s*(?<version>[A-Za-z0-9\-\._]+))?)|\((?<comments>[^()]*)\)";




        public static readonly string TypeName = "User-Agent";
        




        public string Product { get; private set; } = string.Empty;

        public string Version { get; private set; } = string.Empty;
        
        public string Comments { get; private set; } = string.Empty;
        




        public void SetProduct( string value )
        {
            Product = StringRtspHeaderNormalizer.Normalize( value );
        }

        public void SetVersion( string value )
        {
            Version = StringRtspHeaderNormalizer.Normalize( value );
        }

        public void SetComments( string value )
        {
            Comments = StringRtspHeaderNormalizer.Normalize( value , CommentsSeparators );
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





        public static bool TryParse( string input , out UserAgentRtspHeaderValue result )
        {
            result = null;

            input = StringRtspHeaderNormalizer.Normalize( input );

            if ( string.IsNullOrWhiteSpace( input ) )
            {
                return false;
            }

            var matches = new Regex( RegularExpression , RegexOptions.Compiled | RegexOptions.CultureInvariant ).Matches( input );

            if ( matches.Count <= 0 )
            {
                return false;
            }

            result = new UserAgentRtspHeaderValue();

            foreach ( Match match in matches )
            {
                if ( match.Groups["product"].Success )
                {
                    result.SetProduct( match.Groups["product"].Value );
                    result.SetVersion( match.Groups["version"].Value );
                }
                else if ( match.Groups["comments"].Success )
                {
                    result.SetComments( match.Groups["comments"].Value );
                }
            }

            return true;
        }
    }
}
