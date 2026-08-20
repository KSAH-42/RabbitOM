using System;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    public interface IRtspConnection : IDisposable
    {
        event EventHandler<RtspConnectionOpenedEventArgs> Opened;
        event EventHandler<RtspConnectionClosedEventArgs> Closed;
        event EventHandler<RtspMessageSendedEventArgs> MessageSended;
        event EventHandler<RtspMessageReceivedEventArgs> MessageReceived;
        event EventHandler<RtspPacketReceivedEventArgs> PacketReceived;
        event EventHandler<RtspAuthenticationFailedEventArgs> AuthenticationFailed;
        event EventHandler<RtspConnectionErrorEventArgs> Error;



        object SyncRoot
        {
            get;
        }

        string Uri
        {
            get;
        }

        string UserName
        {
            get;
        }

        string Password
        {
            get;
        }

        bool IsConnected
        {
            get;
        }

        bool IsOpened
        {
            get;
        }



        void Open(string uri);

        void Open(string uri, string userName , string password );

        bool TryOpen( string uri );

        bool TryOpen( string uri , string userName , string password );

        void Close();

        void Abort();

        void ConfigureReceiveTimeout( TimeSpan timeout );

        void ConfigureSendTimeout( TimeSpan timeout );

        bool TryConfigureReceiveTimeout( TimeSpan timeout );

        bool TryConfigureSendTimeout( TimeSpan timeout );

        int GetNextSequenceId();

        bool SendRequest( RtspMessageRequest request , out RtspMessageResponse response );

        bool WaitForConnected( TimeSpan timeout );

        IRtspInvoker Options();

        IRtspInvoker Describe();

        IRtspInvoker Setup();

        IRtspInvoker Play();

        IRtspInvoker Pause();

        IRtspInvoker TearDown();

        IRtspInvoker GetParameter();

        IRtspInvoker SetParameter();

        IRtspInvoker Announce();

        IRtspInvoker Redirect();

        IRtspInvoker Record();

        IRtspInvoker KeepAlive();

        IRtspInvoker KeepAlive( RtspKeepAliveType type );
    }
}
