using System;
using System.Windows;
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

    public partial class MediaControl
    {
        private readonly RtspClient _client = new RtspClient();
        private readonly RtpPacketInspector _inspector = new DefaultRtpPacketInspector();
        private readonly RtpMediaBuilderProxy _frameBuilder = new RtpMediaBuilderProxy();
        private readonly Decoder _decoder = new FFMpegDecoder();
        private readonly Renderer _renderer = new FFMpegRenderer();
        private readonly NetworkStatisticsDataSource _datasource = new NetworkStatisticsDataSource();


        private void InitializeClient()
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

        private void UnInitializeClient()
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
                        OnError( "Format not supported ( " + e.TrackInfo.Encoder + " )" );
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
                    OnException( ex );
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
                using ( e.Surface )
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
