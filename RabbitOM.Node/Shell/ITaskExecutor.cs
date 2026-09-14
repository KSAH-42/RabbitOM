using System;

namespace RabbitOM.Node.Shell
{
	public interface ITaskExecutor
	{
		void Execute( string input );
	}
}
