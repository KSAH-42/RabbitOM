// For multi views like quadras, etc..
// you must adapt this sample and create a usercontrol that run on different thread
// avoid to make the mainthread to consume cpu power because wpf main implement a MESSAGE LOOP a dispatcher run and redirect events
// for having an application responsible that display video
// Otherwise your UI can't not respond to users clicks, etc... your UI will hangs

using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace RabbitOM.Sample.Client.Player
{
    using RabbitOM.Streaming;
    using RabbitOM.Streaming.Rtp;
    using RabbitOM.Streaming.Rtp.H264;
    using RabbitOM.Streaming.Rtp.H265;
    using RabbitOM.Streaming.Rtp.Jpeg;
    using RabbitOM.Streaming.Rtsp;
    using RabbitOM.Streaming.Rtsp.Clients;
    using RabbitOM.Sample.Client.Player.Codecs;
    using RabbitOM.Sample.Client.Player.Codecs.FFMpeg;
    using RabbitOM.Sample.Client.Player.Dialogs;
    using RabbitOM.Sample.Client.Player.Extensions;

    // This is not a clean code here if we respect wpf, and others things, it's juste a demo, refactorization must be done
    // There is a base line here, but the right direction for writing a correct architecture is to write a graph and setup it using a builder
    // just something similar to dsf

    public partial class MainWindow : Window
    {
        public static readonly RoutedCommand FillImageCommand = new RoutedCommand();
        public static readonly RoutedCommand UniformImageCommand = new RoutedCommand();
        public static readonly RoutedCommand SaveImageCommand = new RoutedCommand();
        public static readonly RoutedCommand ShowCodecInfoImageCommand = new RoutedCommand();
        public static readonly RoutedCommand HideCodecInfoImageCommand = new RoutedCommand();

        private readonly RtspClient _client = new RtspClient();
        private readonly RtpPacketInspector _inspector = new DefaultRtpPacketInspector();
        private readonly RtpMediaBuilderAdapter _frameBuilder = new RtpMediaBuilderAdapter();
        private readonly FFMpegDecoder _decoder = new FFMpegDecoder();
        private readonly FFMpegRenderer _renderer = new FFMpegRenderer();

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
                _textBoxCodecInfo.Text = "Codec - " + e.TrackInfo.Encoder;

                _frameBuilder.Dispose();

                if ( e.TrackInfo.Encoder?.IndexOf( "H264" , StringComparison.OrdinalIgnoreCase ) >= 0 )
                {
                    _frameBuilder.Setup<H264FrameBuilder>( () =>
                    {
                        return new H264FrameBuilder()
                        {
                            SPS = Convert.FromBase64String(e.TrackInfo.SPS) ,
                            PPS = Convert.FromBase64String(e.TrackInfo.PPS) ,
                        };
                    } );

                    _decoder.Open( CodecType.H264 );
                    _renderer.Open( _image );
                }
                else if ( e.TrackInfo.Encoder?.IndexOf( "H265" , StringComparison.OrdinalIgnoreCase ) >= 0 )
                {
                    _frameBuilder.Setup<H265FrameBuilder>( () =>
                    {
                        return new H265FrameBuilder()
                        {
                            SPS = Convert.FromBase64String(e.TrackInfo.SPS) ,
                            PPS = Convert.FromBase64String(e.TrackInfo.PPS) ,
                            VPS = Convert.FromBase64String(e.TrackInfo.VPS) ,
                        };
                    } );

                    _decoder.Open( CodecType.H265 );
                    _renderer.Open( _image );
                }
                else if ( e.TrackInfo.Encoder?.IndexOf( "JPEG" , StringComparison.OrdinalIgnoreCase ) >= 0 )
                {
                    _frameBuilder.Setup<JpegFrameBuilder>( () =>
                    {
                        return new JpegFrameBuilder();
                    } );

                    _decoder.Open( CodecType.MJPEG );
                    _renderer.Open( _image );
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
                _textBoxCodecInfo.Text = "";
                _renderer.Close();
                _decoder.Close();
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
            if ( ! _decoder.IsOpened )
            {
                return;
            }

            byte[] extraParameters = null;

            if ( e.MediaElement is H265MediaElement h265Frame )
            {
                extraParameters = H265MediaElement.CreateExtraParameters( h265Frame );
            }

            if ( e.MediaElement is H264MediaElement h264Frame )
            {
                extraParameters = H264MediaElement.CreateExtraParameters( h264Frame );
            }
                         

            if ( _decoder.CanConfigure( extraParameters ) && ! _decoder.Configure( extraParameters ) )
            {
                return;
            }

            _decoder.Decode( e.MediaElement.Buffer );
        }

        private void OnFrameDecoded( object sender , DecodedEventArgs e )
        {
            _image.Dispatcher.BeginInvoke( new Action( () =>
            {
                using ( e.Surface ) // Mandatory for freeing unmanaged memory
                {
                    _renderer.Render( e.Surface );
                }
            }));
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

        private void OnExecuteShowCodecInfo( object sender , ExecutedRoutedEventArgs e )
        {
            _textBoxCodecInfo.Visibility = Visibility.Visible;
        }

        private void OnExecuteHideCodecInfo( object sender , ExecutedRoutedEventArgs e )
        {
            _textBoxCodecInfo.Visibility = Visibility.Collapsed;
        }

        private void OnCanExecuteSaveImage( object sender , CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = _client.IsConnected && _image.Source is BitmapSource;
        }

        private void OnExecuteSaveImage( object sender , ExecutedRoutedEventArgs e )
        {
            var dialog = new SaveImageDialog() { Owner = Window.GetWindow( this ) };

            dialog.Source = _image.Source as BitmapSource;

            dialog.TakeSnasphot();
            dialog.ShowDialog();
        }
    }
}
