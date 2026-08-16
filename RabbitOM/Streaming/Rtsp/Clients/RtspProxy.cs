using System;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    internal sealed class RtspProxy : IDisposable
    {
        public event EventHandler<RtspConnectionOpenedEventArgs> Opened;
        public event EventHandler<RtspConnectionClosedEventArgs> Closed;
        public event EventHandler<RtspMessageSendedEventArgs> MessageSended;
        public event EventHandler<RtspMessageReceivedEventArgs> MessageReceived;
        public event EventHandler<RtspPacketReceivedEventArgs> DataReceived;
        public event EventHandler<RtspConnectionErrorEventArgs> Error;
        public event EventHandler<RtspAuthenticationFailedEventArgs> AuthenticationFailed;



        private readonly object _lock;
        private readonly RtspTcpSocket _socket;
        private readonly RtspProxyInformations _informations;
        private readonly RtspProxyRequestManager _requestManager;
        private readonly RtspProxySecurityManager _securityManager;
        private readonly RtspProxyInvocationManager _invocationManager;
        private readonly RtspProxyEventManager _eventManager;
        private readonly RtspProxyEventManager _mediaEventManager;
        private readonly RtspProxyStatus _status;
        private readonly RtspSettings _settings;



        public RtspProxy()
        {
            _lock = new object();
            _socket = new RtspTcpSocket();
            _informations = new RtspProxyInformations();
            _requestManager = new RtspProxyRequestManager( this );
            _securityManager = new RtspProxySecurityManager( this );
            _invocationManager = new RtspProxyInvocationManager( this );
            _eventManager = new RtspProxyEventManager( this );
            _mediaEventManager = new RtspProxyEventManager( this );
            _status = new RtspProxyStatus();
            _settings = new RtspSettings();
        }



        public object SyncRoot
        {
            get => _lock;
        }

        public RtspProxyRequestManager RequestManager
        {
            get => _requestManager;
        }

        public RtspProxySecurityManager SecurityManager
        {
            get => _securityManager;
        }

        public RtspProxyInvocationManager InvocationManager
        {
            get => _invocationManager;
        }

        public RtspProxyEventManager EventManager
        {
            get => _eventManager;
        }

        public RtspProxyEventManager MediaEventManager
        {
            get => _mediaEventManager;
        }

        public string SessionId
        {
            get => _informations.SessionId;
        }

        public bool IsOpened
        {
            get => _socket.IsOpened;
        }

        public bool IsConnected
        {
            get => _status.State;
        }

        public string Uri
        {
            get => _settings.Uri;
        }

        public TimeSpan ReceiveTimeout
        {
            get => _settings.ReceiveTimeout;
        }

        public TimeSpan SendTimeout
        {
            get => _settings.SendTimeout;
        }

        public string UserName
        {
            get => _settings.UserName;
        }

        public string Password
        {
            get => _settings.Password;
        }



        public void Open( string uri )
        {
            Open( uri , string.Empty , string.Empty );
        }

        public void Open( string uri , string userName , string password )
        {
            if ( ! RtspUri.TryParse( uri , out RtspUri rtspUri ) )
            {
                throw new ArgumentException( nameof( uri ) );
            }

            try
            {
                lock ( _lock )
                {
                    if ( _socket.IsOpened )
                    {
                        throw new InvalidOperationException( "the socket is already opened" );
                    }

                    _settings.Uri = uri;
                    _settings.UserName = userName;
                    _settings.Password = password;

                    _socket.Connect( rtspUri.Host , rtspUri.Port );
                    _socket.EnableLingerState( TimeSpan.FromSeconds( 5 ) );

                    OnInitialized();
                }

                OnOpened( new RtspConnectionOpenedEventArgs() );
            }
            catch ( Exception ex )
            {
                OnReleased();

                OnError( new RtspConnectionErrorEventArgs( ex ) );

                throw;
            }
        }

        public void Close()
        {
            bool isOpened = false;

            lock ( _lock )
            {
                isOpened = _socket.IsOpened;

                OnReleasing();

                _socket.Close();

                OnReleased();
            }

            if ( isOpened )
            {
                OnClosed( new RtspConnectionClosedEventArgs() );
            }
        }

        public void Abort()
        {
            try
            {
                bool isOpened = _socket.IsOpened;

                _socket.Close();

                _requestManager.CancelPendingRequests();

                if ( isOpened )
                {
                    OnClosed(new RtspConnectionClosedEventArgs());
                }
            }
            catch ( Exception ex )
            {
                OnError( new RtspConnectionErrorEventArgs( ex ) );
            }
            finally
            {
                OnReleased();
            }
        }

        public void Dispose()
        {
            Close();

            _requestManager.Dispose();
            _socket.Dispose();
        }

        public void ConfigureReceiveTimeout(TimeSpan timeout)
        {
            _socket.ReceiveTimeout = timeout;
            _settings.ReceiveTimeout = timeout;
        }

        public void ConfigureSendTimeout( TimeSpan timeout )
        {
            _socket.SendTimeout = timeout;
        }

        public bool Send( string text )
        {
            return Send( RtspDataConverter.ConvertToBytesUTF8( text ) );
        }

        public bool Send( byte[] buffer )
        {
            if ( buffer == null )
            {
                return false;
            }

            try
            {
                return _socket.Send( buffer , 0 , buffer.Length ) > 0;
            }
            catch ( Exception ex )
            {
                OnError( new RtspConnectionErrorEventArgs( ex ) );
            }

            return false;
        }

        public int Receive( byte[] buffer , int offset , int count )
        {
            try
            {
                return _socket.Receive( buffer , offset , count );
            }
            catch ( Exception ex)
            {
                OnError( new RtspConnectionErrorEventArgs( ex ) );
            }

            return 0;
        }

        public bool TryOpen( string uri )
        {
            return TryOpen( uri , string.Empty , string.Empty );
        }

        public bool TryOpen( string uri , string userName , string password )
        {
            if ( ! RtspUri.TryParse( uri , out RtspUri rtspUri ) )
            {
                return false;
            }

            try
            {
                lock ( _lock )
                {
                    if ( _socket.IsOpened )
                    {
                        return false;
                    }

                    _settings.Uri = uri;
                    _settings.UserName = userName;
                    _settings.Password = password;

                    _socket.Connect( rtspUri.Host , rtspUri.Port );
                    _socket.EnableLingerState( TimeSpan.FromSeconds( 5 ) );

                    OnInitialized();
                }

                OnOpened( new RtspConnectionOpenedEventArgs() );

                return true;
            }
            catch ( Exception ex )
            {
                OnReleased();

                OnError( new RtspConnectionErrorEventArgs( ex ) );
            }

            return false;
        }

        public bool TryConfigureReceiveTimeout( TimeSpan timeout )
        {
            try
            {
                _socket.ReceiveTimeout = timeout;
                _settings.ReceiveTimeout = timeout;
                return true;
            }
            catch ( Exception ex )
            {
                OnError( new RtspConnectionErrorEventArgs( ex ) );
            }

            return false;
        }

        public bool TryConfigureSendTimeout( TimeSpan timeout )
        {
            try
            {
                _socket.SendTimeout = timeout;
                return true;
            }
            catch ( Exception ex )
            {
                OnError( new RtspConnectionErrorEventArgs( ex ) );
            }

            return false;
        }

        public int GetNextSequenceId()
        {
            return _informations.GetNextSequenceIdentifier();
        }

        public bool WaitForConnected( TimeSpan timeout )
        {
            return _status.WaitActivation( timeout );
        }



        public static void RaiseEvent( RtspProxy proxy , EventArgs e )
        {
            if ( proxy == null )
            {
                throw new ArgumentNullException( nameof( proxy ) );
            }

            switch ( e )
            {
                case RtspPacketReceivedEventArgs eventArgs:
                    proxy.OnDataReceived( eventArgs );
                    break;

                case RtspConnectionOpenedEventArgs eventArgs:
                    proxy.OnOpened( eventArgs );
                    break;

                case RtspConnectionClosedEventArgs eventArgs:
                    proxy.OnClosed( eventArgs );
                    break;

                case RtspMessageSendedEventArgs eventArgs:
                    proxy.OnMessageSended( eventArgs );
                    break;

                case RtspMessageReceivedEventArgs eventArgs:
                    proxy.OnMessageReceived( eventArgs );
                    break;

                case RtspAuthenticationFailedEventArgs eventArgs:
                    proxy.OnAuthenticationFailed( eventArgs );
                    break;

                case RtspConnectionErrorEventArgs eventArgs:
                    proxy.OnError( eventArgs );
                    break;
            }
        }



        private void OnOpened( RtspConnectionOpenedEventArgs e )
        {
            _status.TurnOn();

            Opened?.TryInvoke( this, e );
        }

        private void OnClosed( RtspConnectionClosedEventArgs e )
        {
            Closed?.TryInvoke( this , e );
        }

        private void OnMessageSended( RtspMessageSendedEventArgs e )
        {
            _status.KeepStatusActive();

            MessageSended?.TryInvoke( this , e );
        }

        private void OnMessageReceived( RtspMessageReceivedEventArgs e )
        {
            _status.KeepStatusActive();

            MessageReceived?.TryInvoke( this , e );
        }

        private void OnDataReceived(RtspPacketReceivedEventArgs e )
        {
            DataReceived?.TryInvoke( this , e );
        }

        private void OnAuthenticationFailed( RtspAuthenticationFailedEventArgs e )
        {
            AuthenticationFailed?.TryInvoke( this , e );
        }

        private void OnError( RtspConnectionErrorEventArgs e )
        {
            _status.IncreaseErrors();

            Error?.TryInvoke( this , e );
        }

        private void OnInitialized()
        {
            _informations.ResetAll();
            _securityManager.Initialize();

            _eventManager.Start();
            _mediaEventManager.Start();
            _requestManager.Start();

            _status.Initialize();
        }

        private void OnReleasing()
        {
            _requestManager.Stop();
        }

        private void OnReleased()
        {
            _requestManager.Stop();
            _mediaEventManager.Stop();
            _eventManager.Stop();

            _status.TurnOff();

            _informations.ResetAll();
        }
    }
}
