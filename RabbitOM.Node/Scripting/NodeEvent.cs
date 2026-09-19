using System;

namespace RabbitOM.Node.Scripting
{
	public sealed class NodeEvent
	{
		public NodeEvent( object sender , EventArgs e )
		{
			Sender = sender ?? throw new ArgumentNullException( nameof( sender ) );
			EventArgs = e ?? throw new ArgumentNullException( nameof( e ) );
		}

		public object Sender { get; }

		public EventArgs EventArgs { get; }
	}
}
