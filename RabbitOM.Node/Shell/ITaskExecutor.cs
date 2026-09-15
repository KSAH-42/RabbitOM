using System;

namespace RabbitOM.Node.Shell
{
	public interface ITaskExecutor	: IDisposable
	{
		void Execute( string input );
	}
}
