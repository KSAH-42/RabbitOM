using System;
using System.Windows;
using System.Windows.Controls;

namespace RabbitOM.Player.Controls
{
    public partial class MediaControl : UserControl
    {
        public static readonly RoutedEvent CommunicationStartedEvent = EventManager.RegisterRoutedEvent( "CommunicationStarted" , RoutingStrategy.Bubble , typeof(RoutedEventHandler) , typeof(MediaControl) );
        public static readonly RoutedEvent CommunicationStoppedEvent = EventManager.RegisterRoutedEvent( "CommunicationStopped" , RoutingStrategy.Bubble , typeof(RoutedEventHandler) , typeof(MediaControl) );
        public static readonly RoutedEvent ConnectedEvent = EventManager.RegisterRoutedEvent( "Connected" , RoutingStrategy.Bubble , typeof(RoutedEventHandler) , typeof(MediaControl) );
        public static readonly RoutedEvent DisconnectedEvent = EventManager.RegisterRoutedEvent( "Disconnected" , RoutingStrategy.Bubble , typeof(RoutedEventHandler) , typeof(MediaControl) );
        




        public static readonly DependencyProperty UriProperty = DependencyProperty.Register( "Uri", typeof(string) , typeof(MediaControl) );
        public static readonly DependencyProperty UserNameProperty = DependencyProperty.Register( "UserName", typeof(string) , typeof(MediaControl) );
        public static readonly DependencyProperty PasswordProperty = DependencyProperty.Register( "Password", typeof(string) , typeof(MediaControl) );
        public static readonly DependencyProperty TransportProperty = DependencyProperty.Register( "Transport", typeof(MediaPlayerTransport) , typeof(MediaControl) );
        public static readonly DependencyProperty StatusInfoProperty = DependencyProperty.Register( "StatusInfo", typeof(string) , typeof(MediaControl) );
        public static readonly DependencyProperty FooterProperty = DependencyProperty.Register( "Footer", typeof(string) , typeof(MediaControl) );
        public static readonly DependencyProperty FooterVisibilityProperty = DependencyProperty.Register( "FooterVisibility", typeof(Visibility) , typeof(MediaControl) , new PropertyMetadata( Visibility.Collapsed ) );



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
