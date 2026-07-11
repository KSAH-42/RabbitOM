using System;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Authentication
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.DataTypes;

    public sealed class RtspAuthorizationResponseBuilder
    {
        public string Method { get; set; }

        public string Scheme { get; set; }

        public string Algorithm { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Uri { get; set; }

        public string Realm { get; set; }

        public string Nonce { get; set; }

        public string NonceCount { get; set; }

        public string ClientNonce { get; set; }

        public string QualityOfProtection { get; set; }





        public override string ToString()
        {
            if ( string.IsNullOrWhiteSpace( UserName ) || string.IsNullOrWhiteSpace( Password ) )
            {
                return string.Empty;
            }

            if ( SupportedTypes.IsBasicAuthentication( Scheme ) )
            {
                return BuildBasicResponse();
            }

            if ( SupportedTypes.IsDigestAuthentication( Scheme ) )
            {
                if ( string.IsNullOrWhiteSpace( Method ) || string.IsNullOrWhiteSpace( Uri ) || string.IsNullOrWhiteSpace( Realm ) || string.IsNullOrWhiteSpace( Nonce ) )
                {
                    return string.Empty;
                }

                if ( SupportedTypes.IsMd5Algorithm( Algorithm ) )
                {
                    return BuildDigestResponse( RtspHashAlgorithm.CreateMD5() );
                }

                if ( SupportedTypes.IsSha1Algorithm( Algorithm ) )
                {
                    return BuildDigestResponse( RtspHashAlgorithm.CreateSHA1() );
                }

                if ( SupportedTypes.IsSha256Algorithm( Algorithm ) )
                {
                    return BuildDigestResponse( RtspHashAlgorithm.CreateSHA256() );
                }

                if ( SupportedTypes.IsSha384Algorithm( Algorithm ) )
                {
                    return BuildDigestResponse( RtspHashAlgorithm.CreateSHA384() );
                }

                if ( SupportedTypes.IsSha512Algorithm( Algorithm ) )
                {
                    return BuildDigestResponse( RtspHashAlgorithm.CreateSHA512() );
                }
            }

            return string.Empty;
        }

        private string BuildBasicResponse()
        {
            return Convert.ToBase64String( Encoding.UTF8.GetBytes( $"{UserName}:{Password}" ) );
        }

        private string BuildDigestResponse( RtspHashAlgorithm algorithm )
        {
            using ( algorithm )
            {
                var hash1 = algorithm.Compute( UserName + ":" + Realm + ":" + Password );
                var hash2 = algorithm.Compute( Method + ":" + Uri  );

                return string.IsNullOrWhiteSpace( QualityOfProtection )
                    ? algorithm.Compute( $"{hash1}:{Nonce}:{hash2}")
                    : algorithm.Compute( $"{hash1}:{Nonce}:{NonceCount}:{ClientNonce}:{QualityOfProtection}:{hash2}");
            }
        }
    }
}
