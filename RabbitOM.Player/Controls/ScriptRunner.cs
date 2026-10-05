using System;
using System.Runtime.Loader;
using System.Windows;

namespace RabbitOM.Player.Controls
{
	using RabbitOM.Player.Data;
	using RabbitOM.Player.Scripting;

	public sealed class ScriptRunner : IScriptRunner
	{
		static ScriptRunner()
		{
			App.Current.DispatcherUnhandledException += OnUnhandledException;
		}

		private AssemblyLoadContext _loadContext;
		private ApplicationScript _script;

		public bool IsRunning
		{
			get => _script != null;
		}

		public void Run( string script )
		{
			if ( string.IsNullOrWhiteSpace( script ) )
			{
				throw new ArgumentNullException( nameof( script ) );
			}

			if ( _script != null )
			{
				throw new InvalidOperationException();
			}

			_loadContext?.Unload();
			_loadContext = new AssemblyLoadContext( Guid.NewGuid().ToString() , true );

			try
			{
				var model = ScriptSerializer.Deserialize( script );
				var builder = new ApplicationScriptBuilder( model , _loadContext );

				_script = builder.LoadReferences().Build();

				var configurer = new ApplicationScriptConfigurer( _script );

				if ( model.Properties != null )
				{
					foreach( var property in model.Properties )
					{
						if ( property == null )
						{
							continue;
						}

						configurer.ConfigureProperty( property.Name , property.Value );
					}
				}

				_script.Setup();
			}
			catch( Exception )
			{
				Terminate();
				throw;
			}
		}

		public void Terminate()
		{
			if ( _script != null )
			{
				_script.Dispose();
				_script = null;
			}

			_loadContext?.Unload();
			_loadContext = null;

			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect(); // call again to fix unauthorizedexception for release files and write again on the generated assembly script
		}

		public void Dispose()
		{
			Terminate();
		}

		private static void OnUnhandledException( object sender , System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e )
		{
			e.Handled = true;

			MessageBox.Show( e.Exception?.ToString() ?? "unhandled exception" );
		}
	}
}
