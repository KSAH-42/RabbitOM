using System.Text;
using System.Security.Cryptography;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a class used to generate a digest value using MD5 algorithm
    /// </summary>
    internal static class RTSPHashAlgorithm
    {
        /// <summary>
        /// Create a hash
        /// </summary>
        /// <param name="input">the input</param>
        /// <returns>returns a string</returns>
        public static string ComputeMD5Hash( string input )
        {
            return ComputeHash( MD5.Create() , input );
        }

        /// <summary>
        /// Create a hash
        /// </summary>
        /// <param name="input">the input</param>
        /// <returns>returns a string</returns>
        public static string ComputeSHA1Hash( string input )
        {
            return ComputeHash( SHA1.Create() , input );
        }

        /// <summary>
        /// Create a hash
        /// </summary>
        /// <param name="input">the input</param>
        /// <returns>returns a string</returns>
        public static string ComputeSHA256Hash( string input )
        {
            return ComputeHash( SHA256.Create() , input );
        }

        /// <summary>
        /// Create a hash
        /// </summary>
        /// <param name="input">the input</param>
        /// <returns>returns a string</returns>
        public static string ComputeSHA512Hash( string input )
        {
            return ComputeHash( SHA512.Create() , input );
        }

        /// <summary>
        /// Create a hash
        /// </summary>
        /// <param name="algorithm">the algorithm</param>
        /// <param name="input">the input</param>
        /// <returns>returns a string</returns>
        private static string ComputeHash( HashAlgorithm algorithm , string input )
        {
            if ( algorithm == null || string.IsNullOrEmpty( input ) )
            {
                return string.Empty;
            }

            using ( algorithm )
            {
                var plainBytes = System.Text.Encoding.ASCII.GetBytes( input );

                if ( plainBytes == null || plainBytes.Length <= 0 )
                {
                    return string.Empty;
                }

                var digestBytes = algorithm.ComputeHash( plainBytes );

                if ( digestBytes == null || digestBytes.Length <= 0 )
                {
                    return string.Empty;
                }

                var output = new StringBuilder();

                for ( int i = 0 ; i < digestBytes.Length ; i++ )
                {
                    output.Append( digestBytes[i].ToString( "x2" ) );
                }

                return output.ToString();
            }
        }
    }
}
