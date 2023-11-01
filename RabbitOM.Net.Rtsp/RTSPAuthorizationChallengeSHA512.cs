using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent an authorization token generator
    /// </summary>
    internal sealed class RTSPAuthorizationChallengeSHA512 : RTSPAuthorizationChallengeDigest
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="credentials">the credentials</param>
        public RTSPAuthorizationChallengeSHA512( RTSPCredentials credentials )
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
            string hashA1    = RTSPHashAlgorithm.ComputeSHA512Hash( Credentials.UserName + ":" + Realm + ":" + Credentials.Password );
            string hashA2    = RTSPHashAlgorithm.ComputeSHA512Hash( method + ":" + Uri  );
            string response  = RTSPHashAlgorithm.ComputeSHA512Hash( hashA1 + ":" + Nonce + ":" + hashA2 );

            return response;
        }
    }
}
