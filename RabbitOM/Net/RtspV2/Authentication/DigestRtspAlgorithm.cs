using System;
using System.Text;
using System.Security.Cryptography;

namespace RabbitOM.Net.RtspV2.Authentication
{
    public sealed class DigestRtspAlgorithm : IDisposable
    {
        private readonly HashAlgorithm _hashAlgorithm;

        private DigestRtspAlgorithm( HashAlgorithm hashAlgorithm ) => _hashAlgorithm = hashAlgorithm ?? throw new ArgumentNullException( nameof( hashAlgorithm ) );

        public static DigestRtspAlgorithm CreateMD5() => new DigestRtspAlgorithm( MD5.Create() );

        public static DigestRtspAlgorithm CreateSHA1() => new DigestRtspAlgorithm( SHA1.Create() );

        public static DigestRtspAlgorithm CreateSHA256() => new DigestRtspAlgorithm( SHA256.Create() );

        public static DigestRtspAlgorithm CreateSHA384() => new DigestRtspAlgorithm( SHA384.Create() );

        public static DigestRtspAlgorithm CreateSHA512() => new DigestRtspAlgorithm( SHA512.Create() );

        public string Compute( string input )
        {
            if ( string.IsNullOrWhiteSpace( input ) )
            {
                throw new ArgumentException( nameof( input ) );
            }

            var bytes = _hashAlgorithm.ComputeHash( Encoding.UTF8.GetBytes( input ) );

            var builder = new StringBuilder();

            for ( var i = 0 ; i < bytes.Length ; i++ )
            {
                builder.AppendFormat( "{0:x2}" , bytes[i] );
            }

            return builder.ToString();
        }

        public void Dispose()
        {
            _hashAlgorithm.Dispose();
        }
    }
}
