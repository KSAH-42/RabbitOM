using System;

namespace RabbitOM.Player.Scripting
{
	public abstract class ApplicationScript : IDisposable
	{
		~ApplicationScript()
		{
			Dispose( false );
		}

		// TODO: replace by a kind of application classe or service app class 
		// the main is accessible
		public MainWindow Window { get; private set; } = App.Current.MainWindow as MainWindow;

		// TODO: for the next we will replace it by a client at connected when the backend using signal r will be ready, it just temp server for validating some basic ideas or may by we can keep that and expose a different baseScript class , and then we it's ready we smooth transition
		public ScriptServer Server { get; } = new ScriptServer();

		public virtual void Setup() { }

		public void Dispose()
		{
			Dispose( true );
			GC.SuppressFinalize( this );
		}

		protected virtual void Dispose( bool disposing )
		{
			Window = null; // set as null to allow the garbage to collect and tell to it that no more reference are keeped/locked in someway 
			Server.Dispose();
		}
	}
}
