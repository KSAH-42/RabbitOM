using System;
using System.Windows;

namespace RabbitOM.Player.Scripting
{
	public abstract class PlayerScript : IDisposable
	{
		public Window Window { get; } = App.Current.MainWindow;

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
