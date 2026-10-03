using System;
using System.Windows;

namespace RabbitOM.Player.Scripting
{
	public abstract class PlayerScript : IDisposable
	{
		public Window Window { get; } = App.Current.MainWindow;

		public virtual void Setup() { } // call after properties has been apply

		public void Dispose()
		{
			Dispose( true );
			GC.SuppressFinalize( this );
		}

		protected virtual void Dispose( bool disposing )
		{
		}
	}
}
