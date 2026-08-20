using System;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    public sealed class RtspConnection : IRtspConnection
    {
        private readonly RtspConnector _proxy = new RtspConnector();

        public event EventHandler<RtspConnectionOpenedEventArgs> Opened
        {
            add    => _proxy.Opened += value;
            remove => _proxy.Opened -= value;
        }

        public event EventHandler<RtspConnectionClosedEventArgs> Closed
        {
            add    => _proxy.Closed += value;
            remove => _proxy.Closed -= value;
        }

        public event EventHandler<RtspMessageSendedEventArgs> MessageSended
        {
            add    => _proxy.MessageSended += value;
            remove => _proxy.MessageSended -= value;
        }

        public event EventHandler<RtspMessageReceivedEventArgs> MessageReceived
        {
            add    => _proxy.MessageReceived += value;
            remove => _proxy.MessageReceived -= value;
        }

        public event EventHandler<RtspPacketReceivedEventArgs> PacketReceived
        {
            add    => _proxy.DataReceived += value;
            remove => _proxy.DataReceived -= value;
        }

        public event EventHandler<RtspAuthenticationFailedEventArgs> AuthenticationFailed
        {
            add    => _proxy.AuthenticationFailed += value;
            remove => _proxy.AuthenticationFailed -= value;
        }

        public event EventHandler<RtspConnectionErrorEventArgs> Error
        {
            add    => _proxy.Error += value;
            remove => _proxy.Error -= value;
        }



        public object SyncRoot
        {
            get => _proxy.SyncRoot;
        }

        public string Uri
        {
            get => _proxy.Uri;
        }

        public string UserName
        {
            get => _proxy.UserName;
        }

        public string Password
        {
            get => _proxy.Password;
        }

        public bool IsConnected
        {
            get => _proxy.IsConnected;
        }

        public bool IsOpened
        {
            get => _proxy.IsOpened;
        }




        public void Open(string uri)
        {
            _proxy.Open( uri );
        }

        public void Open(string uri, string userName , string password )
        {
            _proxy.Open( uri , userName , password );
        }

        public bool TryOpen( string uri )
        {
            return _proxy.TryOpen( uri );
        }

        public bool TryOpen( string uri , string userName , string password )
        {
            return _proxy.TryOpen( uri , userName , password );
        }

        public void Close()
        {
            _proxy.Close();
        }

        public void Abort()
        {
            _proxy.Abort();
        }

        public void ConfigureReceiveTimeout( TimeSpan timeout )
        {
            _proxy.ConfigureReceiveTimeout( timeout );
        }

        public void ConfigureSendTimeout( TimeSpan timeout )
        {
            _proxy.ConfigureSendTimeout( timeout );
        }

        public bool TryConfigureReceiveTimeout( TimeSpan timeout )
        {
            return _proxy.TryConfigureReceiveTimeout( timeout );
        }

        public bool TryConfigureSendTimeout( TimeSpan timeout )
        {
            return _proxy.TryConfigureSendTimeout( timeout );
        }

        public int GetNextSequenceId()
        {
            return _proxy.GetNextSequenceId();
        }

        public bool SendRequest( RtspMessageRequest request , out RtspMessageResponse response )
        {
            return _proxy.RequestManager.TrySendRequest( request , out response );
        }

        public bool WaitForConnected( TimeSpan timeout )
        {
            return _proxy.WaitForConnected( timeout );
        }

        public IRtspInvoker Options()
        {
            return _proxy.InvocationManager.CreateOptionsInvoker();
        }

        public IRtspInvoker Describe()
        {
            return _proxy.InvocationManager.CreateDescribeInvoker();
        }

        public IRtspInvoker Setup()
        {
            return _proxy.InvocationManager.CreateSetupInvoker();
        }

        public IRtspInvoker Play()
        {
            return _proxy.InvocationManager.CreatePlayInvoker();
        }

        public IRtspInvoker Pause()
        {
            return _proxy.InvocationManager.CreatePauseInvoker();
        }

        public IRtspInvoker TearDown()
        {
            return _proxy.InvocationManager.CreateTearDownInvoker();
        }

        public IRtspInvoker GetParameter()
        {
            return _proxy.InvocationManager.CreateGetParameterInvoker();
        }

        public IRtspInvoker SetParameter()
        {
            return _proxy.InvocationManager.CreateSetParameterInvoker();
        }

        public IRtspInvoker Announce()
        {
            return _proxy.InvocationManager.CreateAnnounceInvoker();
        }

        public IRtspInvoker Redirect()
        {
            return _proxy.InvocationManager.CreateRedirectInvoker();
        }

        public IRtspInvoker Record()
        {
            return _proxy.InvocationManager.CreateRecordInvoker();
        }

        public IRtspInvoker KeepAlive()
        {
            return _proxy.InvocationManager.CreateKeepAliveInvoker();
        }

        public IRtspInvoker KeepAlive( RtspKeepAliveType type )
        {
            return _proxy.InvocationManager.CreateKeepAliveInvoker(type);
        }

        public void Dispose()
        {
            _proxy.Dispose();
        }
    }
}
