using System;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    internal sealed class RtspSession : IDisposable
    {
        private readonly object _lock;

        private readonly RtspClientConfiguration _configuration;

        private readonly RtspConnection _connection;

        private readonly RtspSessionInfos _informations;

        private readonly RtspSessionEventDispatcher _dispatcher;

        private RtspClientSessionDataReceiver _dataReceiver;




        internal RtspSession( object sender )
        {
            _lock = new object();

            _configuration = new RtspClientConfiguration();
            _connection = new RtspConnection();
            _informations = new RtspSessionInfos();
            _dispatcher = new RtspSessionEventDispatcher( sender );
        }





        public object SyncRoot
        {
            get => _lock;
        }

        public RtspClientConfiguration Configuration
        {
            get => _configuration;
        }

        public RtspSessionEventDispatcher Dispatcher
        {
            get => _dispatcher;
        }

        public RtspMethodReadonlyList SupportedMethods
        {
            get => _informations.SupportedMethods;
        }

        public string SessionId
        {
            get => _informations.SessionId;
        }

        public bool IsReady
        {
            get => _informations.IsReady;
        }

        public bool IsPlaying
        {
            get => _informations.IsPlaying;
        }

        public bool CanPrepare
        {
            get => _informations.CanPrepare();
        }

        public bool CanSetup
        {
            get => _informations.CanSetup();
        }

        public bool CanPlay
        {
            get => _informations.CanPlay();
        }

        public bool CanTearDown
        {
            get => _informations.CanTearDown();
        }

        public bool IsOpened
        {
            get => _connection.IsOpened;
        }

        public bool IsConnected
        {
            get => _connection.IsConnected;
        }



        public bool Open()
        {
            try
            {
                if ( _connection.IsOpened )
                {
                    return false;
                }

                _informations.Reset();

                if ( ! _connection.TryOpen( _configuration.Uri , _configuration.UserName , _configuration.Password ) )
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

                        _dataReceiver = new RtspClientSessionDataReceiver( this , new RtspUdpDataReceiver( _configuration.RtpPort , _configuration.ReceiveTimeout ) );

                        setupResult = _connection.Setup()
                            .As<RtspSetupInvoker>().SetDeliveryMode( RtspDeliveryMode.Udp )
                            .As<RtspSetupInvoker>().SetTrackUri( _informations.Descriptor.SelectedTrack.ControlUri )
                            .As<RtspSetupInvoker>().SetUnicastPort( _configuration.RtpPort )
                            .Invoke();

                        break;

                    case RtspDeliveryMode.Multicast:

                        _dataReceiver = new RtspClientSessionDataReceiver( this , new RtspMulticastDataReceiver( _configuration.MulticastAddress , _configuration.RtpPort , _configuration.TimeToLive , _configuration.ReceiveTimeout ) );

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

                if ( _dataReceiver != null )
                {
                    _dataReceiver.Start();
                }

                RtspInvokerResult playResult = _connection.Play().As<RtspPlayInvoker>().SetSessionId( _informations.SessionId ).Invoke();

                if ( playResult == null || ! playResult.Succeed )
                {
                    _connection.TearDown().As<RtspTearDownInvoker>().SetSessionId( _informations.SessionId ).Invoke();
                    _dataReceiver?.Stop();
                    _dispatcher.DispatchEvent( new RtspClientDisconnectedEventArgs() );

                    throw new RtspClientException( RtspClientErrorCode.PlayFailed , "Failed to invoke the play method" );
                }
                
                _informations.TurnOnPlayingStatus();

                return true;
            }
            catch ( Exception ex )
            {
                _connection.Close();
                _dataReceiver?.Stop();

                OnException( ex );
            }

            return false;
        }

        public void Close()
        {
            _dataReceiver?.Stop();
            _dataReceiver = null;

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

        public void Abort()
        {
            _dataReceiver?.Stop();
            _dataReceiver = null;

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

        public bool WaitForOnline( TimeSpan timeout )
        {
            return _connection.WaitForConnected( timeout );
        }

        public void Dispose()
        {
            Close();
            _connection.Dispose();
        }

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
                    throw new RtspClientException( RtspClientErrorCode.PingFailed , "Failed to ping the session" );
                }

                return true;
            }
            catch ( Exception ex )
            {
                OnException( ex );
            }

            return false;
        }

        public void SubscribeEvents()
        {
            _connection.PacketReceived += OnDataReceived;
        }

        public void UnSusbcribeEvents()
        {
            _connection.PacketReceived -= OnDataReceived;
        }

        
        
        
        private void OnDataReceived(object sender, RtspPacketReceivedEventArgs e )
        {
            _dispatcher.DispatchEvent( e );
        }

        private void OnException( Exception ex )
        {
            System.Diagnostics.Debug.WriteLine( ex );

            if ( ex is RtspClientException exception )
            {
                _dispatcher.DispatchEvent( new RtspClientErrorEventArgs( exception.ErrorCode , exception.Message ) );
            }
        }
    }
}
