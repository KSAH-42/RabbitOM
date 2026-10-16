using System;

namespace RabbitOM.Node.Scripting
{
	public abstract class NodeScript : IDisposable
	{
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
