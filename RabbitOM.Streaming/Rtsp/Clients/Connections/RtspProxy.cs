﻿using System;

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

        private readonly RtspProxyInvocationManager _invocationManager;
        
        private readonly RtspProxyEventManager _eventManager;

        private readonly RtspProxyEventManager _mediaEventManager;

        private readonly RtspProxyStatus _status;

        private readonly RtspSettings _settings;

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
            _invocationManager = new RtspProxyInvocationManager( this );
            _eventManager      = new RtspProxyEventManager( this );
            _mediaEventManager = new RtspProxyEventManager( this );
            _status            = new RtspProxyStatus();
            _settings          = new RtspSettings();
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
            get => _invocationManager;
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
        /// <summary>
        /// Check the connection is opened
        /// </summary>
        public bool IsOpened
        {
            get => _socket.IsCreated;
        }

        /// <summary>
        /// Check the socket connection status
        /// </summary>
        public bool IsConnected
        {
            get => _status.State;
        }

        /// <summary>
        /// Gets the uri
        /// </summary>
        public string Uri
        {
            get => _settings.Uri;
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
        /// Gets the user name
        /// </summary>
        public string UserName
        {
            get => _settings.UserName;
        }

        /// <summary>
        /// Gets the user password
        /// </summary>
        public string Password
        {
            get => _settings.Password;
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
        /// <exception cref="Exception"/>
        public void Open( string uri )
        {
            Open( uri , string.Empty , string.Empty );
        }

        /// <summary>
        /// Open the connection
        /// </summary>
        /// <param name="uri">the uri</param>
        /// <param name="userName">the user name</param>
        /// <param name="password">the password</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="Exception"/>
        public void Open( string uri , string userName , string password )
        {
            EnsureNotDispose();

            if ( string.IsNullOrWhiteSpace( uri ) )
            {
                throw new ArgumentNullException( nameof( uri ) );
            }

            if ( ! TryOpen( uri , userName , password ) )
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
            return TryOpen( uri , string.Empty , string.Empty );
        }

        /// <summary>
        /// Open the connection
        /// </summary>
        /// <param name="uri">the uri</param>
        /// <param name="userName">the user name</param>
        /// <param name="password">the password</param>
        /// <returns>returns true for a success otherwise false</returns>
        public bool TryOpen( string uri , string userName , string password )
        {
            if ( string.IsNullOrWhiteSpace( uri ) )
            {
                return false;
            }

            try
            {
                lock ( _lock )
                {
                    if ( _socket.IsCreated || IsDisposed )
                    {
                        return false;
                    }

                    _settings.Uri      = uri;
                    _settings.UserName = userName;
                    _settings.Password = password;

                    if ( ! RtspUri.TryParse( _settings.Uri, out RtspUri rtspUri ) )
                    {
                        return false;
                    }

                    if ( ! _socket.Connect( rtspUri.Host , rtspUri.Port ) )
                    {
                        return false;
                    }

                    _socket.SetLingerState( true , TimeSpan.FromSeconds( 5 ) );

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
                    isOpened = _socket.IsCreated;

                    _socket.Shutdown();

                    OnReleasing();

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
                bool isOpened = _socket.IsCreated;

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
            if ( ! TryConfigureReceiveTimeout( timeout ) )
            {
                throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// Configure the send timeout
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <exception cref="ObjectDisposedException"/>
        public void ConfigureSendTimeout( TimeSpan timeout )
        {
            if ( ! TryConfigureSendTimeout( timeout ) )
            {
                throw new InvalidOperationException();
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
                if ( _socket.SetReceiveTimeout( timeout ) )
                {
                    _settings.ReceiveTimeout = timeout;

                    return true;
                }

                return false;
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
                if ( _socket.SetSendTimeout( timeout ) )
                {
                    _settings.SendTimeout = timeout;

                    return true;
                }

                return false;
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
        public bool WaitForConnected( TimeSpan timeout )
        {
            return _status.WaitActivation( timeout );
        }







        /// <summary>
        /// Raise the event
        /// </summary>
        /// <param name="proxy">the proxy</param>
        /// <param name="e">the event args</param>
        /// <exception cref="ArgumentNullException"/>
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
        /// Occurs when initialization must be done
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
        /// Occurs before releasing resources
        /// </summary>
        private void OnReleasing()
        {
            _requestManager.Stop();
        }

        /// <summary>
        /// Occurs when resources must be ended
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
