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

    public partial class MediaControl
    {
        sealed class MediaControlService : IDisposable
        {
            private readonly RtspClient _client;
            private readonly RtpPacketInspector _inspector;
            private readonly RtpMediaBuilderProxy _frameBuilder;
            private readonly Decoder _decoder;
            private readonly Renderer _renderer;
            private readonly NetworkStatisticsDataSource _statistics;
            private readonly MediaControl _control;




            public MediaControlService( MediaControl control ) // we inject the control here without interface, due that this class is a parts of MediaControl class and there is not reason here to use an interface
            {
                _control = control ?? throw new ArgumentNullException( nameof( control ) );

                _client = new RtspClient();
                _inspector = new DefaultRtpPacketInspector();
                _frameBuilder = new RtpMediaBuilderProxy();
                _decoder = new FFMpegDecoder();
                _renderer = new FFMpegRenderer();
                _statistics = new NetworkStatisticsDataSource();
            }




            public void Initialize()
            {
                _client.CommunicationStarted += OnClientCommunicationStarted;
                _client.CommunicationStopped += OnClientCommunicationStopped;
                _client.Connected += OnClientConnected;
                _client.Disconnected += OnClientDisconnected;
                _client.PacketReceived += OnClientPacketReceived;
                _frameBuilder.MediaBuilded += OnRtpFrameBuilded;
                _frameBuilder.PacketsLost += OnRtpPacketsLost;
                _decoder.Decoded += OnFrameDecoded;
                _control.Statistics.DataSource = _statistics;
                _control.Statistics.StartMonitoring();
            }

            public void Dispose()
            {
                _control.Statistics.StopMonitoring();
                _control.Statistics.DataSource = null;
                _client.StopCommunication();
                _client.CommunicationStarted -= OnClientCommunicationStarted;
                _client.CommunicationStopped -= OnClientCommunicationStopped;
                _client.Connected -= OnClientConnected;
                _client.Disconnected -= OnClientDisconnected;
                _client.PacketReceived -= OnClientPacketReceived;
                _client.Dispose();
                _frameBuilder.MediaBuilded -= OnRtpFrameBuilded;
                _frameBuilder.PacketsLost -= OnRtpPacketsLost;
                _frameBuilder.Dispose();
                _decoder.Decoded -= OnFrameDecoded;
                _renderer.Dispose();
                _decoder.Dispose();
            }







            public bool CanConfigure()
            {
                if ( string.IsNullOrWhiteSpace( _control.Source ) || _control.Transport == null )
                {
                    return false;
                }

                return ! _client.IsCommunicationStarted;
            }

            public void Configure()
            {
                _client.Configuration.Uri = _control.Source;
                _client.Configuration.UserName = _control.UserName;
                _client.Configuration.Password = _control.Password;
                _client.Configuration.ReceiveTimeout = _control.Transport.ReceiveTimeout;
                _client.Configuration.SendTimeout = _control.Transport.SendTimeout;
                _client.Configuration.RetriesInterval = _control.Transport.RetriesInterval;
                _client.Configuration.MediaFormat = RtspMediaFormat.Video;
                _client.Configuration.KeepAliveType = RtspKeepAliveType.Options;
                _client.Configuration.DeliveryMode = RtspDeliveryMode.Tcp;

                if ( _control.Transport is UdpMediaPlayerTransport udpTransport )
                {
                    _client.Configuration.DeliveryMode = RtspDeliveryMode.Udp;
                    _client.Configuration.RtpPort = udpTransport.Port;
                }
                else if ( _control.Transport is MulticastMediaPlayerTransport multicastTransport )
                {
                    _client.Configuration.DeliveryMode = RtspDeliveryMode.Udp;
                    _client.Configuration.RtpPort = multicastTransport.Port;
                    _client.Configuration.MulticastAddress = multicastTransport.IPAddress;
                    _client.Configuration.TimeToLive = multicastTransport.TimeToLive;
                }
            }

            public bool StartCommunication()
            {
                return _client.StartCommunication();
            }

            public void StopCommunication()
            {
                _client.StopCommunication( TimeSpan.FromSeconds(2) );
            }








            private void OnClientCommunicationStarted( object sender , RtspClientCommunicationStartedEventArgs e )
            {
                _control.Dispatcher.BeginFastInvoke( _control.OnCommunicationStarted );
            }

            private void OnClientCommunicationStopped( object sender , RtspClientCommunicationStoppedEventArgs e )
            {
                _control.Dispatcher.BeginFastInvoke( () =>
                {
                    _statistics.Clear();

                    _control.OnCommunicationStopped();
                } );
            }

            private void OnClientConnected( object sender , RtspClientConnectedEventArgs e )
            {
                _control.Dispatcher.BeginFastInvoke( () =>
                {
                    _frameBuilder.Dispose();

                    _statistics.SetConnectionStatusOn();
                    _statistics.SetTransport( _client.Configuration.DeliveryMode.ToString() );
                    _statistics.SetCodec( e.TrackInfo.Encoder );
                    _statistics.SetClock( e.TrackInfo.ClockRate );

                    try
                    {
                        CodecType codec = FFMpegCodecTypeConverter.Convert( e.TrackInfo.Encoder );

                        if ( codec == CodecType.Unknown )
                        {
                            _control.AddError( "Format not supported ( " + e.TrackInfo.Encoder + " )" );
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
                        _renderer.Open( _control.Image );
                    }
                    catch( Exception ex )
                    {
                        _control.AddError( ex.Message );
                    }
                    finally
                    {
                        _control.OnConnected();
                    }
                } );
            }

            private void OnClientDisconnected( object sender , RtspClientDisconnectedEventArgs e )
            {
                _control.Dispatcher.BeginFastInvoke( () =>
                {
                    _statistics.SetConnectionStatusOff();
                    _frameBuilder.Clear();
                    _decoder.Close();
                    _renderer.Close();

                    _control.Image.Source = null;
                    _control.OnDisconnected();
                } );
            }

            private void OnClientPacketReceived( object sender , RtspPacketReceivedEventArgs e )
            {
                _statistics.AddBytesReceived( e.Packet.Data.Length );

                if ( RtpPacket.TryParse( e.Packet.Data , out var packet ) && _inspector.TryInspect( packet ) )
                {
                    _frameBuilder.AddPacket( packet );

                    _statistics.IncreasePacketReceived();
                }
            }

            private void OnRtpPacketsLost( object sender , RtpPacketsLostEventArgs e )
            {
                _statistics.AddPacketsLost( e.NumberOfPacketLost );
            }

            private void OnRtpFrameBuilded( object sender , RtpMediaBuildedEventArgs e )
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
                _control.Dispatcher.BeginFastInvoke( () =>
                {
                    using ( e.Surface )
                    {
                        _renderer.Render( e.Surface );
                        _statistics.SetFrameSize( e.Surface.Height , e.Surface.Width );
                        _statistics.IncreaseFrameCount();
                    }

                    _control.OnFrameDecoded();
                });
            }
        }
    }
}
