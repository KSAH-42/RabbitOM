using System;

namespace RabbitOM.Streaming.Net.Rtsp
{
    /// <summary>
    /// Represent an authorization token generator
    /// </summary>
    internal sealed class RtspSHA256AuthorizationChallenge : RtspDigestAuthorizationChallenge
    {
        /// <summary>
        /// Create an authorization
        /// </summary>
        /// <returns>returns a value</returns>
        public override string CreateAuthorization()
        {
            string method    = RtspDataConverter.ConvertToString( Method );
            string hashA1    = RtspHashAlgorithm.ComputeSHA256Hash( UserName + ":" + Realm + ":" + Password );
            string hashA2    = RtspHashAlgorithm.ComputeSHA256Hash( method + ":" + Uri  );
            string response  = RtspHashAlgorithm.ComputeSHA256Hash( hashA1 + ":" + Nonce + ":" + hashA2 );

            return response;
        }
    }
}
