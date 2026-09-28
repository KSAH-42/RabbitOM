using System;

namespace RabbitOM.Player.Services
{
	using RabbitOM.Player.Scripting;
	using RabbitOM.Player.Scripting.Messages;

	public sealed class ScriptingService : IDisposable
	{
		public bool IsScriptRunning { get; }

		public bool IsScriptCompiled { get; }

		public void CompileScript( string document )
		{
			throw new NotImplementedException();
		}

		public void RunScript()
		{
			throw new NotImplementedException();
		}

		public void TerminateScript()
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public void SendMessage( Message message )
		{
			throw new NotImplementedException();
		}
	}
}
