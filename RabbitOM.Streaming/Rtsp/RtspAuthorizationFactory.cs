using System;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent a authorization factory used to create message authorization header in case an authentication is required when the server response ask to the client to include an authorization header on it's message request.
    /// </summary>
    internal sealed class RtspAuthorizationFactory
    {
        private RtspHeaderWWWAuthenticate _header = null;

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
        public void SetupAuthentication( RtspHeader header )
        {
            _header = header as RtspHeaderWWWAuthenticate;
        }

        /// <summary>
        /// Check if a basic authorization can be created
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool CanCreateBasicAuthorization()
        {
            return _header != null && _header.Type == RtspAuthenticationType.Basic;
        }

        /// <summary>
        /// Check if any digest authorization can be created
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool CanCreateDigestAuthorization()
        {
            return _header != null && _header.Type == RtspAuthenticationType.Digest;
        }

        /// <summary>
        /// Check if a MD5 authorization can be created
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool CanCreateDigestMD5Authorization()
        {
            return _header != null && _header.Type == RtspAuthenticationType.Digest && _header.Algorithm == RtspDigestAlgorithmType.MD5;
        }

        /// <summary>
        /// Check if a SHA 256 authorization can be created
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool CanCreateDigestSHA256Authorization()
        {
            return _header != null && _header.Type == RtspAuthenticationType.Digest && _header.Algorithm == RtspDigestAlgorithmType.SHA_256;
        }

        /// <summary>
        /// Check if a SHA 512 authorization can be created
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool CanCreateDigestSHA512Authorization()
        {
            return _header != null && _header.Type == RtspAuthenticationType.Digest && _header.Algorithm == RtspDigestAlgorithmType.SHA_512;
        }

        /// <summary>
        /// Create an authorization header
        /// </summary>
        /// <param name="credentials">the credentials</param>
        /// <returns>returns a header, otherwise null</returns>
        public RtspHeader CreateBasicAuthorization( RtspCredentials credentials )
        {
            if ( _header == null || credentials == null )
            {
                return null;
            }

            var challenge = new RtspBasicAuthorizationChallenge( credentials );

            if ( !challenge.TryValidate() )
            {
                return null;
            }

            return new RtspHeaderAuthorization()
            {
                Type = RtspAuthenticationType.Basic ,
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
        public RtspHeader CreateDigestAuthorization( RtspCredentials credentials , RtspMethod method , string uri )
        {
            return CreateDigestAuthorization( new RtspMD5AuthorizationChallenge( credentials ) , method , uri );
        }

        /// <summary>
        /// Create an authorization header
        /// </summary>
        /// <param name="credentials">the credentials</param>
        /// <param name="method">the method</param>
        /// <param name="uri">the uri</param>
        /// <returns>returns a header, otherwise null</returns>
        public RtspHeader CreateDigestMD5Authorization( RtspCredentials credentials , RtspMethod method , string uri )
        {
            return CreateDigestAuthorization( new RtspMD5AuthorizationChallenge( credentials ) , method , uri );
        }

        /// <summary>
        /// Create an authorization header
        /// </summary>
        /// <param name="credentials">the credentials</param>
        /// <param name="method">the method</param>
        /// <param name="uri">the uri</param>
        /// <returns>returns a header, otherwise null</returns>
        public RtspHeader CreateDigestSHA256Authorization( RtspCredentials credentials , RtspMethod method , string uri )
        {
            return CreateDigestAuthorization( new RtspSHA256AuthorizationChallenge( credentials ) , method , uri );
        }

        /// <summary>
        /// Create an authorization header
        /// </summary>
        /// <param name="credentials">the credentials</param>
        /// <param name="method">the method</param>
        /// <param name="uri">the uri</param>
        /// <returns>returns a header, otherwise null</returns>
        public RtspHeader CreateDigestSHA512Authorization( RtspCredentials credentials , RtspMethod method , string uri )
        {
            return CreateDigestAuthorization( new RtspSHA512AuthorizationChallenge( credentials ) , method , uri );
        }

        /// <summary>
        /// Create an authorization header
        /// </summary>
        /// <param name="challenge">the authorization value generator</param>
        /// <param name="method">the method</param>
        /// <param name="uri">the uri</param>
        /// <returns>returns a header, otherwise null</returns>
        private RtspHeader CreateDigestAuthorization( RtspDigestAuthorizationChallenge challenge , RtspMethod method , string uri )
        {
            if ( _header == null || challenge == null )
            {
                return null;
            }

            challenge.Method = method;
            challenge.Uri    = uri;
            challenge.Realm  = _header.Realm;
            challenge.Nonce  = _header.Nonce;

            if ( ! challenge.TryValidate() )
            {
                return null;
            }

            return new RtspHeaderAuthorization()
            {
                Type = RtspAuthenticationType.Digest ,
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
