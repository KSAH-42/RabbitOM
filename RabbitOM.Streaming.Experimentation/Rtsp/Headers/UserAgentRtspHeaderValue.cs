using System;
using System.Text;
using System.Text.RegularExpressions;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters;
   
    public sealed class UserAgentRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "User-Agent";

        public static readonly StringComparer ValueComparer = StringComparer.OrdinalIgnoreCase;
        public static readonly StringValueAdapter ValueAdapter = StringValueAdapter.TrimWithUnQuoteAdapter;
        
        private static readonly string RegularExpression = @"(?:(?<product>[A-Za-z0-9\-\._]+)\s*(?:/\s*(?<version>[A-Za-z0-9\-\._]+))?)|\((?<comment>[^()]*)\)";

            

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
        


        public static bool TryParse( string input , out UserAgentRtspHeaderValue result )
        {
            result = null;

            input = ValueAdapter.Adapt( input );

            if ( string.IsNullOrWhiteSpace( input ) )
            {
                return false;
            }

            var matches = new Regex( RegularExpression , RegexOptions.Compiled | RegexOptions.CultureInvariant ).Matches( input.Trim() );

            if ( matches.Count > 0 )
            {
                var header = new UserAgentRtspHeaderValue();

                foreach ( Match match in matches )
                {
                    if ( match.Groups["product"].Success )
                    {
                        header.Product = match.Groups["product"].Value;
                        header.Version = match.Groups["version"].Value;
                    }
                    else if ( match.Groups["comment"].Success )
                    {
                        header.Comment = match.Groups["comment"].Value;
                    }
                }

                if ( RtspHeaderValueValidator.IsValidToken( header.Product ) && RtspHeaderValueValidator.IsValidToken( header.Version ) )
                {
                    result = header;
                }
            }

            return result != null;
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
    }
}
