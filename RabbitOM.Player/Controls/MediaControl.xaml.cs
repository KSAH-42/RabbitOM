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
        public static readonly RoutedEvent ZoomChangedEvent = EventManager.RegisterRoutedEvent(nameof(ZoomChanged),RoutingStrategy.Direct,typeof(RoutedEventHandler<ZoomChangedRoutedEventArgs>),typeof(MediaControl));

        public static readonly DependencyProperty StrechImageProperty = DependencyProperty.Register( nameof(StrechImage) , typeof(Stretch) , typeof(MediaControl) , new PropertyMetadata( Stretch.Fill ) );
        public static readonly DependencyProperty SourceVisibilityProperty = DependencyProperty.Register( nameof(SourceVisibility) , typeof(Visibility) , typeof(MediaControl) , new PropertyMetadata( Visibility.Collapsed ) );
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register( nameof(Source) , typeof(string) , typeof(MediaControl) );
        public static readonly DependencyProperty UserNameProperty = DependencyProperty.Register( nameof(UserName) , typeof(string) , typeof(MediaControl) );
        public static readonly DependencyProperty PasswordProperty = DependencyProperty.Register( nameof(Password) , typeof(string) , typeof(MediaControl) );
        public static readonly DependencyProperty TransportProperty = DependencyProperty.Register( nameof(Transport) , typeof(MediaPlayerTransport) , typeof(MediaControl) , new PropertyMetadata( new TcpMediaPlayerTransport() ) );
        public static readonly DependencyProperty IsCommunicationStartedProperty = DependencyProperty.Register( nameof(IsCommunicationStarted) , typeof(bool) , typeof(MediaControl) , new PropertyMetadata( false ) );
        public static readonly DependencyProperty IsConnectingProperty = DependencyProperty.Register( nameof(IsConnecting) , typeof(bool) , typeof(MediaControl) , new PropertyMetadata( false ) );
        public static readonly DependencyProperty IsConnectedProperty = DependencyProperty.Register( nameof(IsConnected) , typeof(bool) , typeof(MediaControl) , new PropertyMetadata( false ) );
        public static readonly DependencyProperty IsZoomEnabledProperty = DependencyProperty.Register( nameof(IsZoomEnabled) , typeof(bool) , typeof(MediaControl) , new PropertyMetadata( false ) );
        public static readonly DependencyProperty MinimumZoomProperty = DependencyProperty.Register( nameof(MinimumZoom) , typeof(double) , typeof(MediaControl) , new PropertyMetadata( 8 ) );

        private readonly Service _service;
        private readonly ObservableCollection<ErrorInfo> _errors;

        public MediaControl()
        {
            InitializeComponent();

            _errors = new ObservableCollection<ErrorInfo>();
            _service = new Service( this );
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

        public event RoutedEventHandler<ZoomChangedRoutedEventArgs> ZoomChanged
        {
            add    => AddHandler( ZoomChangedEvent , value );
            remove => RemoveHandler( ZoomChangedEvent , value );
        }






        public Stretch StrechImage
        {
            get => (Stretch) GetValue( StrechImageProperty );
            set => SetValue( StrechImageProperty , value );
        }

        public Visibility SourceVisibility
        {
            get => (Visibility) GetValue( SourceVisibilityProperty );
            set => SetValue( SourceVisibilityProperty , value );
        }

        public string Source
        {
            get => GetValue( SourceProperty ) as string;
            set => SetValue( SourceProperty , value );
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

        public bool IsConnecting
        {
            get => (bool) GetValue( IsConnectingProperty );
            private set => SetValue( IsConnectingProperty , value );
        }

        public bool IsConnected
        {
            get => (bool) GetValue( IsConnectedProperty );
            private set => SetValue( IsConnectedProperty , value );
        }









        public ReadOnlyObservableCollection<ErrorInfo> Errors
        {
            get => _errors.ToReadOnly();
        }

        public Image Image
        {
            get => _image;
        }

        public NetworkStatisticsControl Statistics
        {
            get => _statistics;
        }









        public bool CanConfigure()
        {
            return _service.CanConfigure();
        }

        public void Configure()
        {
            _service.Configure();
        }

        public bool StartCommunication()
        {
            return _service.StartCommunication();
        }

        public void StopCommunication()
        {
            _service.StopCommunication();
        }

        public void AddError( ErrorInfo error )
        {
            _errors.Add( error ?? throw new ArgumentNullException( nameof( error ) ) );
        }

        private void ClearImage()
        {
            _image.Source = null;
        }





        private void OnUnloaded( object sender , RoutedEventArgs e )
        {
            _service.Dispose();
        }

        protected virtual void OnCommunicationStarted()
        {
            IsCommunicationStarted = true;
            IsConnecting = true;

            RaiseEvent( new RoutedEventArgs( CommunicationStartedEvent ) );
        }

        protected virtual void OnCommunicationStopped()
        {
            IsConnecting = false;
            IsCommunicationStarted = false;

            RaiseEvent( new RoutedEventArgs( CommunicationStoppedEvent ) );
        }

        protected virtual void OnConnected()
        {
            IsConnected = true;
            IsConnecting = false;

            RaiseEvent( new RoutedEventArgs( ConnectedEvent ) );
        }

        protected virtual void OnDisconnected()
        {
            IsConnected = false;
            IsConnecting = ! _service.IsCommunicationStopping;

            RaiseEvent( new RoutedEventArgs( DisconnectedEvent ) );
        }

        protected virtual void OnFrameDecoded()
        {
            RaiseEvent( new RoutedEventArgs( FrameDecodedEvent ) );
        }

        protected virtual void OnZoomChanged( ZoomChangedRoutedEventArgs e )
        {
            RaiseEvent( e );
        }
    }
}
