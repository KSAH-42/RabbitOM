using System;

namespace RabbitOM.Node.Scripting
{
	using RabbitOM.Node.Scripting.Messages;

	public sealed class NullNodeScriptRunner : IScriptRunner
	{
		public readonly static NullNodeScriptRunner Instance = new NullNodeScriptRunner();

		private NullNodeScriptRunner() { }

		public bool IsStarted { get; }

		public void Start() { }

		public void Stop() { }

		public void PostMessage( Message message ) { }

		public void Dispose() { }
	}
}
