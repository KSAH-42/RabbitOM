using System;

namespace RabbitOM.Net.Rtsp.Remoting
{
    /// <summary>
    /// Represent the internal proxy class
    /// </summary>
    internal sealed class RTSPProxy : IDisposable
    {
        /// <summary>
        /// Raised when the connection has been opened
        /// </summary>
        public event EventHandler<RTSPConnectionOpenedEventArgs>                Opened                = null;

        /// <summary>
        /// Raised when the connection has been closed
        /// </summary>
        public event EventHandler<RTSPConnectionClosedEventArgs>                Closed                = null;

        /// <summary>
        /// Raised when a message has been sended
        /// </summary>
        public event EventHandler<RTSPConnectionMessageSendedEventArgs>         MessageSended         = null;

        /// <summary>
        /// Raised when a message has been received
        /// </summary>
        public event EventHandler<RTSPConnectionMessageReceivedEventArgs>       MessageReceived       = null;

        /// <summary>
        /// Raised the data has been received
        /// </summary>
        public event EventHandler<RTSPPacketReceivedEventArgs>                  DataReceived          = null;

        /// <summary>
        /// Raised when an error occurs
        /// </summary>
        public event EventHandler<RTSPConnectionErrorEventArgs>                 Error                 = null;

        /// <summary>
        /// Raised when the authentication has failed
        /// </summary>
        public event EventHandler<RTSPConnectionAuthenticationFailedEventArgs>  AuthenticationFailed  = null;







        private readonly object                   _lock                  = null;

        private readonly RTSPSocketTcp            _socket                = null;

        private readonly RTSPProxyInformations    _informations          = null;

        private readonly RTSPProxyRequestManager  _requestManager        = null;

        private readonly RTSPProxySecurityManager _securityManager       = null;

        private readonly RTSPProxyInvocationManager  _invokeManager      = null;
        
        private readonly RTSPEventQueue           _eventQueue            = null;

        private readonly RTSPEventQueue           _mediaEventQueue       = null;

        private readonly RTSPThread               _eventListener         = null;

        private readonly RTSPThread               _mediaEventListener    = null;

        private readonly RTSPProxyStatus          _status                = null;

        private readonly RTSPProxySettings        _settings              = null;




        /// <summary>
        /// Constructor
        /// </summary>
        public RTSPProxy()
        {
            _lock = new object();
            _socket = new RTSPSocketTcp( e => OnError( new RTSPConnectionErrorEventArgs( e ) ) );
            _informations = new RTSPProxyInformations();
            _requestManager = new RTSPProxyRequestManager( this);
			_securityManager = new RTSPProxySecurityManager(this);
            _invokeManager = new RTSPProxyInvocationManager(this);
            _eventQueue = new RTSPEventQueue();
            _mediaEventQueue = new RTSPEventQueue();
            _eventListener = new RTSPThread( "RTSP - Proxy Event listener" );
            _mediaEventListener = new RTSPThread( "RTSP - Proxy Media event listener" );
            _status = new RTSPProxyStatus();
            _settings = new RTSPProxySettings();
        }






        /// <summary>
        /// Gets the sync root
        /// </summary>
        public object SyncRoot
        {
            get => _lock;
        }

        /// <summary>
        /// Gets the uri
        /// </summary>
        public string Uri
        {
            get => _settings.Uri;
        }

        /// <summary>
        /// Gets the credentials
        /// </summary>
        public RTSPCredentials Credentials
        {
            get => _settings.Credentials;
        }

        /// <summary>
        /// Gets the receive timeout
        /// </summary>
        public TimeSpan ReceiveTimeout
        {
            get => _settings.ReceiveTimeout;
        }

        /// <summary>
        /// Gets the send timeout
        /// </summary>
        public TimeSpan SendTimeout
        {
            get => _settings.SendTimeout;
        }

        /// <summary>
        /// Gets the request manager
        /// </summary>
        public RTSPProxyRequestManager RequestManager
        {
            get => _requestManager;
        }

        /// <summary>
        /// Gets the security manager
        /// </summary>
        public RTSPProxySecurityManager SecurityManager
        {
            get => _securityManager;
        }

        /// <summary>
        /// Gets the invocation manager
        /// </summary>
        public RTSPProxyInvocationManager InvokeManager
		{
            get => _invokeManager;
		}

        /// <summary>
        /// Gets the session identifier
        /// </summary>
        public string SessionId
		{
            get => _informations.SessionId;
		}

        /// <summary>
        /// Check the connection is opened
        /// </summary>
        public bool IsOpened
        {
            get => _socket.IsOpened;
        }

        /// <summary>
        /// Check the connection status
        /// </summary>
        public bool IsConnected
        {
            get => _socket.IsConnected;
        }

        /// <summary>
        /// Gets the status activation
        /// </summary>
        public bool IsActive
        {
            get => _status.IsActive;
        }






        /// <summary>
        /// Open the connection
        /// </summary>
        /// <param name="uri">the uri</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="Exception"/>
        public void Open(string uri)
        {
            Open(uri, RTSPCredentials.Empty);
        }

        /// <summary>
        /// Open the connection
        /// </summary>
        /// <param name="uri">the uri</param>
        /// <param name="credentials">the credentials</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="Exception"/>
        public void Open(string uri, RTSPCredentials credentials)
        {
            // TODO: Code refactoring here

            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            if (credentials == null)
            {
                throw new ArgumentNullException(nameof(credentials));
            }

            if (string.IsNullOrWhiteSpace(uri))
            {
                throw new ArgumentException(nameof(uri));
            }

            if (TryOpen(uri, credentials))
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
            return TryOpen( uri , RTSPCredentials.Empty );
        }

        /// <summary>
        /// Open the connection
        /// </summary>
        /// <param name="uri">the uri</param>
        /// <param name="credentials">the credentials</param>
        /// <returns>returns true for a success otherwise false</returns>
        public bool TryOpen( string uri , RTSPCredentials credentials )
        {
            if ( string.IsNullOrWhiteSpace( uri ) || credentials == null )
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
                    _settings.Credentials = credentials;

                    RTSPUri rtspUri = new RTSPUri( uri );

                    using ( var scope = new RTSPDisposeScope( () => _socket.Close() ) )
                    {
                        if ( !_socket.Open( rtspUri.Host , rtspUri.Port ) )
                        {
                            return false;
                        }

                        _socket.SetLingerState( true , TimeSpan.FromSeconds( 5 ) );

                        scope.Clear();
                    }

                    Initialize();
                }

                OnOpened( new RTSPConnectionOpenedEventArgs() );

                return true;
            }
            catch ( Exception ex )
            {
                UnInitialize();

                OnError( new RTSPConnectionErrorEventArgs( ex ) );
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

                    UnInitialize();
                }

                if ( isOpened )
                {
                    OnClosed( new RTSPConnectionClosedEventArgs() );
                }
            }
            catch ( Exception ex )
            {
                UnInitialize();

                OnError( new RTSPConnectionErrorEventArgs( ex ) );
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
                    OnClosed(new RTSPConnectionClosedEventArgs());
                }

                UnInitialize();
            }
            catch (Exception ex)
            {
                UnInitialize();

                OnError(new RTSPConnectionErrorEventArgs(ex));
            }
        }

        /// <summary>
        /// Close internal resource
        /// </summary>
        public void Dispose()
        {
            Close();
            GC.SuppressFinalize( this );
        }

        /// <summary>
        /// Configure the timeout
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool ConfigureTimeouts( TimeSpan timeout )
        {
            return ConfigureTimeouts( timeout , timeout );
        }

        /// <summary>
        /// Configure the timeout
        /// </summary>
        /// <param name="receiveTimeout">the receive timeout</param>
        /// <param name="sendTimeout">the send timeout</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool ConfigureTimeouts( TimeSpan receiveTimeout , TimeSpan sendTimeout )
        {
            try
            {
                lock ( _lock )
                {
                    bool succeed = false;

                    if ( _socket.SetReceiveTimeout( receiveTimeout ) )
                    {
                        _settings.ReceiveTimeout = receiveTimeout;

                        succeed = true;
                    }

                    if ( _socket.SetSendTimeout( sendTimeout ) )
                    {
                        _settings.SendTimeout = sendTimeout;

                        succeed = true;
                    }

                    return succeed;
                }
            }
            catch ( Exception ex )
            {
                OnError( new RTSPConnectionErrorEventArgs( ex ) );
            }

            return false;
        }

        /// <summary>
        /// Send data
        /// </summary>
        /// <param name="text">the text</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Send( string text )
        {
            try
            {
                return _socket.Send( text );
            }
            catch ( Exception ex )
            {
                OnError( new RTSPConnectionErrorEventArgs( ex ) );
            }

            return false;
        }

        /// <summary>
        /// Send data
        /// </summary>
        /// <param name="buffer">the text</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Send( byte[] buffer )
        {
            try
            {
                return _socket.Send( buffer );
            }
            catch ( Exception ex )
            {
                OnError( new RTSPConnectionErrorEventArgs( ex ) );
            }

            return false;
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
            try
            {
                return _socket.Send( buffer , offset , count );
            }
            catch ( Exception ex )
            {
                OnError( new RTSPConnectionErrorEventArgs( ex ) );
            }

            return false;
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
            try
            {
                return _socket.Receive( buffer , offset , count );
            }
            catch ( Exception ex )
            {
                OnError( new RTSPConnectionErrorEventArgs( ex ) );
            }

            return -1;
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
        public bool WaitForConnection( TimeSpan timeout )
        {
            return _status.WaitActivation( timeout );
        }

        /// <summary>
        /// Dispatch an event
        /// </summary>
        /// <param name="e">the event args</param>
        public void DispatchEvent( RTSPConnectionOpenedEventArgs e )
        {
            _eventQueue.Enqueue( e );
        }

        /// <summary>
        /// Dispatch an event
        /// </summary>
        /// <param name="e">the event args</param>
        public void DispatchEvent( RTSPConnectionClosedEventArgs e )
        {
            _eventQueue.Enqueue( e );
        }

        /// <summary>
        /// Dispatch an event
        /// </summary>
        /// <param name="e">the event args</param>
        public void DispatchEvent( RTSPConnectionErrorEventArgs e )
        {
            _eventQueue.Enqueue( e );
        }

        /// <summary>
        /// Dispatch an event
        /// </summary>
        /// <param name="e">the event args</param>
        public void DispatchEvent( RTSPConnectionAuthenticationFailedEventArgs e )
        {
            _eventQueue.Enqueue( e );
        }

        /// <summary>
        /// Dispatch an event
        /// </summary>
        /// <param name="e">the event args</param>
        public void DispatchEvent( RTSPConnectionMessageSendedEventArgs e )
        {
            _eventQueue.Enqueue( e );
        }

        /// <summary>
        /// Dispatch an event
        /// </summary>
        /// <param name="e">the event args</param>
        public void DispatchEvent( RTSPConnectionMessageReceivedEventArgs e )
        {
            _eventQueue.Enqueue( e );
        }
        
        /// <summary>
        /// Dispatch an event
        /// </summary>
        /// <param name="e">the event args</param>
        public void DispatchEvent(RTSPPacketReceivedEventArgs e )
        {
            _mediaEventQueue.Enqueue(e);
        }

        /// <summary>
        /// Initialize
        /// </summary>
        private void Initialize()
        {
            _informations.ResetAll();
            _securityManager.Initialize();


            _eventListener.Start( () =>
            {
                while ( WaitEvents() )
                {
                    HandleEvents();
                }

                HandleEvents();
            } );

            _mediaEventListener.Start( () =>
            {
                while (WaitMediaEvents())
                {
                    HandleMediaEvents();
                }
            } );

            _requestManager.Start();

            _status.Activate();
        }

        /// <summary>
        /// Uninitialize
        /// </summary>
        private void UnInitialize()
        {
            _requestManager.Stop();
            _mediaEventListener.Stop();
            _eventListener.Stop();

            _informations.ResetAll();
            _status.Deactivate();
            _eventQueue.Clear();
            _mediaEventQueue.Clear();
        }

        /// <summary>
        /// Wait media events
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        private bool WaitEvents()
        {
            return _eventQueue.Wait( _eventListener.ExitHandle );
        }

        /// <summary>
        /// Handle the events
        /// </summary>
        private void HandleEvents()
        {
            while ( _eventQueue.Any() )
            {
                if ( _eventQueue.Dequeue( out EventArgs eventArgs ) )
                {
                    OnDispatchEvent( eventArgs );
                }
            }
        }

        /// <summary>
        /// Wait media events
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        private bool WaitMediaEvents()
        {
            return _mediaEventQueue.Wait( _mediaEventListener.ExitHandle );
        }

        /// <summary>
        /// Handle media events
        /// </summary>
        private void HandleMediaEvents()
        {
            while (_mediaEventQueue.Dequeue(out EventArgs eventArgs))
            {
                OnDispatchEvent(eventArgs);
            }
        }






        /// <summary>
        /// Occurs when the connection has been opened
        /// </summary>
        /// <param name="e">the event args</param>
        private void OnOpened( RTSPConnectionOpenedEventArgs e )
        {
            RTSPEventInvoker.RaiseEvent<RTSPConnectionOpenedEventArgs>( this , e , Opened );
        }

        /// <summary>
        /// Occurs when the connection has been closed
        /// </summary>
        /// <param name="e">the event args</param>
        private void OnClosed( RTSPConnectionClosedEventArgs e )
        {
            RTSPEventInvoker.RaiseEvent<RTSPConnectionClosedEventArgs>( this , e , Closed );
        }

        /// <summary>
        /// Occurs when a message has been sended
        /// </summary>
        /// <param name="e"></param>
        private void OnMessageSended( RTSPConnectionMessageSendedEventArgs e )
        {
            _status.KeepStatusActive();

            RTSPEventInvoker.RaiseEvent<RTSPConnectionMessageSendedEventArgs>( this , e , MessageSended );
        }

        /// <summary>
        /// Occurs when a message has been received
        /// </summary>
        /// <param name="e">the event args</param>
        private void OnMessageReceived( RTSPConnectionMessageReceivedEventArgs e )
        {
            _status.KeepStatusActive();

            RTSPEventInvoker.RaiseEvent<RTSPConnectionMessageReceivedEventArgs>( this , e , MessageReceived );
        }

        /// <summary>
        /// Occurs when the data has been received
        /// </summary>
        /// <param name="e"></param>
        private void OnDataReceived(RTSPPacketReceivedEventArgs e )
        {
            RTSPEventInvoker.RaiseEvent<RTSPPacketReceivedEventArgs>( this , e , DataReceived );
        }

        /// <summary>
        /// Occurs when the authentication has failed
        /// </summary>
        /// <param name="e">the event args</param>
        private void OnAuthenticationFailed( RTSPConnectionAuthenticationFailedEventArgs e )
        {
            RTSPEventInvoker.RaiseEvent<RTSPConnectionAuthenticationFailedEventArgs>( this , e , AuthenticationFailed );
        }

        /// <summary>
        /// Occurs when an error has been detected
        /// </summary>
        /// <param name="e">the event args</param>
        private void OnError( RTSPConnectionErrorEventArgs e )
        {
            _status.IncreaseErrors();

            RTSPEventInvoker.RaiseEvent<RTSPConnectionErrorEventArgs>( this , e , Error );
        }

        /// <summary>
        /// Process the event
        /// </summary>
        /// <param name="e">the event args</param>
        private void OnDispatchEvent( EventArgs e )
        {
            switch ( e )
            {
                case RTSPPacketReceivedEventArgs eventArgs:
                    OnDataReceived( eventArgs );
                    break;

                case RTSPConnectionOpenedEventArgs eventArgs:
                    OnOpened( eventArgs );
                    break;

                case RTSPConnectionClosedEventArgs eventArgs:
                    OnClosed( eventArgs );
                    break;

                case RTSPConnectionMessageSendedEventArgs eventArgs:
                    OnMessageSended( eventArgs );
                    break;

                case RTSPConnectionMessageReceivedEventArgs eventArgs:
                    OnMessageReceived( eventArgs );
                    break;

                case RTSPConnectionAuthenticationFailedEventArgs eventArgs:
                    OnAuthenticationFailed( eventArgs );
                    break;

                case RTSPConnectionErrorEventArgs eventArgs:
                    OnError( eventArgs );
                    break;
            }
        }
    }
}
