using System;

namespace RabbitOM.Node.Shell
{
	public interface IEventDispatcher
	{
		void DispatchEvent( string eventType );
	}
}
