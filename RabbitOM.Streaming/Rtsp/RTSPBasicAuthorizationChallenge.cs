using System;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent an authorization token generator
    /// </summary>
    internal sealed class RTSPBasicAuthorizationChallenge : RTSPAuthorizationChallenge
    {
        private readonly RTSPCredentials _credentials = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="credentials">the credentials</param>
        /// <exception cref="ArgumentNullException"/>
        public RTSPBasicAuthorizationChallenge( RTSPCredentials credentials )
        {
            _credentials = credentials ?? throw new ArgumentNullException( nameof( credentials ) );
        }

        /// <summary>
        /// Gets the credentials
        /// </summary>
        public override RTSPCredentials Credentials
        {
            get => _credentials;
        }

        /// <summary>
        /// Perform a validation
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool TryValidate()
        {
            return _credentials.TryValidate();
        }

        /// <summary>
        /// Create an authorization
        /// </summary>
        /// <returns>returns a value</returns>
        public override string CreateAuthorization()
        {
            return RTSPDataConverter.ConvertToBase64( _credentials.UserName + ":" + _credentials.Password );
        }
    }
}
