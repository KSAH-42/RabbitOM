namespace RabbitOM.Decoder.FFMpeg.Streaming.Channels
{
	using RabbitOM.Net.Rtp;
    using RabbitOM.Net.Rtp.H264;
    using RabbitOM.Net.Rtp.H265;
    using RabbitOM.Net.Rtp.Jpeg;
    using RabbitOM.Net.Rtsp;
    using RabbitOM.Net.Rtsp.Clients;
    using RabbitOM.Decoder.FFMpeg.Streaming.Media.Decoders;
	using RabbitOM.Decoder.FFMpeg.Streaming.Media.Outputs;
	using RabbitOM.Decoder.FFMpeg.Streaming.Media.Renders;

	public sealed class RtspVideoChannel : IDisposable
	{
		private readonly RtspClient _client;
        private readonly RtpPacketInspector _inspector;
        private readonly RtpMediaBuilderProxy _frameBuilder;
		private readonly RtspVideoChannelMetrics _metrics;
		private readonly RtspVideoChannelMetrics.Writer _metricsUpdater;
        private readonly IVideoDecoder _decoder;
		private readonly IVideoRenderer _renderer;
		private readonly IVideoOutput _output;
        private RtspVideoChannelConfiguration _configuration;







		public RtspVideoChannel( IVideoDecoder decoder , IVideoRenderer renderer , IVideoOutput output ) // used decorator if need
		{
			_decoder = decoder ?? throw new ArgumentNullException( nameof( decoder ) );
			_renderer = renderer ?? throw new ArgumentNullException( nameof( renderer ) );
			_output = output ?? throw new ArgumentNullException( nameof( output ) );

			_client = new RtspClient();
            _inspector = new DefaultRtpPacketInspector();
            _frameBuilder = new RtpMediaBuilderProxy();
			_metrics = new RtspVideoChannelMetrics();
			_metricsUpdater = new RtspVideoChannelMetrics.Writer( _metrics );
            _configuration = RtspVideoChannelConfiguration.Default;

			_client.CommunicationStarted += OnClientCommunicationStarted;
			_client.CommunicationStopped += OnClientCommunicationStopped;
			_client.Connected += OnClientConnected;
			_client.Disconnected += OnClientDisconnected;
			_client.PacketReceived += OnClientPacketReceived;
			_frameBuilder.MediaBuilded += OnRtpFrameBuilded;
			_frameBuilder.PacketsLost += OnRtpPacketsLost;
			_decoder.FrameDecoded += OnFrameDecoded;
			_renderer.Rendered += OnRendered;
		}







		public bool IsConnected
        {
            get => _client.IsConnected;
        }

		public bool IsStarted
        {
            get => _client.IsCommunicationStarted;
        }

        public bool IsStopping
        {
            get => _client.IsCommunicationStopping;
        }

		public RtspVideoChannelMetrics Metrics
		{
			get => _metrics;
		}

		public RtspVideoChannelConfiguration Configuration
		{
			get => _configuration;
		}






		public bool Start( RtspVideoChannelConfiguration configuration )
		{
            if ( configuration == null )
            {
                throw new ArgumentNullException( nameof( configuration ) );
            }

			throw new NotImplementedException();
		}

		public void Stop()
		{
			_client.StopCommunication( TimeSpan.FromSeconds(2) );
		}

		public void Dispose()
		{
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

            _decoder.FrameDecoded -= OnFrameDecoded;
            _renderer.Dispose();
            _decoder.Dispose();
            _output.Dispose();
		}




		private void OnClientCommunicationStarted( object sender , RtspClientCommunicationStartedEventArgs e )
        {
        }

        private void OnClientCommunicationStopped( object sender , RtspClientCommunicationStoppedEventArgs e )
        {
            _metricsUpdater.Clear();
        }

        private void OnClientConnected( object sender , RtspClientConnectedEventArgs e )
        {
            _metricsUpdater.SetTransport( _client.Configuration.DeliveryMode.ToString() );
            _metricsUpdater.SetCodec( e.TrackInfo.Encoder );
            _metricsUpdater.SetClock( e.TrackInfo.ClockRate );

            _frameBuilder.Dispose();

            _output.Open();

            throw new NotImplementedException();
        }

        private void OnClientDisconnected( object sender , RtspClientDisconnectedEventArgs e )
        {
            _frameBuilder.Clear();
            _decoder.Close();
            _renderer.Close();
            _output.Close();
        }

        private void OnClientPacketReceived( object sender , RtspPacketReceivedEventArgs e )
        {
            _metricsUpdater.AddBytesReceived( e.Packet.Data.Length );

            if ( RtpPacket.TryParse( e.Packet.Data , out var packet ) && _inspector.TryInspect( packet ) )
            {
                _metricsUpdater.IncreasePacketReceived();

                _frameBuilder.AddPacket( packet );
            }
        }

        private void OnRtpPacketsLost( object sender , RtpPacketsLostEventArgs e )
        {
            _metricsUpdater.AddPacketsLost( e.NumberOfPacketLost );
        }

        private void OnRtpFrameBuilded( object sender , RtpMediaBuildedEventArgs e )
        {
            if ( ! _decoder.IsOpened )
            {
                return;
            }

            byte[]? extraParameters = e.MediaElement is IExtraParameters parameters ? parameters.GetExtraParameters()  : null;

            if ( ! _decoder.CanConfigure( extraParameters! ) || _decoder.Configure( extraParameters! ) )
            {
                _decoder.Decode( e.MediaElement.Buffer );
            }
        }

        private void OnFrameDecoded( object sender , FrameDecodedEventArgs e )
        {
            _renderer.Render( e.Surface );
        }

        private void OnRendered( object? sender , RenderEventArgs e )
		{
			_output.SendImage( e.Image );
		}
	}
}
