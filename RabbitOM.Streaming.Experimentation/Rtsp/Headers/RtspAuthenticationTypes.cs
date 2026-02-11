using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public static class RtspAuthenticationTypes
    {
        public const string BasicAuthentication = "Basic";

        public const string DigestAuthentication = "Digest";

        public const string NtlmAuthentication = "NTLM";

        public const string NegotiateAuthentication = "Negotiate";

        public const string Md5Algorithm = "MD5";

        public const string Sha1Algorithm = "SHA-1";

        public const string Sha256Algorithm = "SHA-256";

        public const string Sha512Algorithm = "SHA-512";

        
        
        
        private readonly static HashSet<string> Md5AlgorithmKnows 
            = new [] { "MD5" , "MD5-sess" }
                .ToHashSet( StringComparer.OrdinalIgnoreCase );
        
        private readonly static HashSet<string> Sha1AlgorithmKnows 
            = new [] { "SHA-1" , "SHA-1-sess" , "SHA1" , "SHA1-sess" , "SHA_1" , "SHA_1-sess" , "SHA" , "SHA-sess" }
                .ToHashSet( StringComparer.OrdinalIgnoreCase );

        private readonly static HashSet<string> Sha256AlgorithmKnows 
            = new [] { "SHA-256" , "SHA-256-sess" , "SHA256" , "SHA256-sess" , "SHA_256" , "SHA_256-sess" , "SHA256" , "SHA256-sess" }
                .ToHashSet( StringComparer.OrdinalIgnoreCase );
        
        private readonly static HashSet<string> Sha512AlgorithmKnows 
            = new [] { "SHA-512" , "SHA-512-sess" , "SHA512" , "SHA512-sess" , "SHA_512" , "SHA_512-sess" , "SHA512" , "SHA256-sess" }
                .ToHashSet( StringComparer.OrdinalIgnoreCase );

        


        
        public static bool IsBasicAuthentication( string value )
        {
            return string.Equals( value?.Trim() , BasicAuthentication , StringComparison.OrdinalIgnoreCase );
        }

        public static bool IsDigestAuthentication( string value )
        {
            return string.Equals( value?.Trim() , DigestAuthentication , StringComparison.OrdinalIgnoreCase );
        }

        public static bool IsNtlmAuthentication( string value )
        {
            return string.Equals( value?.Trim() , NtlmAuthentication , StringComparison.OrdinalIgnoreCase );
        }

        public static bool IsNegotiateAuthentication( string value )
        {
            return string.Equals( value?.Trim() , NegotiateAuthentication , StringComparison.OrdinalIgnoreCase );
        }

        public static bool IsMd5Algorithm( string value )
        {
            return Md5AlgorithmKnows.Contains( value?.Trim() );
        }

        public static bool IsSha1Algorithm( string value )
        {
            return Sha1AlgorithmKnows.Contains( value?.Trim() );
        }

        public static bool IsSha256Algorithm( string value )
        {
            return Sha256AlgorithmKnows.Contains( value?.Trim() );
        }

        public static bool IsSha512Algorithm( string value )
        {
            return Sha512AlgorithmKnows.Contains( value?.Trim() );
        }
    }
}
