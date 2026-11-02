namespace RabbitOM.Decoder.FFMpeg.Presentation.Dtos
{
	public sealed class SharedMemoryTransportConfiguration
	{
		public string? MappingName { get; set; } // use bool env-var for encryption

		public int? QueueCapacity { get; set; }

		public int? BlockSize { get; set; }
	}
}
