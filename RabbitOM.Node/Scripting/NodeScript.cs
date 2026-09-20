using System;

namespace RabbitOM.Node.Scripting
{
	using RabbitOM.Node.Scripting.Messages;

	public abstract class NodeScript : IDisposable
	{
		~NodeScript()
		{
			Dispose( false );
		}

		public virtual void Setup() { }

		public abstract void Handle( Message mesage );

		public void Dispose()
		{
			Dispose( true );
			GC.SuppressFinalize( this );
		}

		protected virtual void Dispose( bool disposing )
		{
		}
	}
}
