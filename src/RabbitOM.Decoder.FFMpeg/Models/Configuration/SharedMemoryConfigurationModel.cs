namespace RabbitOM.Decoder.FFMpeg.Models
{
	public sealed class SharedMemoryConfigurationModel
	{
		public string? MappingName { get; set; } // use bool env-var for encryption
		public int? QueueCapacity { get; set; }
		public int? BlockSize { get; set; }
	}
}
