using System;

namespace RabbitOM.NodeShell.Runtime
{
	public interface ITaskExecutor	: IDisposable
	{
		void Execute( string input );
	}
}
