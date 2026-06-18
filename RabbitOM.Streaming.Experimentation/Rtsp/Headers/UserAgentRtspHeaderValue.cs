using System;
using System.Text;
using System.Text.RegularExpressions;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class UserAgentRtspHeaderValue
    {
        private static readonly StringComparer ValueComparer = StringComparer.OrdinalIgnoreCase;

        private static readonly string RegularExpression = @"(?:(?<product>[A-Za-z0-9\-\._]+)\s*(?:/\s*(?<version>[A-Za-z0-9\-\._]+))?)|\((?<comment>[^()]*)\)";



        private string _product = string.Empty;
        private string _version = string.Empty;
        private string _comment = string.Empty;




        public string Product
        {
            get => _product;
            set => _product = RtspHeaderValueValidator.EnsureWellFormedToken( RtspHeaderValueSanitizer.UnQuotesWithTrim( value ) ); 
        }

        public string Version
        {
            get => _version;
            set => _version = RtspHeaderValueValidator.EnsureWellFormedToken( RtspHeaderValueSanitizer.UnQuotesWithTrim( value ) ); 
        }
        
        public string Comment
        {
            get => _comment;
            set => _comment = RtspHeaderValueValidator.EnsureWellFormedTokenIfAll( RtspHeaderValueSanitizer.UnQuotesWithTrim( value ) , x => x != '(' && x != ')' ); 
        }
        



        public static bool TryParse( string input , out UserAgentRtspHeaderValue result )
        {
            result = null;

            input = RtspHeaderValueSanitizer.UnQuotesWithTrim( input );

            if ( string.IsNullOrWhiteSpace( input ) )
            {
                return false;
            }

            var matches = new Regex( RegularExpression , RegexOptions.Compiled | RegexOptions.CultureInvariant ).Matches( input.Trim() );

            if ( matches.Count > 0 )
            {
                var product = string.Empty;
                var version = string.Empty;
                var comment = string.Empty;

                foreach ( Match match in matches )
                {
                    if ( match.Groups["product"].Success )
                    {
                        product = match.Groups["product"].Value;
                        version = match.Groups["version"].Value;
                    }
                    else if ( match.Groups["comment"].Success )
                    {
                        comment = match.Groups["comment"].Value;
                    }
                }

                if ( ! RtspHeaderValueValidator.TryEnsureWellFormedToken( product ) )
                {
                    return false;
                }

                if ( ! RtspHeaderValueValidator.TryEnsureWellFormedToken( version ) )
                {
                    return false;
                }

                result = new UserAgentRtspHeaderValue()
                {
                    _product = product ,
                    _version = version ,
                    _comment = RtspHeaderValueValidator.TryEnsureWellFormedTokenIfAll( comment , x => x != '(' && x != ')' ) 
                    ? comment 
                    : "",
                };
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
