using System;

namespace RabbitOM.Node.Scripting
{
	public sealed record class Message( string Type , object Source )
	{
		public object Payload { get; init; }
	}
}
