using System;

namespace RabbitOM.Node.Scripting
{
	public struct NodeScriptRunnerLauncher : IDisposable
	{
		private readonly IScriptRunner _runner;

		public NodeScriptRunnerLauncher( IScriptRunner runner )
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
