using System;

namespace RabbitOM.Node.Scripting.Messages
{
	public sealed class ErrorMessage : Message
	{
		public ErrorMessage( object source , string text )
			: base( source , MessageType.Error )
		{
			Text = text;
		}

		public string Text { get; }
	}
}
