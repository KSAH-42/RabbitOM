namespace RabbitOM.Decoder.FFMpeg.Streaming
{
	// don't add an id, we will put the id else where
	public sealed class RtspVideoChannel : IDisposable
	{
		private readonly IOutputTransport _outputTransport;



		public RtspVideoChannel( IOutputTransport outputTransport )
		{
			_outputTransport = outputTransport ?? throw new ArgumentNullException( nameof( outputTransport ) );
		}




		public bool IsConnected { get => throw new NotImplementedException(); }

		public bool IsStarted { get => throw new NotImplementedException(); }

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
