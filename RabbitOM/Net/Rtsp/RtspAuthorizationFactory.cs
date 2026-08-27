using System;

namespace RabbitOM.Net.Rtsp
{
    using RabbitOM.Net.Rtsp.Headers;

    internal sealed class RtspAuthorizationFactory
    {
        private WWWAuthenticateRtspHeader _header;

        private string _userName = string.Empty;

        private string _password = string.Empty;





        public string UserName
        {
            get => _userName;
            set => _userName = value ?? string.Empty;
        }

        public string Password
        {
            get => _password;
            set => _password = value ?? string.Empty;
        }







        public void Initialize()
        {
            _header   = null;
            _userName = string.Empty;
            _password = string.Empty;
        }

        public bool IsAuthenticationSetup()
        {
            return _header != null;
        }

        public void SetupAuthentication( RtspHeader header )
        {
            _header = header as WWWAuthenticateRtspHeader;
        }

        public bool CanCreateBasicAuthorization()
        {
            return _header != null && _header.Type == RtspAuthenticationType.Basic;
        }

        public bool CanCreateDigestAuthorization()
        {
            return _header != null && _header.Type == RtspAuthenticationType.Digest;
        }

        public bool CanCreateDigestMD5Authorization()
        {
            return _header != null && _header.Type == RtspAuthenticationType.Digest && _header.Algorithm == RtspDigestAlgorithmType.MD5;
        }

        public bool CanCreateDigestSHA1Authorization()
        {
            return _header != null && _header.Type == RtspAuthenticationType.Digest && _header.Algorithm == RtspDigestAlgorithmType.SHA_1;
        }

        public bool CanCreateDigestSHA256Authorization()
        {
            return _header != null && _header.Type == RtspAuthenticationType.Digest && _header.Algorithm == RtspDigestAlgorithmType.SHA_256;
        }

        public bool CanCreateDigestSHA512Authorization()
        {
            return _header != null && _header.Type == RtspAuthenticationType.Digest && _header.Algorithm == RtspDigestAlgorithmType.SHA_512;
        }

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

            return new AuthorizationRtspHeader()
            {
                Type     = RtspAuthenticationType.Basic ,
                Response = challenge.CreateAuthorization()
            };
        }

        public RtspHeader CreateDigestAuthorization( RtspMethod method , string uri )
        {
            return CreateDigestAuthorization( new RtspMD5AuthorizationChallenge() , method , uri );
        }

        public RtspHeader CreateDigestMD5Authorization( RtspMethod method , string uri )
        {
            return CreateDigestAuthorization( new RtspMD5AuthorizationChallenge() , method , uri );
        }

        public RtspHeader CreateDigestSHA1Authorization( RtspMethod method , string uri )
        {
            return CreateDigestAuthorization( new RtspSHA1AuthorizationChallenge() , method , uri );
        }

        public RtspHeader CreateDigestSHA256Authorization( RtspMethod method , string uri )
        {
            return CreateDigestAuthorization( new RtspSHA256AuthorizationChallenge() , method , uri );
        }

        public RtspHeader CreateDigestSHA512Authorization( RtspMethod method , string uri )
        {
            return CreateDigestAuthorization( new RtspSHA512AuthorizationChallenge() , method , uri );
        }

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

            return new AuthorizationRtspHeader()
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
