using System;

namespace RabbitOM.Node.Scripting
{
	public sealed class NullNodeScriptHost : IScriptHost
	{
		public readonly static NullNodeScriptHost Instance = new NullNodeScriptHost();

		private NullNodeScriptHost() { }

		public bool IsStarted { get; }

		public void Start() { }

		public void Stop() { }

		public void PostMessage( Message message ) { }

		public void Dispose() { }
	}
}
