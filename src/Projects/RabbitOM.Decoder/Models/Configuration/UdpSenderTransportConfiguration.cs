namespace RabbitOM.Decoder.Models.Configuration
{
	public sealed class UdpSenderTransportConfiguration : TransportConfiguration
	{
		public string? Address { get; set; }
		public int Port { get; set; }
		public TimeSpan RetryInterval { get; set; }
	}
}
