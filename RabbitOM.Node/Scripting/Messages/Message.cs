using System;

namespace RabbitOM.Node.Scripting.Messages
{
	public abstract class Message
	{
		protected Message( object source , MessageType type )
		{
			Source = source ?? throw new ArgumentNullException( nameof( source ) );

			Type = type;
		}



		public object Source { get; }

		public MessageType Type { get; }
	}
}
