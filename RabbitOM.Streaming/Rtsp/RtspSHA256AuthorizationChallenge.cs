using System;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent an authorization token generator
    /// </summary>
    internal sealed class RtspSHA256AuthorizationChallenge : RtspDigestAuthorizationChallenge
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="credentials">the credentials</param>
        public RtspSHA256AuthorizationChallenge( RtspCredentials credentials )
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
            string hashA1    = RtspHashAlgorithm.ComputeSHA256Hash( Credentials.UserName + ":" + Realm + ":" + Credentials.Password );
            string hashA2    = RtspHashAlgorithm.ComputeSHA256Hash( method + ":" + Uri  );
            string response  = RtspHashAlgorithm.ComputeSHA256Hash( hashA1 + ":" + Nonce + ":" + hashA2 );

            return response;
        }
    }
}
