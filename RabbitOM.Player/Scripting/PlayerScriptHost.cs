using System;
using System.Runtime.Loader;

namespace RabbitOM.Player.Scripting
{
	public sealed class PlayerScriptHost : IDisposable
	{
		private readonly PlayerScript _script;
		private readonly AssemblyLoadContext _loadContext;



		public PlayerScriptHost( PlayerScript script , AssemblyLoadContext loadContext )
		{
			_script = script ?? throw new ArgumentNullException( nameof( script ) );
			_loadContext = loadContext ?? throw new ArgumentNullException( nameof( loadContext ) );
		}



		public bool IsStarted
		{
			get => throw new NotImplementedException();
		}

		public void Start()
		{
			throw new NotImplementedException();
		}

		public void Stop()
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			Stop();
		}

		public void PostMessage( Message message )
		{
			throw new NotImplementedException();
		}
	}
}
