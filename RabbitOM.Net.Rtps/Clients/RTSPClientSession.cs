using System;

namespace RabbitOM.Net.Rtsp.Clients
{
    using RabbitOM.Net.Rtsp.Remoting;
    using RabbitOM.Net.Rtsp.Remoting.Invokers;

    /// <summary>
    /// Represent a rtsp session
    /// </summary>
    public class RTSPClientSession
    {
        private readonly object                                            _lock                   = null;

        private readonly RTSPClientConfiguration                           _configuration          = null;

        private readonly RTSPClientConfigurationOptions                    _options                = null;

        private readonly RTSPConnection                                    _connection             = null;

        private readonly RTSPClientSessionInfos                            _informations           = null;

        private readonly RTSPClientSessionDispatcher                       _dispatcher             = null;

        private readonly RTSPEventWaitHandle                               _abortHandle            = null;

        private RTSPClientSessionTransport                                 _transport              = null;
        



        



        /// <summary>
        /// Constructor
        /// </summary>
        public RTSPClientSession()
        {
            _lock            = new object();
            _configuration   = new RTSPClientConfiguration();
            _options         = new RTSPClientConfigurationOptions();
            _connection      = new RTSPConnection();
            _informations    = new RTSPClientSessionInfos();
            _dispatcher      = new RTSPClientSessionDispatcher();
            _abortHandle     = new RTSPEventWaitHandle();
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
        public RTSPClientConfiguration Configuration
        {
            get => _configuration;
        }

        /// <summary>
        /// Gets the transport configuration options
        /// </summary>
        public RTSPClientConfigurationOptions Options
        {
            get => _options;
        }

        /// <summary>
        /// Gets the event dispatcher
        /// </summary>
        public RTSPClientSessionDispatcher Dispatcher
        {
            get => _dispatcher;
        }

        /// <summary>
        /// Gets the supported methods
        /// </summary>
        public RTSPMethodTypeReadonlyList SupportedMethods
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
        /// Get abort handle
        /// </summary>
        public RTSPEventWaitHandle AbortHandle
        {
            get => _abortHandle;
        }









        /// <summary>
        /// Open a connection
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Connect()
        {
            try
            {
                if ( _connection.IsOpened )
                {
                    return false;
                }

                _informations.Reset();

                if ( ! _connection.TryOpen( _configuration.Uri , new RTSPCredentials( _configuration.UserName , _configuration.Password ) ) )
                {
                    throw new RTSPClientException( RTSPClientErrorCode.ConnectionFailed , "Connection failed" );
                }

                if ( ! _connection.ConfigureTimeouts( _configuration.ReceiveTimeout , _configuration.SendTimeout ) )
                {
                    throw new RTSPClientException( RTSPClientErrorCode.ConnectionFailed , "Failed to configure the timeout" );
                }

                RTSPInvokerResult optionsResult = _connection.GetOptions().Invoke();

                if ( optionsResult == null || ! optionsResult.Succeed )
                {
                    throw new RTSPClientException( RTSPClientErrorCode.GetOptionsFailed , "Failed to invoke the options method" );
                }

                RTSPInvokerResult describeResult = _connection.Describe().Invoke();       

                if ( describeResult == null || ! describeResult.Succeed )
                {
                    throw new RTSPClientException( RTSPClientErrorCode.DescribeFailed , "Failed to invoke the describe method" );
                }

                if ( ! _informations.Descriptor.Extract( describeResult.Response.GetBody() ) )
                {
                    throw new RTSPClientException( RTSPClientErrorCode.DescribeFailed , "Failed to extract / parse the sdp" );
                }

                if ( ! _informations.Descriptor.SelectTrack( _options.MediaFormat ) )
                {
                    throw new RTSPClientException( RTSPClientErrorCode.DescribeFailed , "Failed to select a media track" );
                }

                RTSPInvokerResult setupResult = null;
            
                switch ( _options.DeliveryMode )
                {
                    case RTSPDeliveryMode.Tcp:

                        setupResult = _connection.Setup()
                            .As<RTSPSetupInvoker>().SetDeliveryMode( RTSPDeliveryMode.Tcp )
                            .As<RTSPSetupInvoker>().SetTrackUri( _informations.Descriptor.SelectedTrack.ControlUri )
                            .Invoke();

                        break;

                    case RTSPDeliveryMode.Udp:

                        _transport = new RTSPClientSessionTransportUdpAsync( _options.UnicastPort , _configuration.ReceiveTimeout );
                        _transport.SetSession( this );

                        setupResult = _connection.Setup()
                            .As<RTSPSetupInvoker>().SetDeliveryMode( RTSPDeliveryMode.Udp )
                            .As<RTSPSetupInvoker>().SetTrackUri( _informations.Descriptor.SelectedTrack.ControlUri )
                            .As<RTSPSetupInvoker>().SetUnicastPort( _options.UnicastPort )
                            .Invoke();

                        break;

                    case RTSPDeliveryMode.Multicast:

                        _transport = new RTSPClientSessionTransportMulticast( _options.MulticastAddress , _options.MulticastPort , _options.MulticastTTL , _configuration.ReceiveTimeout );
                        _transport.SetSession( this );

                        setupResult = _connection.Setup()
                            .As<RTSPSetupInvoker>().SetDeliveryMode( RTSPDeliveryMode.Multicast )
                            .As<RTSPSetupInvoker>().SetTrackUri( _informations.Descriptor.SelectedTrack.ControlUri )
                            .As<RTSPSetupInvoker>().SetMulticastAddress( _options.MulticastAddress )
                            .As<RTSPSetupInvoker>().SetMulticastPort( _options.MulticastPort  )
                            .As<RTSPSetupInvoker>().SetMulticastTTL( _options.MulticastTTL  )
                            .Invoke();

                        break;

                    default:
                        throw new RTSPClientException( RTSPClientErrorCode.SetupFailed , "the delivery mode is not supported" );
                }
            
                if ( setupResult == null || ! setupResult.Succeed )
                {
                   throw new RTSPClientException( RTSPClientErrorCode.SetupFailed , "Failed to setup the transport" );
                }

                if ( ! _informations.RegisterSessionId( setupResult.Response.GetHeaderSessionId() ) )
                {
                    throw new RTSPClientException( RTSPClientErrorCode.SetupFailed , "Failed to setup the transport due to invalid session identifier" );
                }

                // Trigger the event connected before to publish DataReceived event
                _dispatcher.DispatchEvent( new RTSPClientConnectedEventArgs( _informations.Descriptor.SelectedTrack ) );

                if ( _transport != null )
                {
                    _transport.Start();
                }

                RTSPInvokerResult playResult = _connection.Play().As<RTSPPlayInvoker>().SetSessionId( _informations.SessionId ).Invoke();

                if ( playResult == null || ! playResult.Succeed )
                {
                    _connection.TearDown().As<RTSPTearDownInvoker>().SetSessionId( _informations.SessionId ).Invoke();
                    _transport?.Stop();
                    _dispatcher.DispatchEvent( new RTSPClientDisconnectedEventArgs() );

                    throw new RTSPClientException( RTSPClientErrorCode.PlayFailed , "Failed to invoke the play method" );
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
		public void Disconnect()
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
                    _connection.TearDown().As<RTSPTearDownInvoker>().SetSessionId(_informations.SessionId).Invoke();
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
                    _dispatcher.DispatchEvent(new RTSPClientDisconnectedEventArgs());
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

                if (_connection.IsOpened)
                {
                    _dispatcher.DispatchEvent(new RTSPClientDisconnectedEventArgs());
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
        /// Call the ping method
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Ping()
        {
            try
            {
                RTSPInvokerResult result = null;

                if ( _informations.IsSessionIdRegistered() )
                {
                    result = _connection.KeepAlive( _configuration.KeepAliveType ).As<RTSPKeepAliveInvoker>().SetSessionId( _informations.SessionId ).Invoke();
                }
                else
                {
                    result = _connection.GetOptions().Invoke();
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
            _connection.DataReceived += OnDataReceived;
        }

        /// <summary>
        /// Un subscribe events
        /// </summary>
        public void UnSusbcribeEvents()
        {
            _connection.DataReceived -= OnDataReceived;
        }
        



        

        /// <summary>
        /// Occurs when a packet has been received
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">the event args</param>
        protected virtual void OnDataReceived(object sender, RTSPPacketReceivedEventArgs e )
		{
            _dispatcher.DispatchEvent( e );
		}


        /// <summary>
        /// Occurs when some session exception has been raised
        /// </summary>
        /// <param name="ex">the exception</param>
        protected virtual void OnException( Exception ex )
        {
            if ( ex == null )
            {
                return;
            }

            if ( ex is RTSPClientException )
            {
                var exception = ex as RTSPClientException;

                _dispatcher.DispatchEvent( new RTSPClientErrorEventArgs( exception.ErrorCode , exception.Message ) );
            }
            
            System.Diagnostics.Debug.WriteLine( ex );
        }
    }
}
