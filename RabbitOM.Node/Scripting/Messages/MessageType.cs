using System;

namespace RabbitOM.Node.Scripting.Messages
{
	public enum MessageType
	{
		CommunicationStarted,
		CommunicationStopped,
		Connected,
		Disconnected,
		PacketReceived,
		Error,
	}
}
