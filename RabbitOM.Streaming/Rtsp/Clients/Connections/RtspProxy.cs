using System;

namespace RabbitOM.Streaming.Rtsp.Clients.Connections
{
    /// <summary>
    /// Represent the internal proxy class
    /// </summary>
    internal sealed class RtspProxy : IDisposable
    {
        /// <summary>
        /// Raised when the connection has been opened
        /// </summary>
        public event EventHandler<RtspConnectionOpenedEventArgs>      Opened;

        /// <summary>
        /// Raised when the connection has been closed
        /// </summary>
        public event EventHandler<RtspConnectionClosedEventArgs>      Closed;

        /// <summary>
        /// Raised when a message has been sended
        /// </summary>
        public event EventHandler<RtspMessageSendedEventArgs>         MessageSended;

        /// <summary>
        /// Raised when a message has been received
        /// </summary>
        public event EventHandler<RtspMessageReceivedEventArgs>       MessageReceived;

        /// <summary>
        /// Raised the data has been received
        /// </summary>
        public event EventHandler<RtspPacketReceivedEventArgs>        DataReceived;

        /// <summary>
        /// Raised when an error occurs
        /// </summary>
        public event EventHandler<RtspConnectionErrorEventArgs>       Error;

        /// <summary>
        /// Raised when the authentication has failed
        /// </summary>
        public event EventHandler<RtspAuthenticationFailedEventArgs>  AuthenticationFailed;







        private readonly object _lock;

        private readonly RtspTcpSocket _socket;

        private readonly RtspProxyInformations _informations;

        private readonly RtspProxyRequestManager _requestManager;

        private readonly RtspProxySecurityManager _securityManager;

        private readonly RtspProxyInvocationManager _invokeManager;
        
        private readonly RtspProxyEventManager _eventManager;

        private readonly RtspProxyEventManager _mediaEventManager;

        private readonly RtspProxyStatus _status;

        private RtspCredentials _credentials;

        private string _uri;

        private bool _isDisposed;







        /// <summary>
        /// Constructor
        /// </summary>
        public RtspProxy()
        {
            _lock = new object();
            
            _socket            = new RtspTcpSocket( e => OnError( new RtspConnectionErrorEventArgs( e ) ) );
            _informations      = new RtspProxyInformations();
            _requestManager    = new RtspProxyRequestManager( this );
            _securityManager   = new RtspProxySecurityManager( this );
            _invokeManager     = new RtspProxyInvocationManager( this );
            _eventManager      = new RtspProxyEventManager( this );
            _mediaEventManager = new RtspProxyEventManager( this );
            _status            = new RtspProxyStatus();
        }







        /// <summary>
        /// Gets the sync root
        /// </summary>
        public object SyncRoot
        {
            get => _lock;
        }

        /// <summary>
        /// Gets the request manager
        /// </summary>
        public RtspProxyRequestManager RequestManager
        {
            get => _requestManager;
        }

        /// <summary>
        /// Gets the security manager
        /// </summary>
        public RtspProxySecurityManager SecurityManager
        {
            get => _securityManager;
        }

        /// <summary>
        /// Gets the invocation manager
        /// </summary>
        public RtspProxyInvocationManager InvocationManager
        {
            get => _invokeManager;
        }

        /// <summary>
        /// Gets the event manager
        /// </summary>
        public RtspProxyEventManager EventManager
        {
            get => _eventManager;
        }

        /// <summary>
        /// Gets the media event manager
        /// </summary>
        public RtspProxyEventManager MediaEventManager
        {
            get => _mediaEventManager;
        }

        /// <summary>
        /// Gets the session identifier
        /// </summary>
        public string SessionId
        {
            get => _informations.SessionId;
        }

        /// <summary>
        /// Gets the current state
        /// </summary>
        public bool IsOnline
        {
            get => _status.IsOnline;
        }

        /// <summary>
        /// <summary>
        /// Check the connection is opened
        /// </summary>
        public bool IsOpened
        {
            get => _socket.IsOpened;
        }

        /// <summary>
        /// Check the socket connection status
        /// </summary>
        public bool IsConnected
        {
            get => _socket.IsConnected;
        }

        /// <summary>
        /// Gets the receive timeout
        /// </summary>
        public TimeSpan ReceiveTimeout
        {
            get => _socket.GetReceiveTimeout();
        }

        /// <summary>
        /// Gets the send timeout
        /// </summary>
        public TimeSpan SendTimeout
        {
            get => _socket.GetSendTimeout();
        }

        /// <summary>
        /// Gets the uri
        /// </summary>
        public string Uri
        {
            get
            {
                lock ( _lock )
                {
                    return _uri ?? string.Empty;
                }
            }
        }

        /// <summary>
        /// Gets the credentials
        /// </summary>
        public RtspCredentials Credentials
        {
            get
            {
                lock ( _lock )
                {
                    return _credentials ?? RtspCredentials.Empty;
                }
            }
        }

        /// <summary>
        /// Check if the underlaying connection has been disposed
        /// </summary>
        public bool IsDisposed
        {
            get
            {
                lock ( _lock )
                {
                    return _isDisposed;
                }
            }

            private set
            {
                lock ( _lock )
                {
                    _isDisposed = value;
                }
            }
        }







        /// <summary>
        /// Open the connection
        /// </summary>
        /// <param name="uri">the uri</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ObjectDisposedException"/>
        public void Open( string uri )
        {
            Open( uri , RtspCredentials.Empty );
        }

        /// <summary>
        /// Open the connection
        /// </summary>
        /// <param name="uri">the uri</param>
        /// <param name="credentials">the credentials</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ObjectDisposedException"/>
        public void Open( string uri , RtspCredentials credentials )
        {
            EnsureNotDispose();

            if ( ! TryOpen( uri ?? throw new ArgumentNullException( nameof( uri ) ) , credentials ?? throw new ArgumentNullException( nameof( credentials ) ) ) )
            {
                throw new Exception("Open failure");
            }
        }

        /// <summary>
        /// Open the connection
        /// </summary>
        /// <param name="uri">the uri</param>
        /// <returns>returns true for a success otherwise false</returns>
        public bool TryOpen( string uri )
        {
            return TryOpen( uri , RtspCredentials.Empty );
        }

        /// <summary>
        /// Open the connection
        /// </summary>
        /// <param name="uri">the uri</param>
        /// <param name="credentials">the credentials</param>
        /// <returns>returns true for a success otherwise false</returns>
        public bool TryOpen( string uri , RtspCredentials credentials )
        {
            if ( string.IsNullOrWhiteSpace( uri ) || credentials == null )
            {
                return false;
            }

            if ( IsDisposed )
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

                    _uri = uri;
                    _credentials = credentials;

                    var rtspUri = RtspUri.Create( uri );

                    using ( var scope = new RtspDisposeScope( () => _socket.Close() ) )
                    {
                        if ( ! _socket.Open( rtspUri.Host , rtspUri.Port ) )
                        {
                            return false;
                        }
                        
                        _socket.SetLingerState( true , TimeSpan.FromSeconds( 5 ) );

                        scope.ClearActions();
                    }

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

        /// <summary>
        /// Close
        /// </summary>
        public void Close()
        {
            try
            {
                bool isOpened = false;

                lock ( _lock )
                {
                    isOpened = _socket.IsOpened;

                    _socket.Shutdown();
                    _socket.Close();

                    OnReleased();
                }

                if ( isOpened )
                {
                    OnClosed( new RtspConnectionClosedEventArgs() );
                }
            }
            catch ( Exception ex )
            {
                OnReleased();

                OnError( new RtspConnectionErrorEventArgs( ex ) );
            }
        }

        /// <summary>
        /// Abort
        /// </summary>
        public void Abort()
        {
            try
            {
                bool isOpened = _socket.IsOpened;

                _socket.Shutdown();
                _socket.Close();

                _requestManager.CancelPendingRequests();

                if ( isOpened )
                {
                    OnClosed(new RtspConnectionClosedEventArgs());
                }
            }
            catch (Exception ex)
            {
                OnError(new RtspConnectionErrorEventArgs(ex));
            }
            finally
            {
                OnReleased();
            }
        }

        /// <summary>
        /// Close internal resource
        /// </summary>
        public void Dispose()
        {
            Close();
            
            _requestManager.Dispose();
            _socket.Dispose();

            IsDisposed = true;
        }

        /// <summary>
        /// Throw an exception if the current instance is already disposed
        /// </summary>
        public void EnsureNotDispose()
        {
            lock ( _lock )
            {
                if ( _isDisposed )
                {
                    throw new ObjectDisposedException( "Rtsp Proxy" );
                }
            }
        }

        /// <summary>
        /// Configure the receive timeout
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <exception cref="ObjectDisposedException"/>
        public void ConfigureReceiveTimeout(TimeSpan timeout)
        {
            lock ( _lock )
            {
                if ( ! _socket.SetReceiveTimeout( timeout ) )
                {
                    throw new InvalidOperationException();
                }
            }
        }

        /// <summary>
        /// Configure the send timeout
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <exception cref="ObjectDisposedException"/>
        public void ConfigureSendTimeout( TimeSpan timeout )
        {
            lock ( _lock )
            {
                if ( ! _socket.SetSendTimeout( timeout ) )
                {
                    throw new InvalidOperationException();
                }
            }
        }

        /// <summary>
        /// Configure the receive timeout
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryConfigureReceiveTimeout( TimeSpan timeout )
        {
            lock ( _lock )
            {
                return _socket.SetReceiveTimeout( timeout );
            }
        }

        /// <summary>
        /// Configure the send timeout
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryConfigureSendTimeout( TimeSpan timeout )
        {
            lock ( _lock )
            {
                return _socket.SetSendTimeout( timeout );
            }
        }

        /// <summary>
        /// Send data
        /// </summary>
        /// <param name="text">the text</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Send( string text )
        {
            return _socket.Send( text );
        }

        /// <summary>
        /// Send data
        /// </summary>
        /// <param name="buffer">the text</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Send( byte[] buffer )
        {
            return _socket.Send( buffer );
        }

        /// <summary>
        /// Send data
        /// </summary>
        /// <param name="buffer">the buffer</param>
        /// <param name="offset">the offset</param>
        /// <param name="count">the count</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Send( byte[] buffer , int offset , int count )
        {
            return _socket.Send( buffer , offset , count );
        }

        /// <summary>
        /// Receive data
        /// </summary>
        /// <param name="buffer">the buffer</param>
        /// <param name="offset">the offset</param>
        /// <param name="count">the count</param>
        /// <returns>returns the number of bytes read</returns>
        public int Receive( byte[] buffer , int offset , int count )
        {
            return _socket.Receive(buffer, offset, count);
        }

        /// <summary>
        /// Gets the next sequence identifier
        /// </summary>
        /// <returns>returns a value</returns>
        public int GetNextSequenceId()
        {
            return _informations.GetNextSequenceIdentifier();
        }
        
        /// <summary>
        /// Wait the connection succeed
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool WaitForOnline( TimeSpan timeout )
        {
            return _status.WaitActivation( timeout );
        }

        /// <summary>
        /// Raise the event
        /// </summary>
        /// <param name="e">the event args</param>
        public void RaiseEvent( EventArgs e )
        {
            switch ( e )
            {
                case RtspPacketReceivedEventArgs eventArgs:
                    OnDataReceived( eventArgs );
                    break;

                case RtspConnectionOpenedEventArgs eventArgs:
                    OnOpened( eventArgs );
                    break;

                case RtspConnectionClosedEventArgs eventArgs:
                    OnClosed( eventArgs );
                    break;

                case RtspMessageSendedEventArgs eventArgs:
                    OnMessageSended( eventArgs );
                    break;

                case RtspMessageReceivedEventArgs eventArgs:
                    OnMessageReceived( eventArgs );
                    break;

                case RtspAuthenticationFailedEventArgs eventArgs:
                    OnAuthenticationFailed( eventArgs );
                    break;

                case RtspConnectionErrorEventArgs eventArgs:
                    OnError( eventArgs );
                    break;
            }
        }








        /// <summary>
        /// Occurs when the connection has been opened
        /// </summary>
        /// <param name="e">the event args</param>
        private void OnOpened( RtspConnectionOpenedEventArgs e )
        {
            _status.TurnOn();

            Opened?.TryInvoke( this, e );
        }

        /// <summary>
        /// Occurs when the connection has been closed
        /// </summary>
        /// <param name="e">the event args</param>
        private void OnClosed( RtspConnectionClosedEventArgs e )
        {
            Closed?.TryInvoke( this , e );
        }

        /// <summary>
        /// Occurs when a message has been sended
        /// </summary>
        /// <param name="e">the event args</param>
        private void OnMessageSended( RtspMessageSendedEventArgs e )
        {
            _status.KeepStatusActive();

            MessageSended?.TryInvoke( this , e );
        }

        /// <summary>
        /// Occurs when a message has been received
        /// </summary>
        /// <param name="e">the event args</param>
        private void OnMessageReceived( RtspMessageReceivedEventArgs e )
        {
            _status.KeepStatusActive();

            MessageReceived?.TryInvoke( this , e );
        }

        /// <summary>
        /// Occurs when the data has been received
        /// </summary>
        /// <param name="e">the event args</param>
        private void OnDataReceived(RtspPacketReceivedEventArgs e )
        {
            DataReceived?.TryInvoke( this , e );
        }

        /// <summary>
        /// Occurs when the authentication has failed
        /// </summary>
        /// <param name="e">the event args</param>
        private void OnAuthenticationFailed( RtspAuthenticationFailedEventArgs e )
        {
            AuthenticationFailed?.TryInvoke( this , e );
        }

        /// <summary>
        /// Occurs when an error has been detected
        /// </summary>
        /// <param name="e">the event args</param>
        private void OnError( RtspConnectionErrorEventArgs e )
        {
            _status.IncreaseErrors();

            Error?.TryInvoke( this , e );
        }

        /// <summary>
        /// Init internal resources
        /// </summary>
        private void OnInitialized()
        {
            _informations.ResetAll();
            _securityManager.Initialize();

            _eventManager.Start();
            _mediaEventManager.Start();
            _requestManager.Start();

            _status.Initialize();
        }

        /// <summary>
        /// Release internal resources
        /// </summary>
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
