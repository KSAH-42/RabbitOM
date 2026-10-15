namespace RabbitOM.Player.Decoder.Models
{
	public class DecoderStatus
	{
		public bool IsStarted { get; set; }
		public bool IsConnected { get; set; }
		public string[]? Errors { get; set; }
		public Dictionary<string,string>? Metrics { get; set; }
	}
}
