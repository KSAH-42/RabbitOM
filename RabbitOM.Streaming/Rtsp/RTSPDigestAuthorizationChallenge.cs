using System;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent an authorization token generator
    /// </summary>
    internal abstract class RTSPDigestAuthorizationChallenge : RTSPAuthorizationChallenge
    {
        private RTSPMethod               _method      = RTSPMethod.UnDefined;

        private string                   _uri         = string.Empty;

        private string                   _realm       = string.Empty;

        private string                   _nonce       = string.Empty;

        private readonly RTSPCredentials _credentials = RTSPCredentials.Empty;




        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="credentials">the credentials</param>
        /// <exception cref="ArgumentNullException"/>
        protected RTSPDigestAuthorizationChallenge( RTSPCredentials credentials )
        {
            _credentials = credentials ?? throw new ArgumentNullException( nameof( credentials ) );
        }




        /// <summary>
        /// Gets / Sets the method
        /// </summary>
        public RTSPMethod Method
        {
            get => _method;
            set => _method = value;
        }

        /// <summary>
        /// Gets / Sets the nonce
        /// </summary>
        public string Uri
        {
            get => _uri;
            set => _uri = value ?? string.Empty;
        }

        /// <summary>
        /// Gets / Sets the realm
        /// </summary>
        public string Realm
        {
            get => _realm;
            set => _realm = value ?? string.Empty;
        }

        /// <summary>
        /// Gets / Sets the nonce
        /// </summary>
        public string Nonce
        {
            get => _nonce;
            set => _nonce = value ?? string.Empty;
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
            if ( _method == RTSPMethod.UnDefined )
            {
                return false;
            }

            return !string.IsNullOrWhiteSpace( _uri )
                && !string.IsNullOrWhiteSpace( _realm )
                && !string.IsNullOrWhiteSpace( _nonce )
                && _credentials.TryValidate();
        }
    }
}
