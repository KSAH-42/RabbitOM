using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent an authorization token generator
    /// </summary>
    internal sealed class RTSPSHA256AuthorizationChallenge : RTSPDigestAuthorizationChallenge
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="credentials">the credentials</param>
        public RTSPSHA256AuthorizationChallenge( RTSPCredentials credentials )
            : base( credentials )
        {
        }

        /// <summary>
        /// Create an authorization
        /// </summary>
        /// <returns>returns a value</returns>
        public override string CreateAuthorization()
        {
            string method    = RTSPDataConverter.ConvertToString( Method );
            string hashA1    = RTSPHashAlgorithm.ComputeSHA256Hash( Credentials.UserName + ":" + Realm + ":" + Credentials.Password );
            string hashA2    = RTSPHashAlgorithm.ComputeSHA256Hash( method + ":" + Uri  );
            string response  = RTSPHashAlgorithm.ComputeSHA256Hash( hashA1 + ":" + Nonce + ":" + hashA2 );

            return response;
        }
    }
}
