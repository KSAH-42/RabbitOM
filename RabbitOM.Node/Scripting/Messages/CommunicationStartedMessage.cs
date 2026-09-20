using System;

namespace RabbitOM.Node.Scripting.Messages
{
	public sealed class CommunicationStartedMessage : Message
	{
		public CommunicationStartedMessage( object source )
			: base( source , MessageType.CommunicationStarted )
		{
		}
	}
}
