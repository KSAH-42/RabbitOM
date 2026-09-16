using System;

namespace RabbitOM.NodeShell.Runtime
{
	public static class EventNames
	{
		public const string CommunicationStartEvent = "on-communication-started";

		public const string CommunicationStopEvent = "on-communication-stopped";

		public const string ConnectedEvent = "on-connected";

		public const string DisconnectedEvent = "on-disconnected";

		public const string ErrorEvent = "on-error";
	}
}
