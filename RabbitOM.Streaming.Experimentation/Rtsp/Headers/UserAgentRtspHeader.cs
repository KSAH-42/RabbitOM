using System;
using System.Text;
using System.Text.RegularExpressions;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Parsers;

    public sealed class UserAgentRtspHeader : RtspHeader
    {
        private static readonly string RegularExpression = @"(?:(?<product>[A-Za-z0-9\-\._]+)\s*(?:/\s*(?<version>[A-Za-z0-9\-\._]+))?)|\((?<comment>[^()]*)\)";




        public static readonly string TypeName = "User-Agent";
        




        public string Product { get; private set; } = string.Empty;

        public string Version { get; private set; } = string.Empty;
        
        public string Comment { get; private set; } = string.Empty;
        




        public void SetProduct( string value )
        {
            Product = StringRtspHeaderParser.TrimValue( value , StringRtspHeaderParser.SpaceWithQuotesChars );
        }

        public void SetVersion( string value )
        {
            Version = StringRtspHeaderParser.TrimValue( value , StringRtspHeaderParser.SpaceWithQuotesChars );
        }

        public void SetComment( string value )
        {
            Comment = StringRtspHeaderParser.TrimValue( value , StringRtspHeaderParser.ParenthesisChars );
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

            if ( ! string.IsNullOrWhiteSpace( Comment ) )
            {
                if ( builder.Length > 0 )
                {
                    builder.Append( ' ' );
                }

                builder.AppendFormat( "({0})" , Comment );
            }

            return builder.ToString();
        }





        public static bool TryParse( string input , out UserAgentRtspHeader result )
        {
            result = null;

            input = StringRtspHeaderParser.TrimValue( input );

            if ( string.IsNullOrWhiteSpace( input ) )
            {
                return false;
            }

            var matches = new Regex( RegularExpression , RegexOptions.Compiled | RegexOptions.CultureInvariant ).Matches( input );

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
                else if ( match.Groups["comment"].Success )
                {
                    result.SetComment( match.Groups["comment"].Value );
                }
            }

            return true;
        }
    }
}
