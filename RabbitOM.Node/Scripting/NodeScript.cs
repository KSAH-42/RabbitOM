using System;

namespace RabbitOM.Node.Scripting
{
	public abstract class NodeScript : IDisposable
	{
		~NodeScript()
		{
			Dispose( false );
		}

		public abstract void Handle( EventContext context );

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
