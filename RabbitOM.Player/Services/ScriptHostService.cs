using System;
using System.Runtime.Loader;

namespace RabbitOM.Player.Services
{
	using RabbitOM.Threading;
	using RabbitOM.Player.Data;
	using RabbitOM.Player.Scripting;

	public sealed class ScriptHostService : IDisposable
	{
		private readonly IApplication _application;
		private readonly BackgroundWorker _worker; // may be use the original thread class and set as foreground thread and allow to control threading appartement, it could be use fully if we manipulated COM objects see MTA, MTA, and neutral COM appartements 
		private readonly CircularMessageQueue _messages;
		private readonly AssemblyLoadContext _loadContext;
		private PlayerScript _script;
		private bool _disposed;	// remove volatile key because all service methode must call in the main thread






		public ScriptHostService( IApplication application )
			: this ( application, AssemblyLoadContext.Default )
		{
		}

		public ScriptHostService( IApplication application , AssemblyLoadContext loadContext )
		{
			_application = application ?? throw new ArgumentNullException( nameof( application ) );
			_loadContext = loadContext ?? throw new ArgumentNullException( nameof( loadContext ) );
			_messages = new CircularMessageQueue();
			_worker = new BackgroundWorker( "Scripting service" );
		}






		public bool IsScriptCreated
		{
			get => _script != null;
		}

		public bool IsScriptRunning
		{
			get => _worker.IsStarted;
		}







		public void CompileScript( string document )
		{
			EnsureNotDisposed();
			EnsureNotStarted();
			FreeScript();

			_script = new PlayerScriptBuilder( ScriptSerializer.Deserialize( document ) , _loadContext )
				.LoadReferences()
				.Build();
		}

		public void RunScript()
		{
			EnsureNotDisposed();
			EnsureNotStarted();
			EnsureScriptCreated();

			_worker.Start( PumpMessages );
		}

		public void TerminateScript()
		{
			_worker.Stop();	// TODO: add abort method in case the script code enter in infinite loop
		}

		public void Dispose()
		{
			TerminateScript();
			FreeScript();
			_disposed = true;
		}

		public void SendMessage( Message message )
		{
			EnsureNotDisposed();

			_messages.Enqueue( message ?? throw new ArgumentNullException( nameof( message ) ) );
		}





		private void EnsureScriptCreated()
		{
			if ( _script == null )
			{
				throw new InvalidOperationException( "the script must be created" );
			}
		}

		private void EnsureNotStarted()
		{
			if ( _worker.IsStarted )
			{
				throw new InvalidOperationException( "the worker must not stopped" );
			}
		}

		private void EnsureNotDisposed()
		{
			if ( _disposed )
			{
				throw new ObjectDisposedException( nameof(ScriptHostService) );
			}
		}

		private void FreeScript()
		{
			if ( _script != null )
			{
				_script.Dispose();
				_loadContext.Unload();
			}
		}






		// don't add a variable store the script reference
		// just to detected a bug and refactor in the right way

		private void PumpMessages()
		{
			System.Diagnostics.Debug.Assert( _script != null );

			if ( _script.Application == null )
			{
				_script.Application = new PlayerScriptApplication( _application );
				_script.Setup(); // called once in case of restart the script
			}

			var count = 0;

			while ( count <= 1 )
			{
				count += CircularMessageQueue.Wait( _messages , _worker.ExitHandle ) ? 0 : 1;

				while ( _messages.TryDequeue( out var message ) )
				{
					_script.TryHandle( message );
				}
			}
		}
	}
}
