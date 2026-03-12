using System;
using System.Security.Cryptography;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Security
{
    public static class RtspAuthorizationAlgorithm
    {
        public static string ComputeAsBasic( string input )
        {
            if ( string.IsNullOrWhiteSpace( input ) )
            {
                return string.Empty;
            }

            var plainBytes = System.Text.Encoding.UTF8.GetBytes( input );

            if ( plainBytes == null || plainBytes.Length <= 0 )
            {
                return string.Empty;
            }

            return Convert.ToBase64String( plainBytes );
        }

        public static string ComputeAsMD5( string input )
        {
            if ( string.IsNullOrWhiteSpace( input ) )
            {
                return string.Empty;
            }

            return ComputeHash( MD5.Create() , input );
        }

        public static string ComputeAsSHA1( string input )
        {
            if ( string.IsNullOrWhiteSpace( input ) )
            {
                return string.Empty;
            }

            return ComputeHash( SHA1.Create() , input );
        }

        public static string ComputeAsSHA256( string input )
        {
            if ( string.IsNullOrWhiteSpace( input ) )
            {
                return string.Empty;
            }

            return ComputeHash( SHA256.Create() , input );
        }

        public static string ComputeAsSHA512( string input )
        {
            if ( string.IsNullOrWhiteSpace( input ) )
            {
                return string.Empty;
            }

            return ComputeHash( SHA512.Create() , input );
        }

        private static string ComputeHash( HashAlgorithm algorithm , string input )
        {
            using ( algorithm )
            {
                var plainBytes = System.Text.Encoding.UTF8.GetBytes( input );

                if ( plainBytes == null || plainBytes.Length <= 0 )
                {
                    return string.Empty;
                }

                var bytes = algorithm.ComputeHash( plainBytes );

                if ( bytes == null || bytes.Length <= 0 )
                {
                    return string.Empty;
                }

                var builder = new StringBuilder();

                for ( var i = 0 ; i < bytes.Length ; i++ )
                {
                    builder.Append( bytes[i].ToString( "x2" ) );
                }

                return builder.ToString();
            }
        }
    }
}
