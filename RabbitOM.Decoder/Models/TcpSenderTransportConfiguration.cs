namespace RabbitOM.Decoder.Models
{
	public sealed class TcpSenderTransportConfiguration : TransportConfiguration
	{
		public string? Address { get; set; }
		public int Port { get; set; }
		public TimeSpan RetryInterval { get; set; }
	}
}
