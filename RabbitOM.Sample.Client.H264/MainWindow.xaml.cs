using System;
using System.Windows;
using System.Windows.Input;
using System.Linq;

namespace RabbitOM.Sample.Client.H264
{
    using RabbitOM.Sample.Client.H264.Codecs;
    using RabbitOM.Sample.Client.H264.Extensions;
    using RabbitOM.Streaming;
    using RabbitOM.Streaming.Rtp;
    using RabbitOM.Streaming.Rtp.H264;
    using RabbitOM.Streaming.Rtsp;
    using RabbitOM.Streaming.Rtsp.Clients;

    public partial class MainWindow : Window
    {
        public static readonly RoutedCommand FillImageCommand = new RoutedCommand();
        public static readonly RoutedCommand UniformImageCommand = new RoutedCommand();
        public static readonly RoutedCommand ConfigureResolutionCommand = new RoutedCommand();

        private readonly RtspClient _client = new RtspClient();
        private readonly RtpPacketInspector _inspector = new DefaultRtpPacketInspector();
        private readonly H264FrameBuilder _frameBuilder = new H264FrameBuilder();
        private H264Decoder _decoder;

        private void OnWindowLoaded( object sender , RoutedEventArgs e )
        {
            // À exécuter une seule fois au démarrage de ton application (ex: dans ton Main ou Form_Load)
            _decoder = new H264Decoder();

            _client.CommunicationStarted += OnCommunicationStarted;
            _client.CommunicationStopped += OnCommunicationStopped;
            _client.Connected += OnConnected;
            _client.Disconnected += OnDisconnected;
            _client.PacketReceived += OnPacketReceived;

            _frameBuilder.MediaBuilded += OnBuildFrame;            
        }

        private void OnFrameDecoded( System.Windows.Media.Imaging.BitmapSource newFrame )
        {
            _image.Dispatcher.BeginInvoke(new Action(() =>
            {
                _image.Source = newFrame;
            }));
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
            _decoder?.Dispose();
            _decoder = null;
        }

        private void OnButtonControlClick( object sender , RoutedEventArgs e )
        {
            try
            {
                if ( _client.IsCommunicationStarted )
                {
                    _client.StopCommunication( TimeSpan.FromSeconds(2) );
                    _image.Source = null;
                    return;
                }

                if ( ! RtspUri.TryParse( _uris.Text , out RtspUri uri ) )
                {
                    MessageBox.Show( "Invalid uri" );
                    return;
                }

                if ( ! _uris.Items.Any( _uris.Text ) )
                {
                    _uris.Items.Add( _uris.Text );
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
                _controlButton.Content = _client.IsCommunicationStarted ? "Stop" : "Play";
            }
        }

        private void OnCommunicationStarted( object sender , RtspClientCommunicationStartedEventArgs e )
        {
            _image.Dispatcher.BeginInvoke( System.Windows.Threading.DispatcherPriority.Render , new Action( () =>
            {
                _textBlockInfo.Text = "Connecting";
            } ) );
        }
        
        private void OnCommunicationStopped( object sender , RtspClientCommunicationStoppedEventArgs e )
        {
            _image.Dispatcher.BeginInvoke( System.Windows.Threading.DispatcherPriority.Render , new Action( () =>
            {
                _textBlockInfo.Text = "";
            } ) );
        }

        private void OnConnected( object sender , RtspClientConnectedEventArgs e )
        {
            _image.Dispatcher.BeginInvoke( System.Windows.Threading.DispatcherPriority.Render , new Action( () =>
            {
                _textBlockInfo.Text = "";

                _frameBuilder.Clear();

                if ( StringComparer.OrdinalIgnoreCase.Equals( e.TrackInfo.Encoder , "H264" ) )
                {
                    _frameBuilder.SPS = Convert.FromBase64String(e.TrackInfo.SPS);
                    _frameBuilder.PPS = Convert.FromBase64String(e.TrackInfo.PPS);
                    _image.Stretch = System.Windows.Media.Stretch.Uniform;
                
                    _decoder.InitializeDecoder();

                    _textBlockInfo.Text = "No yet finished, the implementation will coming soon: this week";
                }
                else
                {
                    _textBlockInfo.Text = "Format not supported ( " + e.TrackInfo.Encoder + " )";
                }
            } ) );
        }

        private void OnDisconnected( object sender , RtspClientDisconnectedEventArgs e )
        {
            _frameBuilder.Clear();

            _image.Dispatcher.BeginInvoke( System.Windows.Threading.DispatcherPriority.Render , new Action( () =>
            {
                _textBlockInfo.Text = _client.IsCommunicationStopping ? "" : "Connecting - Communication Lost";
                _decoder.Dispose();
            } ));
        }

        private void OnPacketReceived( object sender , RtspPacketReceivedEventArgs e )
        {
            if ( RtpPacket.TryParse( e.Packet.Data , out var packet ) )
            {
                if ( _inspector.TryInspect( packet ) )
                {
                    _frameBuilder.AddPacket( packet );
                }
            }
        }

        private void OnBuildFrame( object sender , RtpMediaBuildedEventArgs e )
        {
            _image.Dispatcher.BeginInvoke( System.Windows.Threading.DispatcherPriority.Render , new Action( () =>
            {
                OnRenderFrame( sender , e );
            } ));
        }
        
        private void OnRenderFrame( object sender , RtpMediaBuildedEventArgs e )
        {
            var frame = e.MediaElement as RabbitOM.Streaming.Rtp.H264.H264MediaElement;

            if ( frame == null )
            {
                return;
            }

            _decoder.StartCodePrefix = frame.StartCodePrefix;
            _decoder.PPS = frame.PPS;
            _decoder.SPS = frame.SPS;            

            var spspps = RabbitOM.Streaming.Rtp.H264.H264MediaElement.CreateParamsBuffer( frame );

            if ( _decoder.Decode( frame.Buffer , spspps.ToArray() ) )
            {
                _decoder.TargetControl = _image;
                _decoder.Render();
            }
        }

        private void OnCanExecuteFillImage( object sender , CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = _client.IsCommunicationStarted;
        }

        private void OnExecuteFillImage( object sender , ExecutedRoutedEventArgs e )
        {
            _image.Stretch = System.Windows.Media.Stretch.Fill;
        }

        private void OnCanExecuteUniformImage( object sender , CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = _client.IsCommunicationStarted;
        }

        private void OnExecuteUniformImage( object sender , ExecutedRoutedEventArgs e )
        {
            _image.Stretch = System.Windows.Media.Stretch.Uniform;
        }

        private void OnCanExecuteConfigureResolution( object sender , CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = ! _client.IsCommunicationStarted;
        }
    }
}
