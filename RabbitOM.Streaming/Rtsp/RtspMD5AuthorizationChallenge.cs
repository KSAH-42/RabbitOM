using System;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent an authorization token generator
    /// </summary>
    internal sealed class RtspMD5AuthorizationChallenge : RtspDigestAuthorizationChallenge
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="credentials">the credentials</param>
        public RtspMD5AuthorizationChallenge( RtspCredentials credentials )
            : base( credentials )
        {
        }

        /// <summary>
        /// Create an authorization
        /// </summary>
        /// <returns>returns a value</returns>
        public override string CreateAuthorization()
        {
            string method    = RtspDataConverter.ConvertToString( Method );
            string hashA1    = RtspHashAlgorithm.ComputeMD5Hash( Credentials.UserName + ":" + Realm + ":" + Credentials.Password );
            string hashA2    = RtspHashAlgorithm.ComputeMD5Hash( method + ":" + Uri  );
            string response  = RtspHashAlgorithm.ComputeMD5Hash( hashA1 + ":" + Nonce + ":" + hashA2 );

            return response;
        }
    }
}
