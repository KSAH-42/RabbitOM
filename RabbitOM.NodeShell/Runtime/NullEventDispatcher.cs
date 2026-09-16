using System;

namespace RabbitOM.NodeShell.Runtime
{
	public sealed class NullEventDispatcher : IEventDispatcher
	{
		private NullEventDispatcher(){ }

		public static NullEventDispatcher Instance { get; } = new NullEventDispatcher();

		public void DispatchEvent( string eventType )
		{
		}

		public void AddHandler( string eventType , string code )
		{
		}

		public void RemoveHandler( string eventType )
		{
		}

		public void RemoveHandlers()
		{
		}

		public void Dispose()
		{
		}
	}
}
