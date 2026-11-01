namespace RabbitOM.Decoder.FFMpeg.Domain.Channels
{
	using RabbitOM.Decoder.FFMpeg.Domain.Codecs;
	using RabbitOM.Decoder.FFMpeg.Domain.Transports;

	public sealed class RtspVideoChannel : IDisposable
	{
		private readonly IVideoDecoderFactory _decoderFactory;
		private readonly IVideoRendererFactory _rendererFactory;
		private readonly IOutputTransport _outputTransport;



		public RtspVideoChannel( IVideoDecoderFactory decoderFactory , IVideoRendererFactory rendererFactory , IOutputTransport outputTransport )
		{
			_decoderFactory  = decoderFactory  ?? throw new ArgumentNullException( nameof( decoderFactory  ) );
			_rendererFactory = rendererFactory ?? throw new ArgumentNullException( nameof( rendererFactory ) );
			_outputTransport = outputTransport ?? throw new ArgumentNullException( nameof( outputTransport ) );
		}




		public bool IsConnected { get => throw new NotImplementedException(); }

		public bool IsStarted { get => throw new NotImplementedException(); }

		public bool IsDisposed { get => throw new NotImplementedException(); }

		public RtspVideoChannelMetrics Metrics { get => throw new NotImplementedException(); }

		public RtspVideoChannelConfiguration Configuration { get => throw new NotImplementedException(); }





		public bool Start( RtspVideoChannelConfiguration configuration )
		{
			throw new NotImplementedException();
		}

		public void Stop()
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}
