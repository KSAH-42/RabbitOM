using System;

namespace RabbitOM.Node.Shell
{
	public interface IEventDispatcher : IDisposable
	{
		void DispatchEvent( string eventType );
	}
}
