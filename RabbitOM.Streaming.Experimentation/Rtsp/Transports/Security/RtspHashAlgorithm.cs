using System;
using System.Security.Cryptography;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Security
{
    public static class RtspAuthorizationAlgorithm
    {
        public static string ComputeAsBasic( string input )
        {
            var plainBytes = System.Text.Encoding.UTF8.GetBytes( input );

            if ( plainBytes == null || plainBytes.Length <= 0 )
            {
                return string.Empty;
            }

            return Convert.ToBase64String( plainBytes );
        }
        public static string ComputeAsMD5( string input )
        {
            return ComputeHash( MD5.Create() , input );
        }

        public static string ComputeAsSHA1( string input )
        {
            return ComputeHash( SHA1.Create() , input );
        }

        public static string ComputeAsSHA256( string input )
        {
            return ComputeHash( SHA256.Create() , input );
        }

        public static string ComputeAsSHA512( string input )
        {
            return ComputeHash( SHA512.Create() , input );
        }

        private static string ComputeHash( HashAlgorithm algorithm , string input )
        {
            if ( algorithm == null || string.IsNullOrEmpty( input ) )
            {
                return string.Empty;
            }

            using ( algorithm )
            {
                var plainBytes = System.Text.Encoding.UTF8.GetBytes( input );

                if ( plainBytes == null || plainBytes.Length <= 0 )
                {
                    return string.Empty;
                }

                var digestBytes = algorithm.ComputeHash( plainBytes );

                if ( digestBytes == null || digestBytes.Length <= 0 )
                {
                    return string.Empty;
                }

                var builder = new StringBuilder();

                for ( int i = 0 ; i < digestBytes.Length ; i++ )
                {
                    builder.Append( digestBytes[i].ToString( "x2" ) );
                }

                return builder.ToString();
            }
        }
    }
}
