/*
 
 Please read this section it's important, 
 
 No MVVM is used here, this is not the goal

 This sample illustrate how to receive video from a MJPEG SOURCE only.

 And doesn't contains any optimizations. 

 If you want better performance do not used a bitmap used in this sample

 instead used a WritableBitmap

 About multhreading, you must used the dispatcher and to used BeginInvoke method instead of Invoke method.

 to get more details scroll down a take a look on the method used for receiving packet. 

 If you want to display multiple streams you must refactor this code and go more deeply in wpf and using the dispatcher and multhreading.

 This is not the goal of this sample code


 To Avoid a dispatcher dead lock using BeginInvoke and not the Invoke method
 
 because it in some case it is possible that the dispatcher wait the completion of the main thread 
 it is avoid re entrant call when the OnRenderFrame is call and the client rtps want to stop
 that the main thread where the main is actually occupied to stop the client rtsp
            
 */


using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace RabbitOM.Streaming.Tests.Mjpeg
{
    using RabbitOM.Streaming;
    using RabbitOM.Streaming.Rtp;
    using RabbitOM.Streaming.Rtp.Framing;
    using RabbitOM.Streaming.Rtp.Framing.Jpeg;
    using RabbitOM.Streaming.Rtsp;
    using RabbitOM.Streaming.Rtsp.Clients;

    public partial class MainWindow : Window
    {
        private readonly RtspClient _client = new RtspClient();
        private readonly RtpFrameBuilder _frameBuilder = new JpegFrameBuilder();
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnWindowLoaded( object sender , RoutedEventArgs e )
        {
            _client.CommunicationStarted += OnCommunicationStarted;
            _client.CommunicationStopped += OnCommunicationStopped;
            _client.Connected += OnConnected;
            _client.Disconnected += OnDisconnected;
            _client.PacketReceived += OnPacketReceived;

            _frameBuilder.FrameReceived += OnFrameReceived;
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
            
            _frameBuilder.FrameReceived -= OnFrameReceived;
            _frameBuilder.Dispose();
        }

        private void OnButtonControlClick( object sender , RoutedEventArgs e )
        {
            try
            {
                if ( _client.IsCommunicationStarted )
                {
                    _client.StopCommunication();
                    _image.Source = null;
                    return;
                }

                if ( ! RtspUri.TryParse( _uris.Text , out RtspUri uri ) )
                {
                    MessageBox.Show( "Invalid uri" );
                    return;
                }

                if ( ! _uris.Items.Contains( _uris.Text ) )
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
                _textBlockInfo.Text = e.TrackInfo.Encoder.ToUpper().Contains( "JPEG" ) ? "" : "Format not supported ( " + e.TrackInfo.Encoder + " )" ;
            } ) );
        }

        private void OnDisconnected( object sender , RtspClientDisconnectedEventArgs e )
        {
            _frameBuilder.Clear();

            _image.Dispatcher.BeginInvoke( System.Windows.Threading.DispatcherPriority.Render , new Action( () =>
            {
                _textBlockInfo.Text = _client.IsCommunicationStopping ? "" : "Connecting - Communication Lost";
                _image.Source = null;
            } ));
        }

        private void OnPacketReceived( object sender , RtspPacketReceivedEventArgs e )
        {
            _frameBuilder.Write( e.Packet.Data );
        }

        private void OnFrameReceived( object sender , RtpFrameReceivedEventArgs e )
        {
            _image.Dispatcher.BeginInvoke( System.Windows.Threading.DispatcherPriority.Render , new Action( () => 
            {
                new Action( () => OnRenderFrame( sender , e ) ).TryInvoke() ;
            } ));
        }

        private void OnRenderFrame( object sender , RtpFrameReceivedEventArgs e )
        {
            try
            {
                var image = new BitmapImage();

                image.BeginInit();
                image.CacheOption = BitmapCacheOption.Default;
                image.StreamSource = new MemoryStream( e.Frame.Data );
                image.EndInit();

                _image.BeginInit();
                _image.Source = _client.IsCommunicationStopping ? null : image;
                _image.EndInit();
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex );
            }
        }
    }
}
