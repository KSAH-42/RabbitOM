using System;

namespace RabbitOM.Net.Rtps
{
    /// <summary>
    /// Represent a authorization factory used to create message authorization header in case an authentication is required when the server response ask to the client to include an authorization header on it's message request.
    /// </summary>
    public sealed class RTSPAuthorizationFactory
    {
        private RTSPHeaderWWWAuthenticate _header = null;

        /// <summary>
        /// Initialize
        /// </summary>
        public void Initialize()
        {
            _header = null;
        }

        /// <summary>
        /// Check if the current instance has already been registered with a WWW authentication header
        /// </summary>
        public bool IsAuthenticationSetup()
        {
            return _header != null;
        }

        /// <summary>
        /// Store in memory the authentication header which came from the server response. This header will be used to calculate the right the authorization header for client
        /// </summary>
        /// <param name="header">the authentication header that came from the server response</param>
        public void SetupAuthentication( RTSPHeader header )
        {
            _header = header as RTSPHeaderWWWAuthenticate;
        }

        /// <summary>
        /// Check if a basic authorization can be created
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool CanCreateBasicAuthorization()
        {
            return _header != null && _header.Type == RTSPAuthenticationType.Basic;
        }

        /// <summary>
        /// Check if any digest authorization can be created
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool CanCreateDigestAuthorization()
        {
            return _header != null && _header.Type == RTSPAuthenticationType.Digest;
        }

        /// <summary>
        /// Check if a MD5 authorization can be created
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool CanCreateDigestMD5Authorization()
        {
            return _header != null && _header.Type == RTSPAuthenticationType.Digest && _header.Algorithm == RTSPDigestAlgorithmType.MD5;
        }

        /// <summary>
        /// Check if a SHA 256 authorization can be created
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool CanCreateDigestSHA256Authorization()
        {
            return _header != null && _header.Type == RTSPAuthenticationType.Digest && _header.Algorithm == RTSPDigestAlgorithmType.SHA_256;
        }

        /// <summary>
        /// Check if a SHA 512 authorization can be created
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool CanCreateDigestSHA512Authorization()
        {
            return _header != null && _header.Type == RTSPAuthenticationType.Digest && _header.Algorithm == RTSPDigestAlgorithmType.SHA_512;
        }

        /// <summary>
        /// Create an authorization header
        /// </summary>
        /// <param name="credentials">the credentials</param>
        /// <returns>returns a header, otherwise null</returns>
        public RTSPHeader CreateBasicAuthorization( RTSPCredentials credentials )
        {
            if ( _header == null || credentials == null )
            {
                return null;
            }

            var challenge = new RTSPAuthorizationChallengeBasic( credentials );

            if ( !challenge.Validate() )
            {
                return null;
            }

            return new RTSPHeaderAuthorization()
            {
                Type = RTSPAuthenticationType.Basic ,
                Response = challenge.CreateAuthorization()
            };
        }

        /// <summary>
        /// Create an authorization header
        /// </summary>
        /// <param name="credentials">the credentials</param>
        /// <param name="method">the method</param>
        /// <param name="uri">the uri</param>
        /// <returns>returns a header, otherwise null</returns>
        public RTSPHeader CreateDigestAuthorization( RTSPCredentials credentials , RTSPMethodType method , string uri )
        {
            return CreateDigestAuthorization( new RTSPAuthorizationChallengeMD5( credentials ) , method , uri );
        }

        /// <summary>
        /// Create an authorization header
        /// </summary>
        /// <param name="credentials">the credentials</param>
        /// <param name="method">the method</param>
        /// <param name="uri">the uri</param>
        /// <returns>returns a header, otherwise null</returns>
        public RTSPHeader CreateDigestMD5Authorization( RTSPCredentials credentials , RTSPMethodType method , string uri )
        {
            return CreateDigestAuthorization( new RTSPAuthorizationChallengeMD5( credentials ) , method , uri );
        }

        /// <summary>
        /// Create an authorization header
        /// </summary>
        /// <param name="credentials">the credentials</param>
        /// <param name="method">the method</param>
        /// <param name="uri">the uri</param>
        /// <returns>returns a header, otherwise null</returns>
        public RTSPHeader CreateDigestSHA256Authorization( RTSPCredentials credentials , RTSPMethodType method , string uri )
        {
            return CreateDigestAuthorization( new RTSPAuthorizationChallengeSHA256( credentials ) , method , uri );
        }

        /// <summary>
        /// Create an authorization header
        /// </summary>
        /// <param name="credentials">the credentials</param>
        /// <param name="method">the method</param>
        /// <param name="uri">the uri</param>
        /// <returns>returns a header, otherwise null</returns>
        public RTSPHeader CreateDigestSHA512Authorization( RTSPCredentials credentials , RTSPMethodType method , string uri )
        {
            return CreateDigestAuthorization( new RTSPAuthorizationChallengeSHA512( credentials ) , method , uri );
        }

        /// <summary>
        /// Create an authorization header
        /// </summary>
        /// <param name="method">the method</param>
        /// <param name="uri">the uri</param>
        /// <param name="challenge">the authorization value generator</param>
        /// <returns>returns a header, otherwise null</returns>
        private RTSPHeader CreateDigestAuthorization( RTSPAuthorizationChallengeDigest challenge , RTSPMethodType method , string uri )
        {
            if ( _header == null || challenge == null )
            {
                return null;
            }

            challenge.Method = method;
            challenge.Uri    = uri;
            challenge.Realm  = _header.Realm;
            challenge.Nonce  = _header.Nonce;

            if ( !challenge.Validate() )
            {
                return null;
            }

            return new RTSPHeaderAuthorization()
            {
                Type = RTSPAuthenticationType.Digest ,
                Realm = _header.Realm ,
                Nonce = _header.Nonce ,
                Opaque = _header.Opaque ,
                Uri = challenge.Uri ,
                UserName = challenge.Credentials.UserName ,
                Response = challenge.CreateAuthorization()
            };
        }
    }
}
