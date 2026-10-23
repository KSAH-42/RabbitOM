namespace RabbitOM.Decoder.FFMpeg.Models
{
	public sealed class HttpSenderConfigurationModel : TransportConfigurationModel
	{
		public string? EndPoint { get; set; }
		public TimeSpan RetryInterval { get; set; }
	}
}
