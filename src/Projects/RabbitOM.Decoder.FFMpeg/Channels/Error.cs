namespace RabbitOM.Decoder.FFMpeg.Channels
{
	public sealed class Error
	{
		public Error( string type , string message )
		{
			Type = string.IsNullOrWhiteSpace( type )
				? throw new ArgumentNullException( nameof( type ) )
				: type;

			Message = message ?? string.Empty;
		}

		public string? Type { get; }

		public string? Message { get; }

		public override string ToString()
		{
			return $"{Type} {Message}";
		}
	}
}
