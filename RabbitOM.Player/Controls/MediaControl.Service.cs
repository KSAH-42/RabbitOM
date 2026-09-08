using System;
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
        public sealed class Service : IDisposable
        {
            private readonly RtspClient _client;
            private readonly RtpPacketInspector _inspector;
            private readonly RtpMediaBuilderProxy _frameBuilder;
            private readonly Decoder _decoder;
            private readonly Renderer _renderer;
            private readonly NetworkStatisticsDataSource _datasource;
            private readonly MediaControl _control;


            public Service( MediaControl control ) // here we inject the control here without using an interface for a simple reasons: it just a part of MediaControl class and we don't need to mock, it's enougth, if this class is outside the MediaControl class, at this moment yes, we need to inject something, but not here, it's too much.
            {
                _control = control ?? throw new ArgumentNullException( nameof( control ) );

                _client = new RtspClient();
                _inspector = new DefaultRtpPacketInspector();
                _frameBuilder = new RtpMediaBuilderProxy();
                _decoder = new FFMpegDecoder();
                _renderer = new FFMpegRenderer();
                _datasource = new NetworkStatisticsDataSource();

                _client.CommunicationStarted += OnClientCommunicationStarted;
                _client.CommunicationStopped += OnClientCommunicationStopped;
                _client.Connected += OnClientConnected;
                _client.Disconnected += OnClientDisconnected;
                _client.PacketReceived += OnClientPacketReceived;
                _frameBuilder.MediaBuilded += OnRtpFrameBuilded;
                _frameBuilder.PacketsLost += OnRtpPacketsLost;
                _decoder.Decoded += OnFrameDecoded;
                _control.Statistics.DataSource = _datasource;
                _control.Statistics.StartMonitoring();
            }






            public bool IsCommunicationStopping
            {
                get => _client.IsCommunicationStopping;
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



            private void OnClientCommunicationStarted( object sender , RtspClientCommunicationStartedEventArgs e )
            {
                _control.Dispatcher.BeginInvoke( DispatcherPriority.Render , () =>
                {
                    _control.ClearImage();
                    _control.OnCommunicationStarted();
                } );
            }

            private void OnClientCommunicationStopped( object sender , RtspClientCommunicationStoppedEventArgs e )
            {
                _control.Dispatcher.BeginInvoke( DispatcherPriority.Render , () =>
                {
                    _datasource.Clear();

                    _control.ClearImage();
                    _control.OnCommunicationStopped();
                } );
            }

            private void OnClientConnected( object sender , RtspClientConnectedEventArgs e )
            {
                _control.Dispatcher.BeginInvoke( DispatcherPriority.Render , () =>
                {
                    _datasource.SetConnectionStatusOn();
                    _datasource.SetTransport( _client.Configuration.DeliveryMode.ToString() );
                    _datasource.SetCodec( e.TrackInfo.Encoder );
                    _datasource.SetClock( e.TrackInfo.ClockRate );

                    _frameBuilder.Dispose(); // from .net recommendations, dispose must not throw any exceptions

                    try
                    {
                        var codec = FFMpegCodecTypeConverter.Convert( e.TrackInfo.Encoder );

                        if ( codec == CodecType.MJPEG )
                        {
                            _frameBuilder.Setup( () => new JpegFrameBuilder() );
                        }
                        else if ( codec == CodecType.H264 )
                        {
                            _frameBuilder.Setup( () => new H264FrameBuilder( Convert.FromBase64String(e.TrackInfo.SPS) , Convert.FromBase64String(e.TrackInfo.PPS) ) );
                        }
                        else if ( codec == CodecType.H265 )
                        {
                            _frameBuilder.Setup( () => new H265FrameBuilder( Convert.FromBase64String(e.TrackInfo.SPS) , Convert.FromBase64String(e.TrackInfo.PPS) , Convert.FromBase64String(e.TrackInfo.VPS) ) );
                        }
                        else
                        {
                            _control.AddError( "Format not supported ( " + e.TrackInfo.Encoder + " )" );
                            return;
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
                _control.Dispatcher.BeginInvoke( DispatcherPriority.Render , () =>
                {
                    _datasource.SetConnectionStatusOff();
                    _frameBuilder.Clear();
                    _decoder.Close();
                    _renderer.Close();

                    _control.ClearImage();
                    _control.OnDisconnected();
                } );
            }

            private void OnClientPacketReceived( object sender , RtspPacketReceivedEventArgs e )
            {
                _datasource.AddBytesReceived( e.Packet.Data.Length );

                if ( RtpPacket.TryParse( e.Packet.Data , out var packet ) && _inspector.TryInspect( packet ) )
                {
                    _frameBuilder.AddPacket( packet );

                    _datasource.IncreasePacketReceived();
                }
            }

            private void OnRtpPacketsLost( object sender , RtpPacketsLostEventArgs e )
            {
                _datasource.AddPacketsLost( e.NumberOfPacketLost );
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
                _control.Dispatcher.BeginInvoke( DispatcherPriority.Render , () =>
                {
                    using ( e.Surface ) // mandatory to free unmanaged cloned buffer
                    {
                        _renderer.Render( e.Surface );
                        _datasource.SetFrameSize( e.Surface.Height , e.Surface.Width );
                        _datasource.IncreaseFrameCount();
                    }

                    _control.OnFrameDecoded();
                });
            }
        }
    }
}
