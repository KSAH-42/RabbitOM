// TODO: to fix add: MD5-sess => (Not HA2 must be recheck) -> HA1 = MD5( MD5( username:realm:password ):nonce:cnonce) 
// TODO: don't forget the same with (Not HA2 must be recheck) -> HA1 = SHA( SHA( username:realm:password ):nonce:cnonce) 
using System;

namespace RabbitOM.Net.RtspV2.Authentication
{
    using RabbitOM.Net.RtspV2.Headers.DataTypes;

    public sealed class DigestRtspAuthorizationResponseBuilder
    {
        public string Method { get; set; }

        public string Algorithm { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Uri { get; set; }

        public string Realm { get; set; }

        public string Nonce { get; set; }

        public string NonceCount { get; set; }

        public string ClientNonce { get; set; }

        public string QualityOfProtection { get; set; }

        public bool IsSession { get; set; } // to be used in case the scheme include the "sess" key word 




        public override string ToString()
        {
            if ( string.IsNullOrWhiteSpace( UserName ) || string.IsNullOrWhiteSpace( Password ) )
            {
                return string.Empty;
            }

            if ( string.IsNullOrWhiteSpace( Method ) || string.IsNullOrWhiteSpace( Uri ) || string.IsNullOrWhiteSpace( Realm ) || string.IsNullOrWhiteSpace( Nonce ) )
            {
                return string.Empty;
            }

            DigestRtspAlgorithm algorithm = null;

            if ( SupportedTypes.IsMd5Algorithm( Algorithm ) )
            {
                algorithm = DigestRtspAlgorithm.CreateMD5();
            }
            else if ( SupportedTypes.IsSha1Algorithm( Algorithm ) )
            {
                algorithm = DigestRtspAlgorithm.CreateSHA1();
            }
            else if ( SupportedTypes.IsSha256Algorithm( Algorithm ) )
            {
                algorithm = DigestRtspAlgorithm.CreateSHA256();
            }
            else if ( SupportedTypes.IsSha384Algorithm( Algorithm ) )
            {
                algorithm = DigestRtspAlgorithm.CreateSHA384();
            }
            else if ( SupportedTypes.IsSha512Algorithm( Algorithm ) )
            {
                algorithm = DigestRtspAlgorithm.CreateSHA512();
            }

            if ( algorithm == null ) { return string.Empty; }

            using ( algorithm )
            {
                return OnBuildResponse( algorithm );
            }
        }







        private string OnBuildResponse( DigestRtspAlgorithm algorithm )
        {
            var hash1 = algorithm.Compute( UserName + ":" + Realm + ":" + Password );
            var hash2 = algorithm.Compute( Method + ":" + Uri );

            return string.IsNullOrWhiteSpace( QualityOfProtection )
                ? algorithm.Compute( $"{hash1}:{Nonce}:{hash2}")
                : algorithm.Compute( $"{hash1}:{Nonce}:{NonceCount}:{ClientNonce}:{QualityOfProtection}:{hash2}");
        }
    }
}
