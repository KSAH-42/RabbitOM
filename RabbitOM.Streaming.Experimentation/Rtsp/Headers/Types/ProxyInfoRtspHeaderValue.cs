using System;
using System.Text;
using System.Text.RegularExpressions;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types
{
    public sealed class ProxyInfoRtspHeaderValue
    { 
        private static readonly string RegularExpression = @"^\s*(?<protocol>[A-Za-z]+)\s*\/\s*(?<version>\d+\.\d+)\s+(?<receivedBy>[^\s()]+)(?:\s*\((?<comment>.*)\))?\s*$";
    




        private string _protocol = string.Empty;
        private string _version = string.Empty;
        private string _receivedBy = string.Empty;
        private string _comment = string.Empty;





        public string Protocol
        {
            get => _protocol;
            set => _protocol = RtspHeaderValueValidator.EnsureWellFormedToken( RtspHeaderValueSanitizer.UnQuotesWithTrim( value ) );
        }

        public string Version
        {
            get => _version;
            set => _version = RtspHeaderValueValidator.EnsureWellFormedToken( RtspHeaderValueSanitizer.UnQuotesWithTrim( value ) );
        }

        public string ReceivedBy
        {
            get => _receivedBy;
            set => _receivedBy = RtspHeaderValueValidator.EnsureWellFormedToken( RtspHeaderValueSanitizer.UnQuotesWithTrim( value ) );
        }

        public string Comment
        {
            get => _comment;
            set => _comment = RtspHeaderValueValidator.EnsureWellFormedTokenOrEmpty( RtspHeaderValueSanitizer.UnQuotesWithTrim( value ) );
        }




        public static bool TryParse( string input , out ProxyInfoRtspHeaderValue result )
        {
            result = null;

            if ( string.IsNullOrWhiteSpace( input ) )
            {
                return false;
            }

            var matchResult = new Regex( RegularExpression, RegexOptions.Compiled | RegexOptions.CultureInvariant).Match( input.Trim() );

            if ( matchResult.Success )
            {
                result = new ProxyInfoRtspHeaderValue()
                {
                    Protocol   = matchResult.Groups[ "protocol" ].Value ,
                    Version    = matchResult.Groups[ "version" ].Value ,
                    ReceivedBy = matchResult.Groups[ "receivedBy" ].Value ,
                    Comment    = matchResult.Groups[ "comment" ].Value ,
                };
            }

            return result != null;
        }






        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendFormat( "{0}/{1} {2} " , Protocol , Version , ReceivedBy );

            if ( ! string.IsNullOrWhiteSpace( Comment ) )
            {
                builder.AppendFormat( "({0})" , Comment );
            }

            return builder.ToString().Trim();
        }
    }
}
