using System;

namespace RabbitOM.Net.Rtsp.Remoting
{
    /// <summary>
    /// Represent the client security manager. This is class is used to manage authentication procedures
    /// </summary>
    internal sealed class RTSPProxySecurityManager
    {
        private readonly object                    _lock     = null;

        private readonly RTSPProxy                 _proxy    = null;

        private readonly RTSPAuthorizationFactory  _factory  = null;





        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="proxy">the proxy</param>
        /// <exception cref="ArgumentNullException"/>
        public RTSPProxySecurityManager( RTSPProxy proxy )
        {
            if ( proxy == null )
            {
                throw new ArgumentNullException( nameof( proxy ) );
            }

            _lock = new object();
            _factory = new RTSPAuthorizationFactory();
            _proxy = proxy;
        }





        /// <summary>
        /// Initialize
        /// </summary>
        public void Initialize()
        {
            lock ( _lock )
            {
                _factory.Initialize();
            }
        }

        /// <summary>
        /// Check if the current instance has already been registered with a WWW authentication header
        /// </summary>
        public bool IsAuthenticationSetup()
        {
            lock ( _lock )
            {
                return _factory.IsAuthenticationSetup();
            }
        }

        /// <summary>
        /// Setup the authentication before to build the authorization response 
        /// </summary>
        /// <param name="response">the client response which can hold an authentication header need to build our response</param>
        public void SetupAuthentication( RTSPMessageResponse response )
        {
            lock ( _lock )
            {
                _factory.SetupAuthentication( response?.Headers.FindByName( RTSPHeaderNames.WWWAuthenticate ) );
            }
        }

        /// <summary>
        /// Add an authorization header
        /// </summary>
        /// <param name="request">the request</param>
        /// <returns>returns for a success, otherwise false</returns>
        public bool ConfigureAuthorization( RTSPMessageRequest request )
        {
            if ( request == null || request.Method == RTSPMethodType.UnDefined )
            {
                return false;
            }

            lock ( _lock )
            {
                if ( !_factory.IsAuthenticationSetup() )
                {
                    return false;
                }

                var credentials = _proxy.Credentials ?? RTSPCredentials.Empty;
                var uri         = new RTSPUri( _proxy.Uri );

                uri.ClearCredentials();

                if ( _factory.CanCreateBasicAuthorization() )
                {
                    return request.Headers.AddOrUpdate( _factory.CreateBasicAuthorization( credentials ) );
                }

                if ( _factory.CanCreateDigestAuthorization() )
                {
                    if ( _factory.CanCreateDigestMD5Authorization() )
                    {
                        return request.Headers.AddOrUpdate( _factory.CreateDigestMD5Authorization( credentials , request.Method , uri.ToString() ) );
                    }

                    if ( _factory.CanCreateDigestSHA256Authorization() )
                    {
                        return request.Headers.AddOrUpdate( _factory.CreateDigestSHA256Authorization( credentials , request.Method , uri.ToString() ) );
                    }

                    if ( _factory.CanCreateDigestSHA512Authorization() )
                    {
                        return request.Headers.AddOrUpdate( _factory.CreateDigestSHA512Authorization( credentials , request.Method , uri.ToString() ) );
                    }

                    return request.Headers.AddOrUpdate( _factory.CreateDigestAuthorization( credentials , request.Method , uri.ToString() ) );
                }

                return false;
            }
        }
    }
}
