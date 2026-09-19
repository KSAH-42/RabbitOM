using System;

namespace RabbitOM.Node.Scripting
{
	public sealed class EventContext
	{
		public EventContext( object source , EventArgs eventArgs )
		{
			Source = source ?? throw new ArgumentNullException( nameof( source ) );
			Event = eventArgs ?? throw new ArgumentNullException( nameof( eventArgs ) );
		}

		public object Source { get; }

		public EventArgs Event { get; }
	}
}
