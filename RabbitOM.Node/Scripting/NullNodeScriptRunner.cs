using System;

namespace RabbitOM.Node.Scripting
{
	public sealed class NullNodeScriptRunner : IScriptRunner
	{
		public readonly static NullNodeScriptRunner Instance = new NullNodeScriptRunner();

		private NullNodeScriptRunner() { }

		public bool IsStarted { get; }

		public void Start() { }

		public void Stop() { }

		public void PostEvent( object source , EventArgs e ) { }

		public void Dispose() { }
	}
}
