using System;
using System.Threading;
using System.Windows;

namespace RabbitOM.Player
{
    public partial class App : System.Windows.Application
    {
        private readonly Mutex _mutex;

        public App()
        {
            _mutex = new Mutex( true , "RabbitOM.Player" );
        }

		protected override void OnStartup( StartupEventArgs e )
		{
			if ( ! _mutex.WaitOne( 1000 ) )
			{
				MessageBox.Show( "An instance is already running, the application will be closed. The main reason come from that script dll compile at runtime are actually share accros process, and can potentially become an access issue due that the mediaplayer share the same script file name. The next version will resolve this issue but it's not actually the main priority" );
				Shutdown();
				return;
			}

			base.OnStartup( e );
		}

		protected override void OnExit( ExitEventArgs e )
		{
			base.OnExit( e );
		}
    }
}
