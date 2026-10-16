namespace RabbitOM.Decoder.Models.Configuration
{
	public class DecoderConfiguration
	{
		public string? Uri { get; set; }
		public string? MediaType { get; set; }
		public TimeSpan? ReceiveTimeout { get; set; }
		public TimeSpan? SendTimeout { get; set; }
		public TimeSpan? RetryTimeout { get; set; }
		public TimeSpan? RetryInterval { get; set; }
		public TransportConfiguration? Transport { get; set; }
	}
}
