using System;

namespace RabbitOM.Player.Scripting
{
	public sealed record class Message ( string Type )
	{
		public object Payload { get; init; }
	}
}
