using System;

namespace RabbitOM.Player.Scripting
{
	public abstract class ApplicationScript : IDisposable
	{
		~ApplicationScript()
		{
			Dispose( false );
		}

		public MainWindow Window { get; private set; } = App.Current.MainWindow as MainWindow;

		public virtual void Setup() { }

		public void Dispose()
		{
			Dispose( true );
			GC.SuppressFinalize( this );
		}

		protected virtual void Dispose( bool disposing )
		{
			Window = null; // set as null to allow the garbage to collect and tell to it that no more reference are keeped/locked in someway 
		}
	}
}
