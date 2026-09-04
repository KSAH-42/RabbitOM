using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RabbitOM.Player.Controls
{
    public partial class MediaControl : UserControl
    {
        public static readonly RoutedEvent CommunicationStartedEvent = EventManager.RegisterRoutedEvent( nameof(CommunicationStarted), RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(MediaControl) );
        public static readonly RoutedEvent CommunicationStoppedEvent = EventManager.RegisterRoutedEvent( nameof(CommunicationStopped), RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(MediaControl) );
        public static readonly RoutedEvent ConnectedEvent = EventManager.RegisterRoutedEvent( nameof(Connected) , RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(MediaControl) );
        public static readonly RoutedEvent DisconnectedEvent = EventManager.RegisterRoutedEvent( nameof(Disconnected) , RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(MediaControl) );
        public static readonly RoutedEvent FrameDecodedEvent = EventManager.RegisterRoutedEvent( nameof(FrameDecoded) , RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(MediaControl) );
        public static readonly RoutedEvent RegionSelectedEvent = EventManager.RegisterRoutedEvent(nameof(RegionSelected),RoutingStrategy.Direct,typeof(RoutedEventHandler<SelectedRegionRoutedEventArgs>),typeof(MediaControl));

        public static readonly DependencyProperty UriProperty = DependencyProperty.Register( nameof(Uri) , typeof(string) , typeof(MediaControl) );
        public static readonly DependencyProperty UserNameProperty = DependencyProperty.Register( nameof(UserName) , typeof(string) , typeof(MediaControl) );
        public static readonly DependencyProperty PasswordProperty = DependencyProperty.Register( nameof(Password) , typeof(string) , typeof(MediaControl) );
        public static readonly DependencyProperty TransportProperty = DependencyProperty.Register( nameof(Transport) , typeof(MediaPlayerTransport) , typeof(MediaControl) , new PropertyMetadata( new TcpMediaPlayerTransport() ) );
        public static readonly DependencyProperty FooterProperty = DependencyProperty.Register( nameof(Footer) , typeof(string) , typeof(MediaControl) );
        public static readonly DependencyProperty FooterVisibilityProperty = DependencyProperty.Register( nameof(FooterVisibility) , typeof(Visibility) , typeof(MediaControl) , new PropertyMetadata( Visibility.Collapsed ) );
        public static readonly DependencyProperty IsCommunicationStartedProperty = DependencyProperty.Register( nameof(IsCommunicationStarted) , typeof(bool) , typeof(MediaControl) , new PropertyMetadata( false ) );
        public static readonly DependencyProperty IsConnectedProperty = DependencyProperty.Register( nameof(IsConnected) , typeof(bool) , typeof(MediaControl) , new PropertyMetadata( false ) );
        public static readonly DependencyProperty IsZoomEnabledProperty = DependencyProperty.Register( nameof(IsZoomEnabled) , typeof(bool) , typeof(MediaControl) , new PropertyMetadata( false ) );
        public static readonly DependencyProperty MinimumZoomProperty = DependencyProperty.Register( nameof(MinimumZoom) , typeof(double) , typeof(MediaControl) , new PropertyMetadata( 8 ) );

        private readonly MediaClient _client;
        private readonly ObservableCollection<ErrorInfo> _errors;

        public MediaControl()
        {
            InitializeComponent();

            _client = new MediaClient( new MediaClientHandler( _image , this ,
                OnCommunicationStarted,
                OnCommunicationStopped,
                OnConnected,
                OnDisconnected,
                OnFrameDecoded,
                OnError
                ));

            _errors = new ObservableCollection<ErrorInfo>();
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

        public event RoutedEventHandler FrameDecoded
        {
            add    => AddHandler( FrameDecodedEvent , value );
            remove => RemoveHandler( FrameDecodedEvent , value );
        }

        public event RoutedEventHandler<SelectedRegionRoutedEventArgs> RegionSelected
        {
            add    => AddHandler( RegionSelectedEvent , value );
            remove => RemoveHandler( RegionSelectedEvent , value );
        }






        public NetworkStatisticsControl Statistics
        {
            get => _statistics;
        }

        public ReadOnlyCollection<ErrorInfo> Errors
        {
            get => _errors.ToReadOnly();
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

        public bool IsZoomEnabled
        {
            get => (bool) GetValue( IsZoomEnabledProperty );
            set => SetValue( IsZoomEnabledProperty , value );
        }

        public double MinimumZoom
        {
            get => (double) GetValue( MinimumZoomProperty );
            set => SetValue( MinimumZoomProperty , value );
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
            _statistics.DataSource = _client.StatisticsDataSource;
            _statistics.StartMonitoring();
        }

        private void OnUnloaded( object sender , RoutedEventArgs e )
        {
            _statistics.StopMonitoring();
            _statistics.DataSource = null;
            _client.Dispose();
        }






        public bool StartCommunication()
        {
            if ( ! _client.IsCommunicationStarted() )
            {
                return false;
            }

            _client.Uri = Uri;
            _client.UserName = UserName;
            _client.Password = Password;
            _client.Transport = Transport;

            return _client.StartCommunication();
        }

        public void StopCommunication()
        {
            _client.StopCommunication();
            _errors.Clear();
        }

        public ImageSource GetImage()
        {
            return _image.Source;
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
            Footer = _client.Uri;

            RaiseEvent( new RoutedEventArgs( ConnectedEvent ) );
        }

        protected virtual void OnDisconnected()
        {
            IsConnected = false;
            Footer = "";

            RaiseEvent( new RoutedEventArgs( DisconnectedEvent ) );
        }

        protected virtual void OnFrameDecoded()
        {
            RaiseEvent( new RoutedEventArgs( FrameDecodedEvent ) );
        }

        protected virtual void OnRegionSelected( SelectedRegionRoutedEventArgs e )
        {
            RaiseEvent( e );
        }

        protected virtual void OnError( string error )
        {
            if ( _errors.Count > 100)
            {
                _errors.RemoveAt( 0 );
            }

            _errors.Add( new ErrorInfo() { Message = error } );
        }
    }
}
