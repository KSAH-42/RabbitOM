using System;

namespace RabbitOM.Streaming.Net.Rtsp
{
    /// <summary>
    /// Represent a authorization factory used to create message authorization header in case an authentication is required when the server response ask to the client to include an authorization header on it's message request.
    /// </summary>
    internal sealed class RtspAuthorizationFactory
    {
        private RtspHeaderWWWAuthenticate _header;

        private string _userName = string.Empty;

        private string _password = string.Empty;





        /// <summary>
        /// Gets / Sets the username
        /// </summary>
        public string UserName
        {
            get => _userName;
            set => _userName = value ?? string.Empty;
        }

        /// <summary>
        /// Gets / Sets the password
        /// </summary>
        public string Password
        {
            get => _password;
            set => _password = value ?? string.Empty;
        }







        /// <summary>
        /// Initialize
        /// </summary>
        public void Initialize()
        {
            _header   = null;
            _userName = string.Empty;
            _password = string.Empty;
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
        /// Check if a SHA 1 authorization can be created
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool CanCreateDigestSHA1Authorization()
        {
            return _header != null && _header.Type == RtspAuthenticationType.Digest && _header.Algorithm == RtspDigestAlgorithmType.SHA_1;
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
        /// <returns>returns a header, otherwise null</returns>
        public RtspHeader CreateBasicAuthorization()
        {
            if ( _header == null )
            {
                return null;
            }

            var challenge = new RtspBasicAuthorizationChallenge()
            {
                UserName = _userName ,
                Password = _password
            };

            if ( ! challenge.TryValidate() )
            {
                return null;
            }

            return new RtspHeaderAuthorization()
            {
                Type     = RtspAuthenticationType.Basic ,
                Response = challenge.CreateAuthorization()
            };
        }

        /// <summary>
        /// Create an authorization header
        /// </summary>
        /// <param name="method">the method</param>
        /// <param name="uri">the uri</param>
        /// <returns>returns a header, otherwise null</returns>
        public RtspHeader CreateDigestAuthorization( RtspMethod method , string uri )
        {
            return CreateDigestAuthorization( new RtspMD5AuthorizationChallenge() , method , uri );
        }

        /// <summary>
        /// Create an authorization header
        /// </summary>
        /// <param name="method">the method</param>
        /// <param name="uri">the uri</param>
        /// <returns>returns a header, otherwise null</returns>
        public RtspHeader CreateDigestMD5Authorization( RtspMethod method , string uri )
        {
            return CreateDigestAuthorization( new RtspMD5AuthorizationChallenge() , method , uri );
        }

        /// <summary>
        /// Create an authorization header
        /// </summary>
        /// <param name="method">the method</param>
        /// <param name="uri">the uri</param>
        /// <returns>returns a header, otherwise null</returns>
        public RtspHeader CreateDigestSHA1Authorization( RtspMethod method , string uri )
        {
            return CreateDigestAuthorization( new RtspSHA1AuthorizationChallenge() , method , uri );
        }

        /// <summary>
        /// Create an authorization header
        /// </summary>
        /// <param name="method">the method</param>
        /// <param name="uri">the uri</param>
        /// <returns>returns a header, otherwise null</returns>
        public RtspHeader CreateDigestSHA256Authorization( RtspMethod method , string uri )
        {
            return CreateDigestAuthorization( new RtspSHA256AuthorizationChallenge() , method , uri );
        }

        /// <summary>
        /// Create an authorization header
        /// </summary>
        /// <param name="method">the method</param>
        /// <param name="uri">the uri</param>
        /// <returns>returns a header, otherwise null</returns>
        public RtspHeader CreateDigestSHA512Authorization( RtspMethod method , string uri )
        {
            return CreateDigestAuthorization( new RtspSHA512AuthorizationChallenge() , method , uri );
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

            challenge.Method   = method;
            challenge.Uri      = uri;
            challenge.Realm    = _header.Realm;
            challenge.Nonce    = _header.Nonce;
            challenge.UserName = _userName;
            challenge.Password = _password;

            if ( ! challenge.TryValidate() )
            {
                return null;
            }

            return new RtspHeaderAuthorization()
            {
                Type     = RtspAuthenticationType.Digest ,
                Realm    = _header.Realm ,
                Nonce    = _header.Nonce ,
                Opaque   = _header.Opaque ,
                Uri      = challenge.Uri ,
                UserName = challenge.UserName ,
                Response = challenge.CreateAuthorization()
            };
        }
    }
}
