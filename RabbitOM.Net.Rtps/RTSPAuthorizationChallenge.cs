using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent an authorization token generator
    /// </summary>
    public abstract class RTSPAuthorizationChallenge
    {
        /// <summary>
        /// Gets the credentials
        /// </summary>
        public abstract RTSPCredentials Credentials
        {
            get;
        }

        /// <summary>
        /// Perform a validation
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public abstract bool Validate();

        /// <summary>
        /// Create an authorization
        /// </summary>
        /// <returns>returns a value</returns>
        public abstract string CreateAuthorization();
    }
}
