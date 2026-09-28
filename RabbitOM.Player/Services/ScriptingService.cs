using System;

namespace RabbitOM.Player.Services
{
	using RabbitOM.Player.Scripting;
	using RabbitOM.Player.Scripting.Messages;

	public sealed class ScriptingService : IDisposable
	{
		private readonly IApplication _application;



		public ScriptingService( IApplication application )
		{
			_application = application ?? throw new ArgumentNullException( nameof( application ) );
		}



		public bool IsScriptCreated { get; }

		public bool IsScriptRunning { get; }



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
