using System;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    using RabbitOM.Streaming.Rtsp.Clients.Connections;
    using RabbitOM.Streaming.Rtsp.Clients.Connections.Invokers;

    /// <summary>
    /// Represent a Rtsp session
    /// </summary>
    internal sealed class RtspClientSession : IDisposable
    {
        private readonly object                                            _lock                   = null;

        private readonly RtspClientConfiguration                           _configuration          = null;

        private readonly RtspConnection                                    _connection             = null;

        private readonly RtspClientSessionInfos                            _informations           = null;

        private readonly RtspClientSessionDispatcher                       _dispatcher             = null;

        private RtspClientSessionTransport                                 _transport              = null;
        



        



        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sender">the sender</param>
        internal RtspClientSession( object sender )
        {
            _lock            = new object();
            _configuration   = new RtspClientConfiguration();
            _connection      = new RtspConnection();
            _informations    = new RtspClientSessionInfos();
            _dispatcher      = new RtspClientSessionDispatcher( sender );
        }
        



        



        /// <summary>
        /// Gets the sync root
        /// </summary>
        public object SyncRoot
        {
            get => _lock;
        }

        /// <summary>
        /// Gets the configuration
        /// </summary>
        public RtspClientConfiguration Configuration
        {
            get => _configuration;
        }

        /// <summary>
        /// Gets the event dispatcher
        /// </summary>
        public RtspClientSessionDispatcher Dispatcher
        {
            get => _dispatcher;
        }

        /// <summary>
        /// Gets the supported methods
        /// </summary>
        public RtspMethodReadonlyList SupportedMethods
        {
            get => _informations.SupportedMethods;
        }

        /// <summary>
        /// Gets the current session identifier
        /// </summary>
        public string SessionId
        {
            get => _informations.SessionId;
        }

        /// <summary>
        /// Check if the session has been setup
        /// </summary>
        public bool IsReady
        {
            get => _informations.IsReady;
        }

        /// <summary>
        /// Check if the session is actually playing
        /// </summary>
        public bool IsPlaying
        {
            get => _informations.IsPlaying;
        }

        /// <summary>
        /// Check if prepare method can be called
        /// </summary>
        public bool CanPrepare
        {
            get => _informations.CanPrepare();
        }

        /// <summary>
        /// Check if the setup method can be called
        /// </summary>
        public bool CanSetup
        {
            get => _informations.CanSetup();
        }

        /// <summary>
        /// Check if the play method can be called
        /// </summary>
        public bool CanPlay
        {
            get => _informations.CanPlay();
        }

        /// <summary>
        /// Check if the teardown method can be called
        /// </summary>
        public bool CanTearDown
        {
            get => _informations.CanTearDown();
        }

        /// <summary>
        /// Check if the underlaying connection has been opened
        /// </summary>
        public bool IsOpened
        {
            get => _connection.IsOpened;
        }

        /// <summary>
        /// Check if the connection is still active
        /// </summary>
        public bool IsConnected
        {
            get => _connection.IsConnected;
        }









        /// <summary>
        /// Open a connection
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Open()
        {
            try
            {
                if ( _connection.IsOpened )
                {
                    return false;
                }

                _informations.Reset();

                if ( ! _connection.TryOpen( _configuration.Uri , new RtspCredentials( _configuration.UserName , _configuration.Password ) ) )
                {
                    throw new RtspClientException( RtspClientErrorCode.ConnectionFailed , "Connection failed" );
                }

                if ( ! _connection.TryConfigureReceiveTimeout( _configuration.ReceiveTimeout ) )
                {
                    throw new RtspClientException( RtspClientErrorCode.ConnectionFailed , "Failed to configure the timeout" );
                }

                if ( ! _connection.TryConfigureSendTimeout( _configuration.SendTimeout ) )
                {
                    throw new RtspClientException( RtspClientErrorCode.ConnectionFailed , "Failed to configure the timeout" );
                }

                RtspInvokerResult optionsResult = _connection.Options().Invoke();

                if ( optionsResult == null || ! optionsResult.Succeed )
                {
                    throw new RtspClientException( RtspClientErrorCode.GetOptionsFailed , "Failed to invoke the options method" );
                }

                RtspInvokerResult describeResult = _connection.Describe().Invoke();       

                if ( describeResult == null || ! describeResult.Succeed )
                {
                    throw new RtspClientException( RtspClientErrorCode.DescribeFailed , "Failed to invoke the describe method" );
                }

                if ( ! _informations.Descriptor.Extract( describeResult.Response.GetBody() ) )
                {
                    throw new RtspClientException( RtspClientErrorCode.DescribeFailed , "Failed to extract / parse the sdp" );
                }

                if ( ! _informations.Descriptor.SelectTrack( _configuration.MediaFormat ) )
                {
                    throw new RtspClientException( RtspClientErrorCode.DescribeFailed , "Failed to select a media track" );
                }

                RtspInvokerResult setupResult = null;
            
                switch ( _configuration.DeliveryMode )
                {
                    case RtspDeliveryMode.Tcp:

                        setupResult = _connection.Setup()
                            .As<RtspSetupInvoker>().SetDeliveryMode( RtspDeliveryMode.Tcp )
                            .As<RtspSetupInvoker>().SetTrackUri( _informations.Descriptor.SelectedTrack.ControlUri )
                            .Invoke();

                        break;

                    case RtspDeliveryMode.Udp:

                        _transport = new RtspClientSessionUdpTransport( _configuration.RtpPort , _configuration.ReceiveTimeout );
                        _transport.SetSession( this );

                        setupResult = _connection.Setup()
                            .As<RtspSetupInvoker>().SetDeliveryMode( RtspDeliveryMode.Udp )
                            .As<RtspSetupInvoker>().SetTrackUri( _informations.Descriptor.SelectedTrack.ControlUri )
                            .As<RtspSetupInvoker>().SetUnicastPort( _configuration.RtpPort )
                            .Invoke();

                        break;

                    case RtspDeliveryMode.Multicast:

                        _transport = new RtspClientSessionMulticastTransport( _configuration.MulticastAddress , _configuration.RtpPort , _configuration.TimeToLive , _configuration.ReceiveTimeout );
                        _transport.SetSession( this );

                        setupResult = _connection.Setup()
                            .As<RtspSetupInvoker>().SetDeliveryMode( RtspDeliveryMode.Multicast )
                            .As<RtspSetupInvoker>().SetTrackUri( _informations.Descriptor.SelectedTrack.ControlUri )
                            .As<RtspSetupInvoker>().SetMulticastAddress( _configuration.MulticastAddress )
                            .As<RtspSetupInvoker>().SetMulticastPort( _configuration.RtpPort  )
                            .As<RtspSetupInvoker>().SetMulticastTTL( _configuration.TimeToLive  )
                            .Invoke();

                        break;

                    default:
                        throw new RtspClientException( RtspClientErrorCode.SetupFailed , "the delivery mode is not supported" );
                }
            
                if ( setupResult == null || ! setupResult.Succeed )
                {
                   throw new RtspClientException( RtspClientErrorCode.SetupFailed , "Failed to setup the transport" );
                }

                if ( ! _informations.RegisterSessionId( setupResult.Response.GetHeaderSessionId() ) )
                {
                    throw new RtspClientException( RtspClientErrorCode.SetupFailed , "Failed to setup the transport due to invalid session identifier" );
                }

                // Trigger the event connected before to publish DataReceived event
                _dispatcher.DispatchEvent( new RtspClientConnectedEventArgs( _informations.Descriptor.SelectedTrack ) );

                if ( _transport != null )
                {
                    _transport.Start();
                }

                RtspInvokerResult playResult = _connection.Play().As<RtspPlayInvoker>().SetSessionId( _informations.SessionId ).Invoke();

                if ( playResult == null || ! playResult.Succeed )
                {
                    _connection.TearDown().As<RtspTearDownInvoker>().SetSessionId( _informations.SessionId ).Invoke();
                    _transport?.Stop();
                    _dispatcher.DispatchEvent( new RtspClientDisconnectedEventArgs() );

                    throw new RtspClientException( RtspClientErrorCode.PlayFailed , "Failed to invoke the play method" );
                }
                
                _informations.TurnOnPlayingStatus();

                return true;
            }
            catch ( Exception ex )
            {
                _connection.Close();
                _transport?.Stop();

                OnException( ex );
            }

            return false;
        }

        /// <summary>
        /// Close the underlaying connection
        /// </summary>
        public void Close()
        {
            try
            {
                if (_transport != null)
                {
                    _transport.Stop();
                }
            }
            catch (Exception ex)
            {
                OnException(ex);
            }

            _transport = null;

            try
            {
                if ( _informations.IsSessionIdRegistered())
                {
                    _connection.TearDown().As<RtspTearDownInvoker>().SetSessionId(_informations.SessionId).Invoke();
                }
            }
            catch (Exception ex)
            {
                OnException(ex);
            }

            try
            {
                if (_connection.IsOpened)
                {
                    _connection.Close();
                    
                   _dispatcher.DispatchEvent(new RtspClientDisconnectedEventArgs());
                }
            }
            catch (Exception ex)
            {
                OnException(ex);
            }

            _informations.Reset();
        }

        /// <summary>
        /// Abort the underlaying connection 
        /// </summary>
        public void Abort()
        {
            try
            {
                if (_transport != null)
                {
                    _transport.Stop();
                }
            }
            catch (Exception ex)
            {
                OnException(ex);
            }

            _transport = null;

            try
            {
                // This method is called on different thread see the StartCommunication(TimeSpan timeout) method
                // This is a fix to prevent event ordering issue when the Disconnect event is fired before the raised of CommunicationStopped event
                // Please handle this case when the client classed must be enterily refactored
                // I am convinced that the client class must totaly refactored not the connection class !
                // The client must provided other event handler and provide the parse of rtp packet and deliver statistics infos

                if (_connection.IsConnected)
                {
                    _dispatcher.DispatchEvent(new RtspClientDisconnectedEventArgs());
                }

                _connection.Abort();
            }
            catch (Exception ex)
            {
                OnException(ex);
            }

            _informations.Reset();
        }

        /// <summary>
        /// Just wait for a connection succeed
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <returns>return true for a success, otherwise false</returns>
        public bool WaitForConnection( TimeSpan timeout )
        {
            return _connection.WaitForOnline( timeout );
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Close();
            _connection.Dispose();
        }

        /// <summary>
        /// Call the ping method
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Ping()
        {
            try
            {
                RtspInvokerResult result = null;

                if ( _informations.IsSessionIdRegistered() )
                {
                    result = _connection.KeepAlive( _configuration.KeepAliveType ).As<RtspKeepAliveInvoker>().SetSessionId( _informations.SessionId ).Invoke();
                }
                else
                {
                    result = _connection.Options().Invoke();
                }

                if ( result == null || ! result.Succeed )
                {
                    throw new Exception( "Failed to ping the session" );
                }

                return true;
            }
            catch ( Exception ex )
            {
                OnException( ex );
            }

            return false;
        }

        /// <summary>
        /// Subscribe to events
        /// </summary>
        public void SubscribeEvents()
        {
            _connection.PacketReceived += OnDataReceived;
        }

        /// <summary>
        /// Un subscribe events
        /// </summary>
        public void UnSusbcribeEvents()
        {
            _connection.PacketReceived -= OnDataReceived;
        }
        



        

        /// <summary>
        /// Occurs when a packet has been received
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">the event args</param>
        private void OnDataReceived(object sender, RtspPacketReceivedEventArgs e )
        {
            _dispatcher.DispatchEvent( e );
        }


        /// <summary>
        /// Occurs when some session exception has been raised
        /// </summary>
        /// <param name="ex">the exception</param>
        private void OnException( Exception ex )
        {
            if ( ex == null )
            {
                return;
            }

            if ( ex is RtspClientException )
            {
                var exception = ex as RtspClientException;

                _dispatcher.DispatchEvent( new RtspClientErrorEventArgs( exception.ErrorCode , exception.Message ) );
            }
            
            System.Diagnostics.Debug.WriteLine( ex );
        }
    }
}
