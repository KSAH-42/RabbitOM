using System;
using System.Text;
using System.Text.RegularExpressions;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class ProxyInfo 
    { 
        private static readonly string RegularExpression = @"^\s*(?<protocol>[A-Za-z]+)\s*\/\s*(?<version>\d+\.\d+)\s+(?<receivedBy>[^\s()]+)(?:\s*\((?<comments>.*)\))?\s*$";


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

            input = RtspHeaderValueNormalizer.Normalize( input );

            if ( string.IsNullOrWhiteSpace( input ) )
            {
                return false;
            }

            var matchResult = new Regex( RegularExpression, RegexOptions.Compiled | RegexOptions.CultureInvariant).Match( input );

            if ( ! matchResult.Success )
            {
                return false;
            }

            if ( ! System.Version.TryParse( matchResult.Groups[ "version" ].Value , out _ ) )
            {
                return false;
            }

            var proxyInfo = new ProxyInfo( matchResult.Groups[ "protocol" ].Value , matchResult.Groups[ "version" ].Value , matchResult.Groups[ "receivedBy" ].Value , matchResult.Groups[ "comments" ].Value );
            
            if ( string.IsNullOrWhiteSpace( proxyInfo.Protocol ) || string.IsNullOrWhiteSpace( proxyInfo.Version ) || string.IsNullOrWhiteSpace( proxyInfo.ReceivedBy ) )
            {
                return false;
            }

            result = proxyInfo;

            return true;
        }
    }
}
