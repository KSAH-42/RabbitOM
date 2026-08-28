using System;
using System.Windows;
using System.Windows.Controls;

namespace RabbitOM.Player.Controls
{
    public partial class MediaControl : UserControl
    {
        public static readonly RoutedEvent CommunicationStartedEvent = EventManager.RegisterRoutedEvent( nameof(CommunicationStarted), RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(MediaControl) );
        public static readonly RoutedEvent CommunicationStoppedEvent = EventManager.RegisterRoutedEvent( nameof(CommunicationStopped), RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(MediaControl) );
        public static readonly RoutedEvent ConnectedEvent = EventManager.RegisterRoutedEvent( nameof(Connected) , RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(MediaControl) );
        public static readonly RoutedEvent DisconnectedEvent = EventManager.RegisterRoutedEvent( nameof(Disconnected) , RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(MediaControl) );
        public static readonly RoutedEvent FrameReceivedEvent = EventManager.RegisterRoutedEvent( nameof(FrameReceived) , RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(MediaControl) );

        public static readonly DependencyProperty UriProperty = DependencyProperty.Register( nameof(Uri) , typeof(string) , typeof(MediaControl) );
        public static readonly DependencyProperty UserNameProperty = DependencyProperty.Register( nameof(UserName) , typeof(string) , typeof(MediaControl) );
        public static readonly DependencyProperty PasswordProperty = DependencyProperty.Register( nameof(Password) , typeof(string) , typeof(MediaControl) );
        public static readonly DependencyProperty TransportProperty = DependencyProperty.Register( nameof(Transport) , typeof(MediaPlayerTransport) , typeof(MediaControl) , new PropertyMetadata( new TcpMediaPlayerTransport() ) );
        public static readonly DependencyProperty FooterProperty = DependencyProperty.Register( nameof(Footer) , typeof(string) , typeof(MediaControl) );
        public static readonly DependencyProperty FooterVisibilityProperty = DependencyProperty.Register( nameof(FooterVisibility) , typeof(Visibility) , typeof(MediaControl) , new PropertyMetadata( Visibility.Collapsed ) );
        public static readonly DependencyProperty IsCommunicationStartedProperty = DependencyProperty.Register( nameof(IsCommunicationStarted) , typeof(bool) , typeof(MediaControl) , new PropertyMetadata( false ) );
        public static readonly DependencyProperty IsConnectedProperty = DependencyProperty.Register( nameof(IsConnected) , typeof(bool) , typeof(MediaControl) , new PropertyMetadata( false ) );
        public static readonly DependencyProperty ErrorInfoProperty = DependencyProperty.Register( nameof(ErrorInfo) , typeof(string) , typeof(MediaControl) );



        public MediaControl()
        {
            InitializeComponent();
        }



        public event RoutedEventHandler CommunicationStarted
        {
            add    => AddHandler( CommunicationStartedEvent , value );
            remove => RemoveHandler( CommunicationStartedEvent , value );
        }

        public event RoutedEventHandler CommunicationStopped
        {
            add    => AddHandler( CommunicationStoppedEvent , value );
            remove => RemoveHandler( CommunicationStoppedEvent , value );
        }

        public event RoutedEventHandler Connected
        {
            add    => AddHandler( ConnectedEvent , value );
            remove => RemoveHandler( ConnectedEvent , value );
        }

        public event RoutedEventHandler Disconnected
        {
            add    => AddHandler( DisconnectedEvent , value );
            remove => RemoveHandler( DisconnectedEvent , value );
        }

        public event RoutedEventHandler FrameReceived
        {
            add    => AddHandler( FrameReceivedEvent , value );
            remove => RemoveHandler( FrameReceivedEvent , value );
        }



        public NetworkStatistics Statistics
        {
            get => _statistics;
        }

        public string Uri
        {
            get => GetValue( UriProperty ) as string;
            set => SetValue( UriProperty , value );
        }

        public string UserName
        {
            get => GetValue( UserNameProperty ) as string;
            set => SetValue( UserNameProperty , value );
        }

        public string Password
        {
            get => GetValue( PasswordProperty ) as string;
            set => SetValue( PasswordProperty , value );
        }

        public MediaPlayerTransport Transport
        {
            get => GetValue( TransportProperty ) as MediaPlayerTransport;
            set => SetValue( TransportProperty , value );
        }

        public string Footer
        {
            get => GetValue( FooterProperty ) as string;
            set => SetValue( FooterProperty , value );
        }

        public Visibility FooterVisibility
        {
            get => (Visibility) GetValue( FooterVisibilityProperty );
            set => SetValue( FooterVisibilityProperty , value );
        }

        public string ErrorInfo
        {
            get => GetValue( ErrorInfoProperty ) as string;
            private set => SetValue( ErrorInfoProperty , value );
        }

        public bool IsCommunicationStarted
        {
            get => (bool) GetValue( IsCommunicationStartedProperty );
            private set => SetValue( IsCommunicationStartedProperty , value );
        }

        public bool IsConnected
        {
            get => (bool) GetValue( IsConnectedProperty );
            private set => SetValue( IsConnectedProperty , value );
        }



        private void OnLoaded( object sender , RoutedEventArgs e )
        {
            InitializeClient();
        }

        private void OnUnloaded( object sender , RoutedEventArgs e )
        {
            UnInitializeClient();
        }



        protected virtual void OnCommunicationStarted()
        {
            IsCommunicationStarted = true;

            RaiseEvent( new RoutedEventArgs( CommunicationStartedEvent ) );
        }

        protected virtual void OnCommunicationStopped()
        {
            IsCommunicationStarted = false;

            RaiseEvent( new RoutedEventArgs( CommunicationStoppedEvent ) );
        }

        protected virtual void OnConnected()
        {
            IsConnected = true;

            RaiseEvent( new RoutedEventArgs( ConnectedEvent ) );
        }

        protected virtual void OnDisconnected()
        {
            IsConnected = false;
            Footer = "";
            ErrorInfo = "";
            _image.Source = null;

            RaiseEvent( new RoutedEventArgs( DisconnectedEvent ) );
        }

        protected virtual void OnFrameReceived()
        {
            RaiseEvent( new RoutedEventArgs( FrameReceivedEvent ) );
        }

        protected virtual void OnError( string error )
        {
            ErrorInfo = error;
        }

        protected virtual void OnException( Exception exception )
        {
            ErrorInfo = "Internal Error: " + exception?.Message;
        }
    }
}
