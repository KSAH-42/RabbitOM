using System;

namespace RabbitOM.Node.Scripting
{
	public struct NodeScriptHostLauncher : IDisposable
	{
		private readonly IScriptHost _runner;

		public NodeScriptHostLauncher( IScriptHost runner )
		{
			_runner = runner ?? throw new ArgumentNullException( nameof( runner ) );

			_runner.Start();
		}

		public void Dispose()
		{
			_runner.Dispose();
		}
	}
}
