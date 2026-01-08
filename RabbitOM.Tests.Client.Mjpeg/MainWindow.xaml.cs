/*

This sample illustrate how to receive video from a MJPEG SOURCE only.

After configuring a jpeg rtsp server using the happyRtspServer, 

please make that you can receive the JPEG using VLC before launching this app

Download and configure this rtsp server:

https://www.happytimesoft.com/products/rtsp-server/index.html

go to the rtsp server folder, and add movies and the same folder and edit the xml configuration used by this server. and then launch this sample application.

you can also copy the configuration files located in Resources\Configuration\ folder

and run using the following command: 

RtspServer.exe -c config-JPEG.xml

Otherwise use a security ip camera, and go the web page configuration section, and select the mjpeg codec.
and then read the manufacturer pdf in order to get the right rtsp uri. 
Please also, check the rtsp settings on the web page of the camera.

 */

using System;
using System.Windows;

namespace RabbitOM.Tests.Client.Mjpeg
{
    using RabbitOM.Streaming;
    using RabbitOM.Streaming.Net.Rtp;
    using RabbitOM.Streaming.Net.Rtp.Jpeg;
    using RabbitOM.Streaming.Net.Rtsp;
    using RabbitOM.Streaming.Net.Rtsp.Clients;
    using RabbitOM.Streaming.Windows.Presentation.Renders;
    using RabbitOM.Tests.Client.Mjpeg.Extensions;
    
    public partial class MainWindow : Window
    {
        private readonly RtspClient _client = new RtspClient();
        private readonly DefaultPacketInspector _inspector = new DefaultPacketInspector();
        private readonly JpegFrameBuilder _frameBuilder = new JpegFrameBuilder();
        private readonly JpegRenderer _renderer = new JpegRenderer();
        
        private void OnWindowLoaded( object sender , RoutedEventArgs e )
        {
            _client.CommunicationStarted += OnCommunicationStarted;
            _client.CommunicationStopped += OnCommunicationStopped;
            _client.Connected += OnConnected;
            _client.Disconnected += OnDisconnected;
            _client.PacketReceived += OnPacketReceived;

            _frameBuilder.Builded += OnBuildFrame;
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
            
            _frameBuilder.Builded -= OnBuildFrame;
            _frameBuilder.Dispose();

            _renderer.Dispose();
        }

        private void OnButtonControlClick( object sender , RoutedEventArgs e )
        {
            try
            {
                if ( _client.IsCommunicationStarted )
                {
                    _client.StopCommunication( TimeSpan.FromSeconds(2) ); // It can hangs due to socket connection timeout here just specify a timeout value in this case, or do ....
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
                _textBlockInfo.Text = e.TrackInfo.Encoder.ToUpper().Contains( "JPEG" ) ? "" : "Format not supported ( " + e.TrackInfo.Encoder + " )" ;
                
                // resolution fallback are used in case where rtsp server can not deliver the width and height due of the jpeg rtp rfc limitation, it's happen when the resolution become to big and can not be placed in the rtp jpeg packet.

                _frameBuilder.Configure( new JpegSettings( ResolutionInfo.Resolution_2040x2040 ) );
                
                _renderer.TargetControl = _image;
            } ) );
        }

        private void OnDisconnected( object sender , RtspClientDisconnectedEventArgs e )
        {
            _frameBuilder.Clear();

            _image.Dispatcher.BeginInvoke( System.Windows.Threading.DispatcherPriority.Render , new Action( () =>
            {
                _textBlockInfo.Text = _client.IsCommunicationStopping ? "" : "Connecting - Communication Lost";
                _renderer.Clear();
            } ));
        }

        private void OnPacketReceived( object sender , RtspPacketReceivedEventArgs e )
        {
            if ( RtpPacket.TryParse( e.Packet.Data , out var packet ) )
            {
                _inspector.Inspect( packet );

                _frameBuilder.AddPacket( packet );
            }
        }

        private void OnBuildFrame( object sender , RtpBuildEventArgs e )
        {
            _image.Dispatcher.BeginInvoke( System.Windows.Threading.DispatcherPriority.Render , new Action( () =>
            {
                OnRenderFrame( sender , e );
            } ));
        }
        
        private void OnRenderFrame( object sender , RtpBuildEventArgs e )
        {
            _renderer.Frame = e.MediaContent.Buffer;

            if ( _renderer.CanRender() )
            {
                _renderer.Render();
            }
        }

        private void OnContextMenuImageUniform( object sender , RoutedEventArgs e )
        {
            _image.Stretch = System.Windows.Media.Stretch.Uniform;
        }

        private void OnContextMenuImageFill( object sender , RoutedEventArgs e )
        {
            _image.Stretch = System.Windows.Media.Stretch.Fill;
        }
    }
}
