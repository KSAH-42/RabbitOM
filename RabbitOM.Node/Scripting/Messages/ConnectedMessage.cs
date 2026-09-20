using System;

namespace RabbitOM.Node.Scripting.Messages
{
	public sealed class ConnectedMessage : Message
	{
		public ConnectedMessage( object source )
			: base( source , MessageType.Connected )
		{
		}
	}
}
