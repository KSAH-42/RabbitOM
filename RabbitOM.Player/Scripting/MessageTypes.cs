using System;

namespace RabbitOM.Player.Scripting
{
	public static class MessageTypes
	{
		public const string EventPlayerStarted = "EVENT/PLAYER/STARTED";

		public const string EventPlayerStopped = "EVENT/PLAYER/STOPPED";

		public const string EventPlayerConnected = "EVENT/PLAYER/CONNECTED";

		public const string EventPlayerDisconnected = "EVENT/PLAYER/DISCONNECTED";

		public const string ActionStartPlayer = "ACTION/PLAYER/START";

		public const string ActionStopPlayer = "ACTION/PLAYER/STOP";
	}
}
