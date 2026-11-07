namespace RabbitOM.Decoder.FFMpeg.Domain.Channels
{
	using RabbitOM.Decoder.FFMpeg.Domain.Media;
	using RabbitOM.Decoder.FFMpeg.Domain.Transports;

	public sealed class RtspVideoChannel : IDisposable
	{
		private readonly IVideoDecoder _decoder;
		private readonly IVideoRenderer _renderer;
		private readonly IOutputTransport _outputTransport;



		public RtspVideoChannel( IVideoDecoder decoder , IVideoRenderer renderer , IOutputTransport outputTransport )
		{
			_decoder  = decoder ?? throw new ArgumentNullException( nameof( decoder ) );
			_renderer = renderer ?? throw new ArgumentNullException( nameof( renderer ) );
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
