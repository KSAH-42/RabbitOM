using System;
using System.Security.Cryptography;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Authentication
{
    internal sealed class RtspHashAlgorithm : IDisposable
    {
        private readonly HashAlgorithm _hashAlgorithm;

        private RtspHashAlgorithm( HashAlgorithm hashAlgorithm )
        {
            _hashAlgorithm = hashAlgorithm ?? throw new ArgumentNullException( nameof( hashAlgorithm ) );
        }

        public static RtspHashAlgorithm CreateMD5() => new RtspHashAlgorithm( MD5.Create() );

        public static RtspHashAlgorithm CreateSHA1() => new RtspHashAlgorithm( SHA1.Create() );

        public static RtspHashAlgorithm CreateSHA256() => new RtspHashAlgorithm( SHA256.Create() );

        public static RtspHashAlgorithm CreateSHA384() => new RtspHashAlgorithm( SHA384.Create() );

        public static RtspHashAlgorithm CreateSHA512() => new RtspHashAlgorithm( SHA512.Create() );

        public string Compute( string input )
        {
            if ( string.IsNullOrWhiteSpace( input ) )
            {
                throw new ArgumentException( nameof( input ) );
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
