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
    using RabbitOM.Streaming.Rtp;
    using RabbitOM.Streaming.Rtp.H264;
    using RabbitOM.Streaming.Rtp.H265;
    using RabbitOM.Streaming.Rtp.Jpeg;
    using RabbitOM.Streaming.Rtsp;
    using RabbitOM.Streaming.Rtsp.Clients;
    using RabbitOM.Player.Codecs;
    using RabbitOM.Player.Codecs.FFMpeg;
    using RabbitOM.Player.Configuration;
    using RabbitOM.Player.Controls;
    using RabbitOM.Player.Dialogs;
    using RabbitOM.Player.Extensions;

    public partial class MainWindow : Window
    {
        public static readonly RoutedCommand ControlCommand = new RoutedCommand();
        public static readonly RoutedCommand SaveImageCommand = new RoutedCommand();
        public static readonly RoutedCommand ShowAboutDialogCommand = new RoutedCommand();
        public static readonly RoutedCommand ShowUrisDialogCommand = new RoutedCommand();
        public static readonly RoutedCommand ShowNetworkSettingsDialogCommand = new RoutedCommand();

        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register( "Image", typeof(ImageSource) , typeof(MainWindow) );
        public static readonly DependencyProperty StatusInfoProperty = DependencyProperty.Register( "StatusInfo", typeof(string) , typeof(MainWindow) );
        public static readonly DependencyProperty CodecInfoProperty = DependencyProperty.Register( "CodecInfo", typeof(string) , typeof(MainWindow) );
        public static readonly DependencyProperty ButtonStatusProperty = DependencyProperty.Register( "ButtonStatus", typeof(string) , typeof(MainWindow) , new PropertyMetadata( "Play" ) );
        public static readonly DependencyProperty SelectedUriProperty = DependencyProperty.Register( "SelectedUri", typeof(string) , typeof(MainWindow) );
        public static readonly DependencyProperty FooterProperty = DependencyProperty.Register( "Footer", typeof(string) , typeof(MainWindow) );

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

        public string CodecInfo
        {
            get => GetValue( CodecInfoProperty ) as string;
            set => SetValue( CodecInfoProperty , value );
        }

        public string ButtonStatus
        {
            get => GetValue( ButtonStatusProperty ) as string;
            set => SetValue( ButtonStatusProperty , value );
        }

        public string SelectedUri
        {
            get => GetValue( SelectedUriProperty ) as string;
            set => SetValue( SelectedUriProperty , value );
        }

        public string Footer
        {
            get => GetValue( FooterProperty ) as string;
            set => SetValue( FooterProperty , value );
        }

        public ObservableCollection<string> Uris { get; } = new ObservableCollection<string>( ApplicationConfiguration.Load().GetSourcesOrDefault().Select( element => element.Uri ) );

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
            _statistics.DataSource = _datasource;
            _statistics.StartCollect();
        }

        private void OnWindowClosing( object sender , System.ComponentModel.CancelEventArgs e )
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

                if ( ! RtspUri.TryParse( SelectedUri , out RtspUri uri ) )
                {
                    MessageBox.Show( "Invalid uri" );
                    return;
                }

                if ( ! Uris.Any( uriValue => StringComparer.OrdinalIgnoreCase.Equals( uriValue ?? string.Empty , SelectedUri ?? string.Empty ) ) )
                {
                    Uris.Add( SelectedUri );
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

            dialog.Uris.AddRange( Uris.Select( uri => new UriInfo() { Value = uri } ) );

            if (dialog.ShowDialog() == true )
            {
                var selectedUri = SelectedUri;

                Uris.Clear();
                Uris.AddRange( dialog.Uris.Select( uri => uri.Value ) );
                SelectedUri = Uris.Contains( selectedUri ) ? selectedUri : Uris.FirstOrDefault();
            }
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
                _datasource.SetConnectionStatusOn();
                _datasource.SetTransport( _client.Configuration.DeliveryMode.ToString() );
                _datasource.SetCodec( e.TrackInfo.Encoder );

                _frameBuilder.Dispose();

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

                    CodecInfo = $"Codec : {e.TrackInfo.Encoder} | Clock : {e.TrackInfo.ClockRate}Hz";
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
                CodecInfo = "";
                Footer = "";
                Image = null;

                _datasource.SetConnectionStatusOff();
                _frameBuilder.Clear();
                _decoder.Close();
                _renderer.Close();
            } ));
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
                using ( e.Surface ) // Here, that's absolutely mandatory for freeing unmanaged memory 
                {
                    _renderer.Render( e.Surface );

                    _datasource.IncreaseFrameCount();
                    _datasource.SetFrameSize( e.Surface.Height , e.Surface.Width );
                }
            }));
        }
    }
}
