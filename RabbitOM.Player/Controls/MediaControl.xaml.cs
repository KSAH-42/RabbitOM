using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace RabbitOM.Player.Controls
{
    using RabbitOM.Net.Rtp;
    using RabbitOM.Net.Rtp.H264;
    using RabbitOM.Net.Rtp.H265;
    using RabbitOM.Net.Rtp.Jpeg;
    using RabbitOM.Net.Rtsp;
    using RabbitOM.Net.Rtsp.Clients;
    using RabbitOM.Player.Codecs;
    using RabbitOM.Player.Codecs.FFMpeg;

    public partial class MediaControl : UserControl
    {
        private readonly RtspClient _client = new RtspClient();
        private readonly RtpPacketInspector _inspector = new DefaultRtpPacketInspector();
        private readonly RtpMediaBuilderProxy _frameBuilder = new RtpMediaBuilderProxy();
        private readonly Decoder _decoder = new FFMpegDecoder();
        private readonly Renderer _renderer = new FFMpegRenderer();
        private readonly NetworkStatisticsDataSource _datasource = new NetworkStatisticsDataSource();






        public MediaControl()
        {
            InitializeComponent();
        }






        public static readonly RoutedEvent CommunicationStartedEvent =
            EventManager.RegisterRoutedEvent(
                nameof(CommunicationStarted),
                    RoutingStrategy.Direct,
                        typeof(RoutedEventHandler),
                            typeof(MediaControl));


        public static readonly RoutedEvent CommunicationStoppedEvent =
            EventManager.RegisterRoutedEvent(
                nameof(CommunicationStopped),
                    RoutingStrategy.Direct,
                        typeof(RoutedEventHandler),
                            typeof(MediaControl));

        public static readonly RoutedEvent ConnectedEvent =
            EventManager.RegisterRoutedEvent(
                nameof(Connected),
                    RoutingStrategy.Direct,
                        typeof(RoutedEventHandler),
                            typeof(MediaControl));

        public static readonly RoutedEvent DisconnectedEvent =
            EventManager.RegisterRoutedEvent(
                nameof(Disconnected),
                    RoutingStrategy.Direct,
                        typeof(RoutedEventHandler),
                            typeof(MediaControl));

        public static readonly RoutedEvent FrameReceivedEvent =
            EventManager.RegisterRoutedEvent(
                nameof(FrameReceived),
                    RoutingStrategy.Direct,
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

        public event RoutedEventHandler FrameReceived
        {
            add    => AddHandler( FrameReceivedEvent , value );
            remove => RemoveHandler( FrameReceivedEvent , value );
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

        public static readonly DependencyProperty IsCommunicationStartedProperty =
            DependencyProperty.Register(
                nameof(IsCommunicationStarted),
                    typeof(bool),
                        typeof(MediaControl),
                            new PropertyMetadata(false));

        public static readonly DependencyProperty IsConnectedProperty =
            DependencyProperty.Register(
                nameof(IsConnected),
                    typeof(bool),
                        typeof(MediaControl),
                            new PropertyMetadata(false));

        public static readonly DependencyProperty ErrorInfoProperty =
            DependencyProperty.Register(
                nameof(ErrorInfo),
                    typeof(string),
                        typeof(MediaControl));









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

        public string ErrorInfo
        {
            get => (string) GetValue( ErrorInfoProperty );
            private set => SetValue( ErrorInfoProperty , value );
        }

        public NetworkStatistics Statistics
        {
            get => _statistics;
        }






        private void OnLoaded( object sender , RoutedEventArgs e )
        {
            _client.CommunicationStarted += OnCommunicationStarted;
            _client.CommunicationStopped += OnCommunicationStopped;
            _client.Connected += OnConnected;
            _client.Disconnected += OnDisconnected;
            _client.PacketReceived += OnPacketReceived;
            _frameBuilder.MediaBuilded += OnBuildFrame;
            _frameBuilder.PacketsLost += OnPacketsLost;
            _decoder.Decoded += OnFrameDecoded;
            _statistics.DataSource = _datasource;
            _statistics.StartCollect();
        }

        private void OnUnloaded( object sender , RoutedEventArgs e )
        {
            _statistics.StopMonitoring();
            _statistics.DataSource = null;
            _client.StopCommunication();
            _client.CommunicationStarted -= OnCommunicationStarted;
            _client.CommunicationStopped -= OnCommunicationStopped;
            _client.Connected -= OnConnected;
            _client.Disconnected -= OnDisconnected;
            _client.PacketReceived -= OnPacketReceived;
            _client.Dispose();
            _frameBuilder.MediaBuilded -= OnBuildFrame;
            _frameBuilder.PacketsLost -= OnPacketsLost;
            _frameBuilder.Dispose();
            _decoder.Decoded -= OnFrameDecoded;
            _renderer.Dispose();
            _decoder.Dispose();
        }






        public void StartCommunication()
        {
            if ( string.IsNullOrWhiteSpace( Uri ) )
            {
                throw new InvalidOperationException( "the uri must be defined" );
            }

            if ( Transport == null )
            {
                throw new InvalidOperationException( "the transport must be set" );
            }

            if ( IsCommunicationStarted )
            {
                throw new InvalidOperationException( "the communication is already running" );
            }

            _client.Configuration.Uri = Uri;
            _client.Configuration.UserName = UserName;
            _client.Configuration.Password = Password;
            _client.Configuration.ReceiveTimeout = Transport.ReceiveTimeout;
            _client.Configuration.SendTimeout = Transport.SendTimeout;
            _client.Configuration.RetriesInterval = Transport.RetriesInterval;
            _client.Configuration.MediaFormat = RtspMediaFormat.Video;
            _client.Configuration.KeepAliveType = RtspKeepAliveType.Options;
            _client.Configuration.DeliveryMode = RtspDeliveryMode.Tcp;

            if ( Transport is UdpMediaPlayerTransport udpTransport )
            {
                _client.Configuration.DeliveryMode = RtspDeliveryMode.Udp;
                _client.Configuration.RtpPort = udpTransport.Port;
            }
            else if ( Transport is MulticastMediaPlayerTransport multicastTransport )
            {
                _client.Configuration.DeliveryMode = RtspDeliveryMode.Udp;
                _client.Configuration.RtpPort = multicastTransport.Port;
                _client.Configuration.MulticastAddress = multicastTransport.IPAddress;
                _client.Configuration.TimeToLive = multicastTransport.TimeToLive;
            }

            _client.StartCommunication();
        }

        public void StopCommunication()
        {
            _client.StopCommunication( TimeSpan.FromSeconds(2) );
        }

        public ImageSource GetImage()
        {
            return _image.Source;
        }

        private void Dispatch( Action action )
        {
            Dispatcher.BeginInvoke( DispatcherPriority.Render , action );
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


        private void OnCommunicationStarted( object sender , RtspClientCommunicationStartedEventArgs e )
        {
            Dispatch( OnCommunicationStarted );
        }

        private void OnCommunicationStopped( object sender , RtspClientCommunicationStoppedEventArgs e )
        {
            Dispatch( () =>
            {
                _datasource.Clear();

                OnCommunicationStopped();
            } );
        }

        private void OnConnected( object sender , RtspClientConnectedEventArgs e )
        {
            Dispatch( () =>
            {
                _frameBuilder.Dispose();

                _datasource.SetConnectionStatusOn();
                _datasource.SetTransport( _client.Configuration.DeliveryMode.ToString() );
                _datasource.SetCodec( e.TrackInfo.Encoder );
                _datasource.SetClock( e.TrackInfo.ClockRate );

                try
                {
                    CodecType codec = FFMpegCodecTypeConverter.Convert( e.TrackInfo.Encoder );

                    if ( codec == CodecType.Unknown )
                    {
                        ErrorInfo = "Format not supported ( " + e.TrackInfo.Encoder + " )";
                        return;
                    }

                    if ( codec == CodecType.H265 )
                    {
                        _frameBuilder.Setup( () => new H265FrameBuilder()
                        {
                            SPS = Convert.FromBase64String(e.TrackInfo.SPS) ,
                            PPS = Convert.FromBase64String(e.TrackInfo.PPS) ,
                            VPS = Convert.FromBase64String(e.TrackInfo.VPS) ,
                        } );
                    }

                    if ( codec == CodecType.H264 )
                    {
                        _frameBuilder.Setup( () => new H264FrameBuilder()
                        {
                            SPS = Convert.FromBase64String(e.TrackInfo.SPS) ,
                            PPS = Convert.FromBase64String(e.TrackInfo.PPS) ,
                        } );
                    }

                    if ( codec == CodecType.MJPEG )
                    {
                        _frameBuilder.Setup( () => new JpegFrameBuilder() );
                    }

                    _decoder.Open( codec );
                    _renderer.Open( _image );

                    Footer = _client.Configuration.Uri;
                }
                catch( Exception ex )
                {
                    ErrorInfo = "Internal Error: " + ex.Message;
                }
                finally
                {
                    OnConnected();
                }
            } );
        }

        private void OnDisconnected( object sender , RtspClientDisconnectedEventArgs e )
        {
            Dispatch( () =>
            {
                _datasource.SetConnectionStatusOff();
                _frameBuilder.Clear();
                _decoder.Close();
                _renderer.Close();

                OnDisconnected();
            } );
        }

        private void OnPacketReceived( object sender , RtspPacketReceivedEventArgs e )
        {
            _datasource.AddBytesReceived( e.Packet.Data.Length );

            if ( RtpPacket.TryParse( e.Packet.Data , out var packet ) && _inspector.TryInspect( packet ) )
            {
                _frameBuilder.AddPacket( packet );

                _datasource.IncreasePacketReceived();
            }
        }

        private void OnPacketsLost( object sender , RtpPacketsLostEventArgs e )
        {
            _datasource.AddPacketsLost( e.NumberOfPacketLost );
        }

        private void OnBuildFrame( object sender , RtpMediaBuildedEventArgs e )
        {
            if ( ! _decoder.IsOpened )
            {
                return;
            }

            byte[] extraParameters = e.MediaElement is IExtraParameters parameters ? parameters.GetExtraParameters() : null;

            if ( ! _decoder.CanConfigure( extraParameters ) || _decoder.Configure( extraParameters ) )
            {
                _decoder.Decode( e.MediaElement.Buffer );
            }
        }

        private void OnFrameDecoded( object sender , DecodedEventArgs e )
        {
            Dispatch( () =>
            {
                using ( e.Surface ) // add this step for freeing unmanaged memory now
                {
                    _renderer.Render( e.Surface );

                    _datasource.IncreaseFrameCount();
                    _datasource.SetFrameSize( e.Surface.Height , e.Surface.Width );
                }

                OnFrameReceived();
            });
        }
    }
}
