namespace RabbitOM.Decoder.FFMpeg.Models
{
	public sealed class HttpStreamingConfigurationModel
	{
		public string? EndPoint { get; set; }
		public bool? UseCompression { get; set; }
		public TimeSpan? RetryInterval { get; set; }
	}
}
