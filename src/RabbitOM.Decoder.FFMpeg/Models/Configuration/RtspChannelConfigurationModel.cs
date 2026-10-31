namespace RabbitOM.Decoder.FFMpeg.Models
{
	// TODO: for validation of string don't trim throw exception in case
	// where if the value contains only one space
	// ie: bad message contains a very long string with n space chars
	// throw exception to tell the client to review its implementation for reduicing memory space and time sending and force the client to fix this issue a big string with many space carrier on the network is a just a bad packet present on the network
	// and then transform exception to http error do not make a ToString, log the exception instead.
	public sealed class RtspChannelConfigurationModel
	{
		public string? Uri { get; set; }
		public TimeSpan? ReceiveTimeout { get; set; }
		public TimeSpan? SendTimeout { get; set; }
		public TimeSpan? RetryTimeout { get; set; }
		public TimeSpan? RetryInterval { get; set; }
		public string? OutputTransportType { get; set; }
		public HttpStreamingConfigurationModel? HttpStreaming { get; set; }
		public SharedMemoryConfigurationModel? SharedMemory { get; set; }
	}
}
