using System;

namespace RabbitOM.Node.Shell
{
	public sealed class NullEventDispatcher : IEventDispatcher
	{
		private NullEventDispatcher(){ }

		public static NullEventDispatcher Instance { get; } = new NullEventDispatcher();

		public void DispatchEvent( string eventType )
		{
		}
	}
}
