using System;

namespace RabbitOM.Node.Scripting.Messages
{
	public sealed class CommunicationStoppedMessage : Message
	{
		public CommunicationStoppedMessage( object source )
			: base( source , MessageType.CommunicationStopped )
		{
		}
	}
}
