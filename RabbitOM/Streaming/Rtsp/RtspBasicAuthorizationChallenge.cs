using System;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent an authorization token generator
    /// </summary>
    internal sealed class RtspBasicAuthorizationChallenge : RtspAuthorizationChallenge
    {
        /// <summary>
        /// Create an authorization
        /// </summary>
        /// <returns>returns a value</returns>
        public override string CreateAuthorization()
        {
            return RtspDataConverter.ConvertToBase64( UserName + ":" + Password );
        }
    }
}
