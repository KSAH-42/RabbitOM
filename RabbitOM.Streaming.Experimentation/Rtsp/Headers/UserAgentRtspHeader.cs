using System;
using System.Text;
using System.Text.RegularExpressions;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Formatting;

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

            var matches = new Regex( @"(?:(?<product>[A-Za-z0-9\-\._]+)\s*(?:/\s*(?<version>[A-Za-z0-9\-\._]+))?)|\((?<comments>[^()]*)\)" , RegexOptions.Compiled | RegexOptions.CultureInvariant ).Matches( input );

            if ( matches.Count <= 0 )
            {
                return false;
            }

            result = new UserAgentRtspHeader();

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
