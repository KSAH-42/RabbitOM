namespace RabbitOM.Decoder.FFMpeg.Data.Dtos.Configuration
{
	public sealed class SharedMemoryTransportConfiguration
	{
		public string? MappingName { get; set; } // use bool env-var for encryption

		public int? QueueCapacity { get; set; }

		public int? BlockSize { get; set; }
	}
}
