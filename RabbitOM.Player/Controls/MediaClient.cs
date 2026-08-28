using System;

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

    public sealed class MediaClient : IDisposable
    {
        private readonly RtspClient _client;
        private readonly RtpPacketInspector _inspector;
        private readonly RtpMediaBuilderProxy _frameBuilder;
        private readonly Decoder _decoder;
        private readonly Renderer _renderer;
        private readonly NetworkStatisticsDataSource _datasource;
        private readonly IMediaClientHandler _handler;





        public MediaClient( IMediaClientHandler handler )
        {
            _handler = handler ?? throw new ArgumentNullException( nameof( handler ) );

            _client = new RtspClient();
            _inspector = new DefaultRtpPacketInspector();
            _frameBuilder = new RtpMediaBuilderProxy();
            _decoder = new FFMpegDecoder();
            _renderer = new FFMpegRenderer();
            _datasource = new NetworkStatisticsDataSource();

            _client.CommunicationStarted += OnCommunicationStarted;
            _client.CommunicationStopped += OnCommunicationStopped;
            _client.Connected += OnConnected;
            _client.Disconnected += OnDisconnected;
            _client.PacketReceived += OnPacketReceived;
            _frameBuilder.MediaBuilded += OnBuildFrame;
            _frameBuilder.PacketsLost += OnPacketsLost;
            _decoder.Decoded += OnFrameDecoded;
        }





        public string Uri { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public MediaPlayerTransport Transport { get; set; }

        public IStatisticsDataSource DataSource { get => _datasource; }






        public bool IsCommunicationStarted()
        {
            return _client.IsCommunicationStarted;
        }

        public bool StartCommunication()
        {
            if ( string.IsNullOrWhiteSpace( Uri ) || Transport == null )
            {
                return false;
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

            return _client.StartCommunication();
        }

        public void StopCommunication()
        {
            _client.StopCommunication( TimeSpan.FromSeconds(2) );
        }

        public void Dispose()
        {
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

        private void OnCommunicationStarted( object sender , RtspClientCommunicationStartedEventArgs e )
        {
            _handler.Dispatch( _handler.OnCommunicationStarted );
        }

        private void OnCommunicationStopped( object sender , RtspClientCommunicationStoppedEventArgs e )
        {
            _handler.Dispatch( () =>
            {
                _datasource.Clear();

                _handler.OnCommunicationStopped();
            } );
        }

        private void OnConnected( object sender , RtspClientConnectedEventArgs e )
        {
            _handler.Dispatch( () =>
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
                        _handler.OnError( "Format not supported ( " + e.TrackInfo.Encoder + " )" );
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
                    _renderer.Open( _handler.Image );
                }
                catch( Exception ex )
                {
                    _handler.OnError( ex.Message );
                }
                finally
                {
                    _handler.OnConnected();
                }
            } );
        }

        private void OnDisconnected( object sender , RtspClientDisconnectedEventArgs e )
        {
            _handler.Dispatch( () =>
            {
                _datasource.SetConnectionStatusOff();
                _frameBuilder.Clear();
                _decoder.Close();
                _renderer.Close();

                _handler.Image.Source = null;
                _handler.OnDisconnected();
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
            _handler.Dispatch( () =>
            {
                using ( e.Surface )
                {
                    _renderer.Render( e.Surface );

                    _datasource.IncreaseFrameCount();
                    _datasource.SetFrameSize( e.Surface.Height , e.Surface.Width );
                }

                _handler.OnFrameDecoded();
            });
        }
    }
}
