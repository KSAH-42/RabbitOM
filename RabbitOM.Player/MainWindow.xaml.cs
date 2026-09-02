using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace RabbitOM.Player
{
    using RabbitOM.Net.Rtp;
    using RabbitOM.Net.Rtp.H264;
    using RabbitOM.Net.Rtp.H265;
    using RabbitOM.Net.Rtp.Jpeg;
    using RabbitOM.Net.Rtsp;
    using RabbitOM.Net.Rtsp.Clients;
    using RabbitOM.Player.Codecs;
    using RabbitOM.Player.Codecs.FFMpeg;
    using RabbitOM.Player.Configuration;
    using RabbitOM.Player.Controls;
    using RabbitOM.Player.Dialogs;
    using DialogStyle = RabbitOM.Player.Themes.Styles.WindowStyle;

    public partial class MainWindow : Window
    {
        public static readonly RoutedCommand ControlCommand = new RoutedCommand();
        public static readonly RoutedCommand SaveImageCommand = new RoutedCommand();
        public static readonly RoutedCommand ShowAboutDialogCommand = new RoutedCommand();
        public static readonly RoutedCommand ShowUrisDialogCommand = new RoutedCommand();
        public static readonly RoutedCommand ShowNetworkSettingsDialogCommand = new RoutedCommand();
        public static readonly RoutedCommand ToggleFullScreenCommand = new RoutedCommand();
        public static readonly RoutedCommand FocusCommand = new RoutedCommand();

        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register( nameof(Image), typeof(ImageSource) , typeof(MainWindow) );
        public static readonly DependencyProperty StatusInfoProperty = DependencyProperty.Register( nameof(StatusInfo), typeof(string) , typeof(MainWindow) );
        public static readonly DependencyProperty ButtonStatusProperty = DependencyProperty.Register( nameof(ButtonStatus), typeof(string) , typeof(MainWindow) , new PropertyMetadata( "Play" ) );
        public static readonly DependencyProperty SelectedSourceProperty = DependencyProperty.Register( nameof(SelectedSource), typeof(string) , typeof(MainWindow) );
        public static readonly DependencyProperty FooterProperty = DependencyProperty.Register( nameof(Footer), typeof(string) , typeof(MainWindow) );
        public static readonly DependencyProperty IsConnectingProperty = DependencyProperty.Register( nameof(IsConnecting), typeof(bool) , typeof(MainWindow) );

        private readonly RtspClient _client = new RtspClient();
        private readonly RtpPacketInspector _inspector = new DefaultRtpPacketInspector();
        private readonly RtpMediaBuilderProxy _frameBuilder = new RtpMediaBuilderProxy();
        private readonly Decoder _decoder = new FFMpegDecoder();
        private readonly Renderer _renderer = new FFMpegRenderer();
        private readonly NetworkStatisticsDataSource _datasource = new NetworkStatisticsDataSource();

        public ImageSource Image
        {
            get => GetValue( ImageProperty ) as ImageSource;
            set => SetValue( ImageProperty , value );
        }

        public string StatusInfo
        {
            get => GetValue( StatusInfoProperty ) as string;
            set => SetValue( StatusInfoProperty , value );
        }

        public string ButtonStatus
        {
            get => GetValue( ButtonStatusProperty ) as string;
            set => SetValue( ButtonStatusProperty , value );
        }

        public string SelectedSource
        {
            get => GetValue( SelectedSourceProperty ) as string;
            set => SetValue( SelectedSourceProperty , value );
        }

        public string Footer
        {
            get => GetValue( FooterProperty ) as string;
            set => SetValue( FooterProperty , value );
        }

        public bool IsConnecting
        {
            get => (bool) GetValue( IsConnectingProperty );
            private set => SetValue( IsConnectingProperty , value );
        }

        public ObservableCollection<string> Sources { get; } = new ObservableCollection<string>( new ApplicationConfiguration().GetSourcesOrDefault().Select( element => element.Uri ) );

        private void OnWindowLoaded( object sender , RoutedEventArgs e )
        {
            _client.CommunicationStarted += OnCommunicationStarted;
            _client.CommunicationStopped += OnCommunicationStopped;
            _client.Connected += OnConnected;
            _client.Disconnected += OnDisconnected;
            _client.PacketReceived += OnPacketReceived;
            _frameBuilder.MediaBuilded += OnBuildFrame;
            _frameBuilder.PacketsLost += OnPacketsLost;
            _decoder.Decoded += OnFrameDecoded;
            Statistics.DataSource = _datasource;
            Statistics.StartMonitoring();
        }

        private void OnWindowClosing( object sender , System.ComponentModel.CancelEventArgs e )
        {
            Statistics.StopMonitoring();
            Statistics.DataSource = null;
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

        private void OnControl( object sender , ExecutedRoutedEventArgs e )
        {
            try
            {
                if ( _client.IsCommunicationStarted )
                {
                    _client.StopCommunication( TimeSpan.FromSeconds(2) );
                    Image = null;
                    return;
                }

                if ( ! RtspUri.TryParse( SelectedSource , out RtspUri uri ) )
                {
                    MessageBox.Show( "Invalid uri" );
                    return;
                }

                if ( ! Sources.Any( uriValue => StringComparer.OrdinalIgnoreCase.Equals( uriValue ?? string.Empty , SelectedSource ?? string.Empty ) ) )
                {
                    Sources.Add( SelectedSource );
                }

                _client.Configuration.Uri = uri.ToString( true );
                _client.Configuration.UserName = uri.UserName;
                _client.Configuration.Password = uri.Password;
                _client.Configuration.ReceiveTimeout = TimeSpan.FromSeconds( 3 );
                _client.Configuration.SendTimeout = TimeSpan.FromSeconds( 3 );
                _client.Configuration.RetriesInterval = TimeSpan.FromSeconds( 5 );
                _client.Configuration.KeepAliveType = RtspKeepAliveType.Options; // for heart beat, please read the camera vendor documentations, sometimes it just change.
                _client.Configuration.MediaFormat = RtspMediaFormat.Video;

                _client.StartCommunication();
            }
            finally
            {
                ButtonStatus = _client.IsCommunicationStarted ? "Stop" : "Play";
            }
        }

        private void OnCloseApplication( object sender , ExecutedRoutedEventArgs e )
        {
            if ( MessageBox.Show( "Would you like to close the application ?" , "Closing Application" , MessageBoxButton.YesNo , MessageBoxImage.Question) == MessageBoxResult.Yes )
            {
                this.Close();
            }
        }

        private void OnCanSaveImage( object sender , CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = Image is BitmapSource;
        }

        private void OnSaveImage( object sender , ExecutedRoutedEventArgs e )
        {
            var dialog = new SaveImageDialog() { Owner = Window.GetWindow( this ) };

            dialog.Source = Image as BitmapSource;

            dialog.TakeSnasphot();
            dialog.ShowDialog();
        }

        private void OnShowAboutDialog( object sender , ExecutedRoutedEventArgs e )
        {
            var dialog = new AboutDialog() { Owner = Window.GetWindow( this ) };

            dialog.ShowDialog();
        }

        private void OnShowUrisDialog( object sender , ExecutedRoutedEventArgs e )
        {
            var dialog = new UrisDialog() { Owner = Window.GetWindow( this ) };

            dialog.Uris.AddRange( Sources.Select( uri => new UriInfo() { Value = uri } ) );

            if (dialog.ShowDialog() == true )
            {
                var selectedUri = SelectedSource;

                Sources.Clear();
                Sources.AddRange( dialog.Uris.Select( uri => uri.Value ) );
                SelectedSource = Sources.Contains( selectedUri ) ? selectedUri : Sources.FirstOrDefault();
            }
        }

        private void OnFocusElement( object sender , ExecutedRoutedEventArgs e )
        {
            var source = e.Parameter as UIElement;

            if ( source == null )
            {
                return;
            }

            source.Focus();
        }

        private void OnShowNetworkSettingsDialog( object sender , ExecutedRoutedEventArgs e )
        {
            var dialog = new NetworkSettingsDialog() { Owner = Window.GetWindow( this ) };

            if ( dialog.ShowDialog() == true )
            {
                if ( dialog.UseUdpTransport )
                {
                    _client.Configuration.DeliveryMode = RtspDeliveryMode.Udp;
                    _client.Configuration.RtpPort = dialog.Port;
                }

                else if ( dialog.UseMulticastTransport )
                {
                    _client.Configuration.DeliveryMode = RtspDeliveryMode.Multicast; // if we see red color on wireshark even the video is displayed, please contact your adminstrator to fix the issue or change the multicast settings of device who packet, here the client doesn't send multicast cast
                    _client.Configuration.RtpPort = dialog.Port;
                    _client.Configuration.MulticastAddress = dialog.IPAddress;
                    _client.Configuration.TimeToLive = 1;
                }
                else
                {
                    _client.Configuration.DeliveryMode = RtspDeliveryMode.Tcp; // use tcp if rtsp source is located on internet
                }
            }
        }

        private void OnToggleFullScreen( object sender , ExecutedRoutedEventArgs e )
        {
            DialogStyle.SetFullScreen( this , ! DialogStyle.GetFullScreen( this ) );
        }

        private void OnCommunicationStarted( object sender , RtspClientCommunicationStartedEventArgs e )
        {
            Dispatcher.BeginInvoke( DispatcherPriority.Render , new Action( () =>
            {
                StatusInfo = "Connecting...";
                IsConnecting = true;
            } ) );
        }

        private void OnCommunicationStopped( object sender , RtspClientCommunicationStoppedEventArgs e )
        {
            Dispatcher.BeginInvoke( DispatcherPriority.Render , new Action( () =>
            {
                ZoomControl.ClearSelection();
                _datasource.Clear();
                StatusInfo = "";
                IsConnecting = false;
            } ) );
        }

        private void OnConnected( object sender , RtspClientConnectedEventArgs e )
        {
            Dispatcher.BeginInvoke( DispatcherPriority.Render , new Action( () =>
            {
                ZoomControl.ClearSelection();
                IsConnecting = false;
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
                Image = null;
                IsConnecting = ! _client.IsCommunicationStopping;
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

            byte[] extraParameters = e.MediaElement is IExtraParameters parameters ? parameters.GetExtraParameters() : null;

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
