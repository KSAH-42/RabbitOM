using System;
using System.Text;
using System.Text.RegularExpressions;

namespace RabbitOM.Streaming.Experimentation.Rtsp.HeadersToBeRemoved
{
    public sealed class ProxyInfo 
    { 
        private static readonly string RegularExpression = @"^\s*(?<protocol>[A-Za-z]+)\s*\/\s*(?<version>\d+\.\d+)\s+(?<receivedBy>[^\s()]+)(?:\s*\((?<comment>.*)\))?\s*$";



        public ProxyInfo( string protocol , string version , string receivedBy )
            : this ( protocol , version , receivedBy , null )
        {
        }

        public ProxyInfo( string protocol , string version , string receivedBy , string comment )
        {
            if ( ! RtspHeaderValueValidator.TryValidateToken( protocol ) )
            {
                throw new ArgumentException( protocol , "the argument called protocol contains bad things");
            }

            if ( ! RtspHeaderValueValidator.TryValidateToken( version ) )
            {
                throw new ArgumentException( version , "the argument called version is not valid or may contains invalid chars");
            }

            if ( ! RtspHeaderValueValidator.TryValidateToken( receivedBy ) )
            {
                throw new ArgumentException( receivedBy , "the argument called receivedBy is not valid or may contains invalid chars");
            }

            if ( ! RtspHeaderValueValidator.TryValidateVersion( version ) )
            {
                throw new ArgumentException( nameof( version ) ,"the version is not well formated" );
            }

            if ( ! RtspHeaderValueValidator.TryValidateComment( comment ) )
            {
                throw new ArgumentException( comment , "the argument called comment is not valid or may contains invalid chars");
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

            if ( ! RtspHeaderValueValidator.TryValidate( input ) )
            {
                return false;
            }

            var matchResult = new Regex( RegularExpression, RegexOptions.Compiled | RegexOptions.CultureInvariant).Match( input.Trim() );

            if ( matchResult.Success )
            {
                if ( ! RtspHeaderValueValidator.TryValidateToken( matchResult.Groups[ "protocol" ].Value ) || 
                     ! RtspHeaderValueValidator.TryValidateToken( matchResult.Groups[ "version" ].Value ) || 
                     ! RtspHeaderValueValidator.TryValidateToken( matchResult.Groups[ "receivedBy" ].Value ) ||
                     ! RtspHeaderValueValidator.TryValidateVersion( matchResult.Groups[ "version" ].Value ) )
                {
                    return false;
                }

                if ( ! RtspHeaderValueValidator.TryValidateComment( matchResult.Groups[ "comment" ].Value ) )
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
