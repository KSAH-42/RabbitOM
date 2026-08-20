using System;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    internal sealed class RtspSecurityManager
    {
        private readonly object _lock;

        private readonly RtspConnector _proxy;

        private readonly RtspAuthorizationFactory _factory;

        public RtspSecurityManager( RtspConnector proxy )
        {
            _proxy   = proxy ?? throw new ArgumentNullException( nameof( proxy ) ) ;

            _lock    = new object();
            _factory = new RtspAuthorizationFactory();
        }

        public void Initialize()
        {
            lock ( _lock )
            {
                _factory.Initialize();
            }
        }

        public bool IsAuthenticationSetup()
        {
            lock ( _lock )
            {
                return _factory.IsAuthenticationSetup();
            }
        }

        public void SetupAuthentication( RtspMessageResponse response )
        {
            lock ( _lock )
            {
                _factory.SetupAuthentication( response?.Headers.FindByName( RtspHeaderNames.WWWAuthenticate ) );
            }
        }

        public bool AddAuthorization( RtspMessageRequest request )
        {
            if ( request == null || request.Method == RtspMethod.UnDefined )
            {
                return false;
            }

            lock ( _lock )
            {
                if ( ! _factory.IsAuthenticationSetup() )
                {
                    return false;
                }

                if ( ! RtspUri.TryParse( _proxy.Uri , out RtspUri uri ) || uri == null )
                {
                    return false;
                }

                uri.RemoveCredentials();
                
                _factory.UserName = _proxy.UserName;
                _factory.Password = _proxy.Password;

                if ( _factory.CanCreateBasicAuthorization() )
                {
                    return request.Headers.TryAddOrUpdate( _factory.CreateBasicAuthorization() );
                }

                if ( _factory.CanCreateDigestAuthorization() )
                {
                    if ( _factory.CanCreateDigestMD5Authorization() )
                    {
                        return request.Headers.TryAddOrUpdate( _factory.CreateDigestMD5Authorization( request.Method , uri.ToString() ) );
                    }

                    if ( _factory.CanCreateDigestSHA1Authorization() )
                    {
                        return request.Headers.TryAddOrUpdate( _factory.CreateDigestSHA1Authorization( request.Method , uri.ToString() ) );
                    }

                    if ( _factory.CanCreateDigestSHA256Authorization() )
                    {
                        return request.Headers.TryAddOrUpdate( _factory.CreateDigestSHA256Authorization( request.Method , uri.ToString() ) );
                    }

                    if ( _factory.CanCreateDigestSHA512Authorization() )
                    {
                        return request.Headers.TryAddOrUpdate( _factory.CreateDigestSHA512Authorization( request.Method , uri.ToString() ) );
                    }

                    return request.Headers.TryAddOrUpdate( _factory.CreateDigestAuthorization( request.Method , uri.ToString() ) );
                }

                return false;
            }
        }
    }
}
