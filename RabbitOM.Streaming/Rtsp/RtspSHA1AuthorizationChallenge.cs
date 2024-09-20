using System;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent an authorization token generator
    /// </summary>
    internal sealed class RtspSHA1AuthorizationChallenge : RtspDigestAuthorizationChallenge
    {
        /// <summary>
        /// Create an authorization
        /// </summary>
        /// <returns>returns a value</returns>
        public override string CreateAuthorization()
        {
            string method    = RtspDataConverter.ConvertToString( Method );
            string hashA1    = RtspHashAlgorithm.ComputeSHA1Hash( UserName + ":" + Realm + ":" + Password );
            string hashA2    = RtspHashAlgorithm.ComputeSHA1Hash( method + ":" + Uri  );
            string response  = RtspHashAlgorithm.ComputeSHA1Hash( hashA1 + ":" + Nonce + ":" + hashA2 );

            return response;
        }
    }
}
