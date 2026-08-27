using System;
using System.Windows;
using System.Windows.Controls;

namespace RabbitOM.Player.Controls
{
    public partial class MediaControl : UserControl
    {
        public MediaControl()
        {
            InitializeComponent();
        }






        public static readonly RoutedEvent CommunicationStartedEvent =
            EventManager.RegisterRoutedEvent(
                nameof(CommunicationStarted),
                    RoutingStrategy.Bubble,
                        typeof(RoutedEventHandler),
                            typeof(MediaControl));


        public static readonly RoutedEvent CommunicationStoppedEvent =
            EventManager.RegisterRoutedEvent(
                nameof(CommunicationStopped),
                    RoutingStrategy.Bubble,
                        typeof(RoutedEventHandler),
                            typeof(MediaControl));

        public static readonly RoutedEvent ConnectedEvent =
            EventManager.RegisterRoutedEvent(
                nameof(Connected),
                    RoutingStrategy.Bubble,
                        typeof(RoutedEventHandler),
                            typeof(MediaControl));

        public static readonly RoutedEvent DisconnectedEvent =
            EventManager.RegisterRoutedEvent(
                nameof(Disconnected),
                    RoutingStrategy.Bubble,
                        typeof(RoutedEventHandler),
                            typeof(MediaControl));








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







        public static readonly DependencyProperty UriProperty =
            DependencyProperty.Register(
                nameof(Uri),
                    typeof(string),
                        typeof(MediaControl));

        public static readonly DependencyProperty UserNameProperty =
            DependencyProperty.Register(
                nameof(UserName),
                    typeof(string),
                        typeof(MediaControl));

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register(
                nameof(Password),
                    typeof(string),
                    typeof(MediaControl));

        public static readonly DependencyProperty TransportProperty =
            DependencyProperty.Register(
                nameof(Transport),
                    typeof(MediaPlayerTransport),
                        typeof(MediaControl));

        public static readonly DependencyProperty StatusInfoProperty =
            DependencyProperty.Register(
                nameof(StatusInfo),
                    typeof(string),
                    typeof(MediaControl));

        public static readonly DependencyProperty FooterProperty =
            DependencyProperty.Register(
                nameof(Footer),
                    typeof(string),
                    typeof(MediaControl));

        public static readonly DependencyProperty FooterVisibilityProperty =
            DependencyProperty.Register(
                nameof(FooterVisibility),
                    typeof(Visibility),
                        typeof(MediaControl),
                            new PropertyMetadata(Visibility.Collapsed));






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

        public string StatusInfo
        {
            get => GetValue( StatusInfoProperty ) as string;
            set => SetValue( StatusInfoProperty , value );
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

        public NetworkStatistics Statistics
        {
            get => _statistics;
        }






        protected virtual void OnCommunicationStarted( RoutedEventArgs e )
        {
            RaiseEvent( e );
        }

        protected virtual void OnCommunicationStopped( RoutedEventArgs e )
        {
            RaiseEvent( e );
        }

        protected virtual void OnConnected( RoutedEventArgs e )
        {
            RaiseEvent( e );
        }

        protected virtual void OnDisconnected( RoutedEventArgs e )
        {
            RaiseEvent( e );
        }

        protected virtual void OnFrameReceived( RoutedEventArgs e )
        {
            RaiseEvent( e );
        }
    }
}
