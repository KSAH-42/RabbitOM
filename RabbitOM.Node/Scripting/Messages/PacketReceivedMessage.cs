using System;

namespace RabbitOM.Node.Scripting.Messages
{
	public sealed class PacketReceivedMessage : Message
	{
		public PacketReceivedMessage( object source , byte[] data )
			: base( source , MessageType.PacketReceived )
		{
			Data = data ?? throw new ArgumentNullException( nameof( data ) );
		}

		public byte[] Data { get; }
	}
}
