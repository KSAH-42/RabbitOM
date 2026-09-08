using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RabbitOM.Player.Controls
{
    public partial class MediaPlayer : UserControl
    {
        public static readonly RoutedEvent StartedEvent = EventManager.RegisterRoutedEvent( nameof(Started), RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(MediaPlayer) );
        public static readonly RoutedEvent StoppedEvent = EventManager.RegisterRoutedEvent( nameof(Stopped), RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(MediaPlayer) );
        public static readonly RoutedEvent ConnectedEvent = EventManager.RegisterRoutedEvent( nameof(Connected) , RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(MediaPlayer) );
        public static readonly RoutedEvent DisconnectedEvent = EventManager.RegisterRoutedEvent( nameof(Disconnected) , RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(MediaPlayer) );
        public static readonly RoutedEvent FrameDecodedEvent = EventManager.RegisterRoutedEvent( nameof(FrameDecoded) , RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(MediaPlayer) );
        public static readonly RoutedEvent ZoomChangedEvent = EventManager.RegisterRoutedEvent(nameof(ZoomChanged),RoutingStrategy.Direct,typeof(RoutedEventHandler<ZoomChangedRoutedEventArgs>),typeof(MediaPlayer));

        public static readonly DependencyProperty StretchImageProperty = DependencyProperty.Register( nameof(StretchImage) , typeof(Stretch) , typeof(MediaPlayer) , new PropertyMetadata( Stretch.Fill ) );
        public static readonly DependencyProperty StatisticsVisibilityProperty = DependencyProperty.Register( nameof(StatisticsVisibility) , typeof(Visibility) , typeof(MediaPlayer) , new PropertyMetadata( Visibility.Visible ) );
        public static readonly DependencyProperty SourceVisibilityProperty = DependencyProperty.Register( nameof(SourceVisibility) , typeof(Visibility) , typeof(MediaPlayer) , new PropertyMetadata( Visibility.Collapsed ) );
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register( nameof(Source) , typeof(string) , typeof(MediaPlayer) );
        public static readonly DependencyProperty UserNameProperty = DependencyProperty.Register( nameof(UserName) , typeof(string) , typeof(MediaPlayer) );
        public static readonly DependencyProperty PasswordProperty = DependencyProperty.Register( nameof(Password) , typeof(string) , typeof(MediaPlayer) );
        public static readonly DependencyProperty TransportProperty = DependencyProperty.Register( nameof(Transport) , typeof(MediaPlayerTransport) , typeof(MediaPlayer) , new PropertyMetadata( null ) );
        public static readonly DependencyProperty IsConnectingProperty = DependencyProperty.Register( nameof(IsConnecting) , typeof(bool) , typeof(MediaPlayer) , new PropertyMetadata( false ) );
        public static readonly DependencyProperty IsConnectedProperty = DependencyProperty.Register( nameof(IsConnected) , typeof(bool) , typeof(MediaPlayer) , new PropertyMetadata( false ) );
        public static readonly DependencyProperty IsPlayingProperty = DependencyProperty.Register( nameof(IsPlaying) , typeof(bool) , typeof(MediaPlayer) , new PropertyMetadata( false ) );

        private readonly Service _service;
        private readonly ObservableCollection<ErrorInfo> _errors;

        public MediaPlayer()
        {
            InitializeComponent();

            Transport = new TcpMediaPlayerTransport();
            _errors = new ObservableCollection<ErrorInfo>();
            _service = new Service( this );
        }






        public event RoutedEventHandler Started
        {
            add    => AddHandler( StartedEvent , value );
            remove => RemoveHandler( StartedEvent , value );
        }

        public event RoutedEventHandler Stopped
        {
            add    => AddHandler( StoppedEvent , value );
            remove => RemoveHandler( StoppedEvent , value );
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






        public Stretch StretchImage
        {
            get => (Stretch) GetValue( StretchImageProperty );
            set => SetValue( StretchImageProperty , value );
        }
       
        public Visibility StatisticsVisibility
        {
            get => (Visibility) GetValue( StatisticsVisibilityProperty );
            set => SetValue( StatisticsVisibilityProperty , value );
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

        public bool IsPlaying
        {
            get => (bool) GetValue( IsPlayingProperty );
            private set => SetValue( IsPlayingProperty , value );
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









        public void Configure()
        {
            _service.Configure();
        }

        public bool IsStarted()
        {
            return _service.IsStarted;
        }

        public bool Play()
        {
            return _service.Start();
        }

        public void Stop()
        {
            _service.Stop();
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

        protected virtual void OnStarted()
        {
            IsConnecting = true;
            IsPlaying = false;
            ZoomControl.ClearSelection();

            RaiseEvent( new RoutedEventArgs( StartedEvent ) );
        }

        protected virtual void OnStopped()
        {
            IsConnecting = false;
            IsPlaying = false;
            ZoomControl.ClearSelection();

            RaiseEvent( new RoutedEventArgs( StoppedEvent ) );
        }

        protected virtual void OnConnected()
        {
            IsConnected = true;
            IsConnecting = false;
            ZoomControl.ClearSelection();

            RaiseEvent( new RoutedEventArgs( ConnectedEvent ) );
        }

        protected virtual void OnDisconnected()
        {
            IsPlaying = false;
            IsConnected = false;
            IsConnecting = ! _service.IsCommunicationStopping;
            ZoomControl.ClearSelection();

            RaiseEvent( new RoutedEventArgs( DisconnectedEvent ) );
        }

        protected virtual void OnFrameDecoded()
        {
            IsPlaying = true;

            RaiseEvent( new RoutedEventArgs( FrameDecodedEvent ) );
        }

        protected virtual void OnZoomChanged( ZoomChangedRoutedEventArgs e )
        {
            RaiseEvent( e );
        }
    }
}
