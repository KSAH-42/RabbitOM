using System;

namespace RabbitOM.Net.Rtps
{
    /// <summary>
    /// Represent type converter class
    /// </summary>
    public static class RTSPDigestAlgorithmTypeConverter
    {
        /// <summary>
        /// Perform a convertion
        /// </summary>
        /// <param name="algorithmType">the algorithm type</param>
        /// <returns>returns a string value</returns>
        public static string Convert( RTSPDigestAlgorithmType algorithmType )
        {
            switch ( algorithmType )
            {
                case RTSPDigestAlgorithmType.MD5:
                    return "MD5";

                case RTSPDigestAlgorithmType.SHA_256:
                    return "SHA-256";

                case RTSPDigestAlgorithmType.SHA_512:
                    return "SHA-512";
            }

            return string.Empty;
        }

        /// <summary>
        /// Perform a convertion
        /// </summary>
        /// <param name="algorithmType">the algorithm type</param>
        /// <returns>returns a string value</returns>
        public static RTSPDigestAlgorithmType Convert( string algorithmType )
        {
            if ( string.IsNullOrWhiteSpace( algorithmType ) )
            {
                return RTSPDigestAlgorithmType.UnDefined;
            }

            var method     = algorithmType.Trim();
            var ignoreCase = true;

            if ( string.Compare( "MD5" , method , ignoreCase ) == 0 )
            {
                return RTSPDigestAlgorithmType.MD5;
            }

            if ( string.Compare( "SHA-256" , method , ignoreCase ) == 0 ||
                 string.Compare( "SHA256" , method , ignoreCase ) == 0
                )
            {
                return RTSPDigestAlgorithmType.SHA_256;
            }

            if ( string.Compare( "SHA-512" , method , ignoreCase ) == 0 ||
                 string.Compare( "SHA512" , method , ignoreCase ) == 0
                )
            {
                return RTSPDigestAlgorithmType.SHA_512;
            }

            return RTSPDigestAlgorithmType.UnDefined;
        }
    }
}
