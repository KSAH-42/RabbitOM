using System;

namespace RabbitOM.Node.Scripting.Events
{
	public interface INodeEventHandler<in TNodeEvent>
		where TNodeEvent : NodeEvent
	{
		void ProcessEvent( TNodeEvent nodeEvent );
	}
}
