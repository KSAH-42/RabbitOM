using System;

namespace RabbitOM.Streaming.Net.Rtsp.Clients.Connections
{
    /// <summary>
    /// Represent the client security manager. This is class is used to manage authentication procedures
    /// </summary>
    internal sealed class RtspProxySecurityManager
    {
        private readonly object _lock;

        private readonly RtspProxy _proxy;

        private readonly RtspAuthorizationFactory _factory;





        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="proxy">the proxy</param>
        /// <exception cref="ArgumentNullException"/>
        public RtspProxySecurityManager( RtspProxy proxy )
        {
            _proxy   = proxy ?? throw new ArgumentNullException( nameof( proxy ) ) ;

            _lock    = new object();
            _factory = new RtspAuthorizationFactory();
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
        public void SetupAuthentication( RtspMessageResponse response )
        {
            lock ( _lock )
            {
                _factory.SetupAuthentication( response?.Headers.FindByName( RtspHeaderNames.WWWAuthenticate ) );
            }
        }

        /// <summary>
        /// Add an authorization header
        /// </summary>
        /// <param name="request">the request</param>
        /// <returns>returns for a success, otherwise false</returns>
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
