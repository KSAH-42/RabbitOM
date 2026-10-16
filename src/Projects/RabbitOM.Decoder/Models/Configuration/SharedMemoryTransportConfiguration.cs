namespace RabbitOM.Decoder.Models.Configuration
{
	public sealed class SharedMemoryTransportConfiguration : TransportConfiguration
	{
		public string? MappingName { get; set; } // use bool env-var for encryption
		public int QueueCapacity { get; set; }
		public int BlockSize { get; set; }
	}
}
