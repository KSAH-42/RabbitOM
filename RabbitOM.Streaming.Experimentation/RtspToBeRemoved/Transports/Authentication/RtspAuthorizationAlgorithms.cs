using System;
using System.Security.Cryptography;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp2BeRemoved.Transports.Authentication
{
    public static class RtspAuthorizationAlgorithms
    {
        public static string ComputeAsBasic( string input )
        {
            if ( string.IsNullOrWhiteSpace( input ) )
            {
                return string.Empty;
            }

            return Convert.ToBase64String( Encoding.UTF8.GetBytes( input ) );
        }

        public static string ComputeAsMD5( string input )
        {
            return ComputeHash( input , MD5.Create );
        }

        public static string ComputeAsSHA1( string input )
        {
            return ComputeHash( input , SHA1.Create );
        }

        public static string ComputeAsSHA256( string input )
        {
            return ComputeHash( input , SHA256.Create );
        }

        public static string ComputeAsSHA384( string input )
        {
            return ComputeHash( input , SHA384.Create );
        }

        public static string ComputeAsSHA512( string input )
        {
            return ComputeHash( input , SHA512.Create );
        }

        private static string ComputeHash( string input , Func<HashAlgorithm> factory )
        {
            if ( string.IsNullOrWhiteSpace( input ) )
            {
                return string.Empty;
            }

            using ( var algorithm = factory() )
            {
                var bytes = algorithm.ComputeHash( Encoding.UTF8.GetBytes( input ) ) ?? Array.Empty<byte>();

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
