using System;

namespace RabbitOM.Node.Scripting.Messages
{
	public sealed class DisconnectedMessage : Message
	{
		public DisconnectedMessage( object source )
			: base( source , MessageType.Disconnected )
		{
		}
	}
}
