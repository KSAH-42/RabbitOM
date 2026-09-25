using System;
using System.Windows.Threading;

namespace RabbitOM.Player.Scripting
{
	public sealed class PlayerScriptApplication 
	{
		private readonly IApplication _application;
		private readonly Dispatcher _dispatcher;

		public PlayerScriptApplication( IApplication application )
		{
			_application = application ?? throw new ArgumentNullException( nameof( application ) );
			_dispatcher = Dispatcher.CurrentDispatcher;
		}

		public void StartStreaming()
		{
			_dispatcher.BeginInvoke( DispatcherPriority.Normal , () => _application.StartStreaming() );
		}

		public void StopStreaming()
		{
			_dispatcher.BeginInvoke( DispatcherPriority.Normal , () => _application.StopStreaming() );
		}
	}
}
