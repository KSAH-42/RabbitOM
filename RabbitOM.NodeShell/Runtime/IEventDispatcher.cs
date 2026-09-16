using System;

namespace RabbitOM.NodeShell.Runtime
{
	public interface IEventDispatcher : IDisposable
	{
		void AddHandler( string eventType , string code );

		void DispatchEvent( string eventType );
	}
}
