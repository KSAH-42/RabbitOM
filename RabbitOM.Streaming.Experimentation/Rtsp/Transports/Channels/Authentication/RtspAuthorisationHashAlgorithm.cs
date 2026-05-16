using System;
using System.Security.Cryptography;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Authentication
{
    public sealed class RtspAuthorisationHashAlgorithm : IDisposable
    {
        private readonly HashAlgorithm _hashAlgorithm;



        private RtspAuthorisationHashAlgorithm( HashAlgorithm hashAlgorithm )
        {
            _hashAlgorithm = hashAlgorithm ?? throw new ArgumentNullException( nameof( hashAlgorithm ) );
        }



        public static RtspAuthorisationHashAlgorithm CreateMD5()
        {
            return new RtspAuthorisationHashAlgorithm( MD5.Create() );
        }

        public static RtspAuthorisationHashAlgorithm CreateSHA1()
        {
            return new RtspAuthorisationHashAlgorithm( SHA1.Create() );
        }

        public static RtspAuthorisationHashAlgorithm CreateSHA256()
        {
            return new RtspAuthorisationHashAlgorithm( SHA256.Create() );
        }

        public static RtspAuthorisationHashAlgorithm CreateSHA384()
        {
            return new RtspAuthorisationHashAlgorithm( SHA384.Create() );
        }

        public static RtspAuthorisationHashAlgorithm CreateSHA512()
        {
            return new RtspAuthorisationHashAlgorithm( SHA512.Create() );
        }




        public string Compute( string input )
        {
            if ( string.IsNullOrWhiteSpace( input ) )
            {
                return string.Empty;
            }

            var bytes = _hashAlgorithm.ComputeHash( Encoding.UTF8.GetBytes( input ) ) ?? Array.Empty<byte>();

            var builder = new StringBuilder();
                
            for ( var i = 0 ; i < bytes.Length ; i++ )
            {
                builder.Append( bytes[i].ToString( "x2" ) );
            }

            return builder.ToString();
        }

        public void Dispose()
        {
            _hashAlgorithm.Dispose();
        }
    }
}
