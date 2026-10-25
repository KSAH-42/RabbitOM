using System;

namespace RabbitOM.Node.Scripting
{
	public static class MessageTypes
	{
		public const string CommunicationStarted = "COMMUNICATION/STARTED";
		public const string CommunicationStopped = "COMMUNICATION/STOPPED";
		public const string Connected = "COMMUNICATION/STATUS/CONNECTED";
		public const string Disconnected = "COMMUNICATION/STATUS/DISCONNECTED";
		public const string DataReceived = "COMMUNICATION/DATA/RECEIVED";
		public const string Error = "COMMUNICATION/ERROR";
	}
}
