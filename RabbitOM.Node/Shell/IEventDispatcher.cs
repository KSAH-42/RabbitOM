using System;

namespace RabbitOM.Node.Shell
{
	public interface IEventDispatcher : IDisposable
	{
		void AddHandler( string eventType , string code );

		void RemoveHandler( string eventType );

		void RemoveHandlers();

		void DispatchEvent( string eventType );
	}
}
