// For multi views like quadras, etc..
// you must adapt this sample and create a usercontrol that run on different thread
// avoid to make the mainthread to consume cpu power because wpf main implement a MESSAGE LOOP a dispatcher run and redirect events
// for having an application responsible that display video
// Otherwise your UI can't not respond to users clicks, etc... your UI will hangs

// There is a base line here, but the right direction for writing a correct architecture is probably to write a graph and setup it using a builder
// just something similar to dsf using a modern approachs
// or something like a micro service capable to configure just a simple pipeline shoud be enough
// and may be having different microservice per graph type could be enough

using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace RabbitOM.Sample.Player
{
    using RabbitOM.Streaming.Rtp;
    using RabbitOM.Streaming.Rtp.H264;
    using RabbitOM.Streaming.Rtp.H265;
    using RabbitOM.Streaming.Rtp.Jpeg;
    using RabbitOM.Streaming.Rtsp;
    using RabbitOM.Streaming.Rtsp.Clients;
    using RabbitOM.Sample.Player.Codecs;
    using RabbitOM.Sample.Player.Codecs.FFMpeg;
    using RabbitOM.Sample.Player.Configuration;
    using RabbitOM.Sample.Player.Dialogs;

    public partial class MainWindow : Window
    {
        public static readonly RoutedCommand ControlCommand = new RoutedCommand();
        public static readonly RoutedCommand SaveImageCommand = new RoutedCommand();

        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register( "Image", typeof(ImageSource) , typeof(MainWindow) );
        public static readonly DependencyProperty StatusInfoProperty = DependencyProperty.Register( "StatusInfo", typeof(string) , typeof(MainWindow) );
        public static readonly DependencyProperty CodecInfoProperty = DependencyProperty.Register( "CodecInfo", typeof(string) , typeof(MainWindow) );
        public static readonly DependencyProperty ButtonStatusProperty = DependencyProperty.Register( "ButtonStatus", typeof(string) , typeof(MainWindow) , new PropertyMetadata( "Play" ) );
        public static readonly DependencyProperty SelectedUriProperty = DependencyProperty.Register( "SelectedUri", typeof(string) , typeof(MainWindow) );

        private readonly RtspClient _client = new RtspClient();
        private readonly RtpPacketInspector _inspector = new DefaultRtpPacketInspector();
        private readonly RtpMediaBuilderProxy _frameBuilder = new RtpMediaBuilderProxy();
        private readonly FFMpegDecoder _decoder = new FFMpegDecoder();
        private readonly FFMpegRenderer _renderer = new FFMpegRenderer();

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

        public ObservableCollection<string> Uris { get; } = new ObservableCollection<string>( ApplicationConfiguration.CreateDefaultUris() );

        private void OnWindowLoaded( object sender , RoutedEventArgs e )
        {
            _client.CommunicationStarted += OnCommunicationStarted;
            _client.CommunicationStopped += OnCommunicationStopped;
            _client.Connected += OnConnected;
            _client.Disconnected += OnDisconnected;
            _client.PacketReceived += OnPacketReceived;
            _frameBuilder.MediaBuilded += OnBuildFrame;
            _decoder.Decoded += OnFrameDecoded;
        }

        private void OnWindowClosing( object sender , System.ComponentModel.CancelEventArgs e )
        {
            _client.StopCommunication();
            _client.CommunicationStarted -= OnCommunicationStarted;
            _client.CommunicationStopped -= OnCommunicationStopped;
            _client.Connected -= OnConnected;
            _client.Disconnected -= OnDisconnected;
            _client.PacketReceived -= OnPacketReceived;
            _client.Dispose();
            _frameBuilder.MediaBuilded -= OnBuildFrame;
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
                _client.Configuration.KeepAliveType = RtspKeepAliveType.Options;
                _client.Configuration.MediaFormat = RtspMediaFormat.Video;
                _client.Configuration.DeliveryMode = RtspDeliveryMode.Tcp;

                _client.StartCommunication();
            }
            finally
            {
                ButtonStatus = _client.IsCommunicationStarted ? "Stop" : "Play";
            }
        }

        private void OnSaveImage( object sender , CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = _client.IsConnected && Image is BitmapSource;
        }

        private void OnSaveImage( object sender , ExecutedRoutedEventArgs e )
        {
            var dialog = new SaveImageDialog() { Owner = Window.GetWindow( this ) };

            dialog.Source = Image as BitmapSource;

            dialog.TakeSnasphot();
            dialog.ShowDialog();
        }

        private void OnCommunicationStarted( object sender , RtspClientCommunicationStartedEventArgs e )
        {
            Dispatcher.BeginInvoke( DispatcherPriority.Render , new Action( () => StatusInfo = "Connecting" ) );
        }

        private void OnCommunicationStopped( object sender , RtspClientCommunicationStoppedEventArgs e )
        {
            Dispatcher.BeginInvoke( DispatcherPriority.Render , new Action( () => StatusInfo = "" ) );
        }

        private void OnConnected( object sender , RtspClientConnectedEventArgs e )
        {
            Dispatcher.BeginInvoke( DispatcherPriority.Render , new Action( () =>
            {
                _frameBuilder.Dispose();

                CodecType codec = FFMpegCodecTypeConverter.Convert( e.TrackInfo.Encoder );

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

                if ( codec != CodecType.Unknown )
                {
                    _decoder.Open( codec );
                    _renderer.Open( _image );

                    CodecInfo = "Codec - " + e.TrackInfo.Encoder;
                    StatusInfo = "";
                    return;
                }

                StatusInfo = "Format not supported ( " + e.TrackInfo.Encoder + " )";
            } ) );
        }

        private void OnDisconnected( object sender , RtspClientDisconnectedEventArgs e )
        {
            Dispatcher.BeginInvoke( DispatcherPriority.Render , new Action( () =>
            {
                StatusInfo = "Connecting - Communication Lost";
                CodecInfo = "";

                _frameBuilder.Clear();
                _renderer.Close();
                _decoder.Close();
            } ));
        }

        private void OnPacketReceived( object sender , RtspPacketReceivedEventArgs e )
        {
            if ( RtpPacket.TryParse( e.Packet.Data , out var packet ) && _inspector.TryInspect( packet ) )
            {
                _frameBuilder.AddPacket( packet );
            }
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
                }
            }));
        }
    }
}
