using System;
using System.Text;
using System.Text.RegularExpressions;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Validation;
    using System.Xml.Linq;

    public sealed class ProxyInfo 
    { 
        private static readonly string RegularExpression = @"^\s*(?<protocol>[A-Za-z]+)\s*\/\s*(?<version>\d+\.\d+)\s+(?<receivedBy>[^\s()]+)(?:\s*\((?<comment>.*)\))?\s*$";

        private static readonly char[] ParenthesisChars = { '(' , ')' };

        public static readonly StringValueValidator ValueValidator = StringValueValidator.TokenValidator;
        


        public ProxyInfo( string protocol , string version , string receivedBy )
            : this ( protocol , version , receivedBy , null )
        {
        }

        public ProxyInfo( string protocol , string version , string receivedBy , string comment )
        {
            if ( ! ValueValidator.TryValidate( protocol ) )
            {
                throw new ArgumentException( protocol , "the argument called protocol contains bad things");
            }

            if ( ! ValueValidator.TryValidate( version ) )
            {
                throw new ArgumentException( version , "the argument called version is not valid or may contains invalid chars");
            }

            if ( ! ValueValidator.TryValidate( receivedBy ) )
            {
                throw new ArgumentException( receivedBy , "the argument called receivedBy is not valid or may contains invalid chars");
            }

            if ( ! System.Version.TryParse( version , out _ ) )
            {
                throw new ArgumentException( nameof( version ) ,"the version is not well formated" );
            }

            if ( ! string.IsNullOrWhiteSpace( comment ) )
            {
                if ( ! ValueValidator.TryValidate( comment ) || comment.IndexOfAny( ParenthesisChars ) >= 0 )
                {
                    throw new ArgumentException( comment , "the argument called comment is not valid or may contains invalid chars");
                }
            }

            Protocol = protocol.Trim();
            Version = version.Trim();
            ReceivedBy = receivedBy.Trim();
            Comment = comment?.Trim();
        }



        public string Protocol { get; }

        public string Version { get; }

        public string ReceivedBy { get; }

        public string Comment { get; }
        


        public static bool TryParse( string input , out ProxyInfo result )
        {
            result = null;

            if ( ! ValueValidator.TryValidate( input ) )
            {
                return false;
            }

            var matchResult = new Regex( RegularExpression, RegexOptions.Compiled | RegexOptions.CultureInvariant).Match( input.Trim() );

            if ( matchResult.Success )
            {
                if ( ! System.Version.TryParse( matchResult.Groups[ "version" ].Value , out _ ) )
                {
                    return false;
                }

                if ( ! ValueValidator.TryValidate( matchResult.Groups[ "protocol" ].Value ) || 
                     ! ValueValidator.TryValidate( matchResult.Groups[ "version" ].Value ) || 
                     ! ValueValidator.TryValidate( matchResult.Groups[ "receivedBy" ].Value ) )
                {
                    return false;
                }

                if ( matchResult.Groups[ "comment" ].Value?.IndexOfAny( ParenthesisChars ) >=0 )
                {
                    return false;
                }

                result = new ProxyInfo( matchResult.Groups[ "protocol" ].Value , matchResult.Groups[ "version" ].Value , matchResult.Groups[ "receivedBy" ].Value , matchResult.Groups[ "comment" ].Value );
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
