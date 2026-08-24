using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace RabbitOM.Player.Controls
{
    using RabbitOM.Player.Codecs;
    using RabbitOM.Player.Codecs.FFMpeg;
    using RabbitOM.Streaming.Rtp;
    using RabbitOM.Streaming.Rtp.H264;
    using RabbitOM.Streaming.Rtp.H265;
    using RabbitOM.Streaming.Rtp.Jpeg;
    using RabbitOM.Streaming.Rtsp;
    using RabbitOM.Streaming.Rtsp.Clients;

    // TODO: finish to move the code from mainwindow to this usercontrol before modify the render
    public partial class MediaControl : UserControl
    {
        public static readonly RoutedEvent CommunicationStartedEvent = EventManager.RegisterRoutedEvent( "CommunicationStarted" , RoutingStrategy.Bubble , typeof(RoutedEventHandler) , typeof(MediaControl) );
        public static readonly RoutedEvent CommunicationStoppedEvent = EventManager.RegisterRoutedEvent( "CommunicationStopped" , RoutingStrategy.Bubble , typeof(RoutedEventHandler) , typeof(MediaControl) );
        public static readonly RoutedEvent ConnectedEvent = EventManager.RegisterRoutedEvent( "Connected" , RoutingStrategy.Bubble , typeof(RoutedEventHandler) , typeof(MediaControl) );
        public static readonly RoutedEvent DisconnectedEvent = EventManager.RegisterRoutedEvent( "Disconnected" , RoutingStrategy.Bubble , typeof(RoutedEventHandler) , typeof(MediaControl) );
        public static readonly RoutedEvent FrameReceivedEvent = EventManager.RegisterRoutedEvent( "Disconnected" , RoutingStrategy.Bubble , typeof(RoutedEventHandler) , typeof(MediaControl) );




        public static readonly DependencyProperty UriProperty = DependencyProperty.Register( "Uri", typeof(string) , typeof(MediaControl) );
        public static readonly DependencyProperty UserNameProperty = DependencyProperty.Register( "UserName", typeof(string) , typeof(MediaControl) );
        public static readonly DependencyProperty PasswordProperty = DependencyProperty.Register( "Password", typeof(string) , typeof(MediaControl) );
        public static readonly DependencyProperty TransportProperty = DependencyProperty.Register( "Transport", typeof(MediaPlayerTransport) , typeof(MediaControl) );
        public static readonly DependencyProperty StatusInfoProperty = DependencyProperty.Register( "StatusInfo", typeof(string) , typeof(MediaControl) );
        public static readonly DependencyProperty FooterProperty = DependencyProperty.Register( "Footer", typeof(string) , typeof(MediaControl) );
        public static readonly DependencyProperty FooterVisibilityProperty = DependencyProperty.Register( "FooterVisibility", typeof(Visibility) , typeof(MediaControl) , new PropertyMetadata( Visibility.Collapsed ) );




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





        public bool IsCommunicationStarted()
        {
            return _client.IsCommunicationStarted;
        }

        public bool StartCommunication()
        {
            throw new NotImplementedException();
        }

        public void StopCommunication()
        {
            if ( _client.IsCommunicationStarted )
            {
                _client.StopCommunication( TimeSpan.FromSeconds(2) );
                _image.Source = null;
            }
        }

        public ImageSource GetImage()
        {
            return _image.Source;
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



        protected virtual void OnCommunicationStarted()
        {
            RaiseEvent( new RoutedEventArgs( CommunicationStartedEvent , this ) );
        }

        protected virtual void OnCommunicationStopped()
        {
            RaiseEvent( new RoutedEventArgs( CommunicationStoppedEvent , this ) );
        }

        protected virtual void OnConnected()
        {
            RaiseEvent( new RoutedEventArgs( ConnectedEvent , this ) );
        }

        protected virtual void OnDisconnected()
        {
            RaiseEvent( new RoutedEventArgs( DisconnectedEvent , this ) );
        }

        protected virtual void OnFrameReceived()
        {
            RaiseEvent( new RoutedEventArgs( FrameReceivedEvent , this ) );
        }




        private void OnCommunicationStarted( object sender , RtspClientCommunicationStartedEventArgs e )
        {
            Dispatcher.BeginInvoke( DispatcherPriority.Render , new Action( () =>
            {
                StatusInfo = "Connecting";
            } ) );
        }

        private void OnCommunicationStopped( object sender , RtspClientCommunicationStoppedEventArgs e )
        {
            Dispatcher.BeginInvoke( DispatcherPriority.Render , new Action( () =>
            {
                _datasource.Clear();
                StatusInfo = "";
            } ) );
        }

        private void OnConnected( object sender , RtspClientConnectedEventArgs e )
        {
            Dispatcher.BeginInvoke( DispatcherPriority.Render , new Action( () =>
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
                        StatusInfo = "Format not supported ( " + e.TrackInfo.Encoder + " )";
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
                    StatusInfo = "";
                }
                catch( Exception ex )
                {
                    StatusInfo = "Exception Error: " + ex.Message;
                }
            } ) );
        }

        private void OnDisconnected( object sender , RtspClientDisconnectedEventArgs e )
        {
            Dispatcher.BeginInvoke( DispatcherPriority.Render , new Action( () =>
            {
                StatusInfo = "Connecting - Communication Lost";
                Footer = "";
                _image.Source = null;

                _datasource.SetConnectionStatusOff();
                _frameBuilder.Clear();
                _decoder.Close();
                _renderer.Close();
            } ));
        }

        private void OnPacketReceived( object sender , RtspPacketReceivedEventArgs e )
        {
            _datasource.AddBytesReceived( e.Packet.Data.Length );

            // TODO: move the inspector when the rtp sequence will be completed, it should be used on OnBuildFrame function, but in reality the inspector must be used in before the decoder filter 

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

            // TODO: add interface on the mediaelement for getting extraparameters and to avoid h264/h265 mediaelement cast

            byte[] extraParameters = null;

            if ( e.MediaElement is H265MediaElement h265Frame )
            {
                extraParameters = H265MediaElement.CreateExtraParameters( h265Frame );
            }

            else if ( e.MediaElement is H264MediaElement h264Frame )
            {
                extraParameters = H264MediaElement.CreateExtraParameters( h264Frame );
            }

            if ( ! _decoder.CanConfigure( extraParameters ) || _decoder.Configure( extraParameters ) )
            {
                _decoder.Decode( e.MediaElement.Buffer );
            }
        }

        private void OnFrameDecoded( object sender , DecodedEventArgs e )
        {
            Dispatcher.BeginInvoke( DispatcherPriority.Render , new Action( () =>
            {
                using ( e.Surface ) // add this step for freeing unmanaged memory now
                {
                    _renderer.Render( e.Surface );

                    _datasource.IncreaseFrameCount();
                    _datasource.SetFrameSize( e.Surface.Height , e.Surface.Width );
                }
            }));
        }
    }
}
