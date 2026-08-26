using System;
using System.Windows;
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

    public partial class MediaControl
    {
        private readonly RtspClient _client = new RtspClient();
        private readonly RtpPacketInspector _inspector = new DefaultRtpPacketInspector();
        private readonly RtpMediaBuilderProxy _frameBuilder = new RtpMediaBuilderProxy();
        private readonly Decoder _decoder = new FFMpegDecoder();
        private readonly Renderer _renderer = new FFMpegRenderer();
        private readonly NetworkStatisticsDataSource _datasource = new NetworkStatisticsDataSource();

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

        public bool IsCommunicationStarted()
        {
            return _client.IsCommunicationStarted;
        }

        public bool StartCommunication()
        {
            return false;
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
