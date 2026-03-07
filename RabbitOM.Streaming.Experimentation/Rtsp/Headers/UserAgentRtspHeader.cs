using System;
using System.Text;
using System.Text.RegularExpressions;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters;
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Validation;

    public sealed class UserAgentRtspHeader
    {
        public static readonly string TypeName = "User-Agent";

        public static readonly StringComparer ValueComparer = StringComparer.OrdinalIgnoreCase;
        public static readonly StringValueAdapter ValueAdapter = StringValueAdapter.UnQuoteAdapter;
        public static readonly StringValueValidator ValueValidator = StringValueValidator.TokenValidator;


        private string _product = string.Empty;
        private string _version = string.Empty;
        private string _comment = string.Empty;
                

        public string Product
        {
            get => _product;
            set => _product = ValueAdapter.Adapt( value );
        }

        public string Version
        {
            get => _version;
            set => _version = ValueAdapter.Adapt( value );
        }
        
        public string Comment
        {
            get => _comment;
            set => _comment = ValueAdapter.Adapt( value );
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

            input = ValueAdapter.Adapt( input );

            if ( string.IsNullOrWhiteSpace( input ) )
            {
                return false;
            }

            var pattern = @"(?:(?<product>[A-Za-z0-9\-\._]+)\s*(?:/\s*(?<version>[A-Za-z0-9\-\._]+))?)|\((?<comment>[^()]*)\)";

            var matches = new Regex( pattern , RegexOptions.Compiled | RegexOptions.CultureInvariant ).Matches( input.Trim() );

            if ( matches.Count <= 0 )
            {
                return false;
            }

            result = new UserAgentRtspHeader();

            foreach ( Match match in matches )
            {
                if ( match.Groups["product"].Success )
                {
                    result.Product = match.Groups["product"].Value;
                    result.Version = match.Groups["version"].Value;
                }
                else if ( match.Groups["comment"].Success )
                {
                    result.Comment = match.Groups["comment"].Value;
                }
            }

            return true;
        }
    }
}
