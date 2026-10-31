using System;
using System.Runtime.Loader;
using System.Windows;

namespace RabbitOM.Player.Controls
{
	using RabbitOM.Player.Data;
	using RabbitOM.Player.Scripting;

	public sealed class ScriptRunner : IScriptRunner
	{
		private AssemblyLoadContext _loadContext;
		private ApplicationScript _script;




		static ScriptRunner()
		{
			App.Current.DispatcherUnhandledException += OnUnhandledException;
		}




		public bool IsRunning
		{
			get => _script != null;
		}





		private static void OnUnhandledException( object sender , System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e )
		{
			e.Handled = true;

			MessageBox.Show( e.Exception?.ToString() ?? "unhandled exception" );
		}





		public void Run( string script )
		{
			if ( _script != null )
			{
				throw new InvalidOperationException( "the instance of the script is not null" );
			}

			if ( _loadContext != null )
			{
				throw new InvalidOperationException( "the instance of the loadContext is not null" );
			}

			_loadContext = new AssemblyLoadContext( "RabbitOM.MediaPlayer.Scripting" , true );

			try
			{
				var model = ScriptSerializer.Deserialize( script );

				_script = new ApplicationScriptBuilder( model , _loadContext )
					.ClearOutputDirectory()
					.LoadReferences()
					.Build();

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

			if ( _loadContext != null )
			{
				_loadContext.Unload();
				_loadContext = null;
			}
		}

		public void Dispose()
		{
			Terminate();
		}
	}
}
