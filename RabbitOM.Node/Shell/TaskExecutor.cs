using System;

namespace RabbitOM.Node.Shell
{
	public abstract class TaskExecutor : ITaskExecutor
	{
		~TaskExecutor()
		{
			Dispose( false );
		}

		public abstract void Execute( string input );

		public virtual void Abort()
		{
		}

		public void Dispose()
		{
			Dispose( true );
			GC.SuppressFinalize( this );
		}

		protected virtual void Dispose( bool dispose )
		{
		}
	}
}
