using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent an authorization token generator
    /// </summary>
    internal sealed class RTSPAuthorizationChallengeMD5 : RTSPAuthorizationChallengeDigest
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="credentials">the credentials</param>
        public RTSPAuthorizationChallengeMD5( RTSPCredentials credentials )
            : base( credentials )
        {
        }

        /// <summary>
        /// Create an authorization
        /// </summary>
        /// <returns>returns a value</returns>
        public override string CreateAuthorization()
        {
            string method    = RTSPMethodTypeConverter.Convert( Method );
            string hashA1    = RTSPHashAlgorithm.ComputeMD5Hash( Credentials.UserName + ":" + Realm + ":" + Credentials.Password );
            string hashA2    = RTSPHashAlgorithm.ComputeMD5Hash( method + ":" + Uri  );
            string response  = RTSPHashAlgorithm.ComputeMD5Hash( hashA1 + ":" + Nonce + ":" + hashA2 );

            return response;
        }
    }
}
