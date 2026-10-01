using System;
using System.Windows.Threading;

namespace RabbitOM.Player.Scripting
{
	public sealed class ApplicationProxy
	{
		private readonly IApplication _application;
		private readonly Dispatcher _dispatcher;

		public ApplicationProxy( IApplication application )
		{
			_application = application ?? throw new ArgumentNullException( nameof( application ) );
			_dispatcher = application.Window.Dispatcher;
		}

		public void StartStreaming()
		{
			_dispatcher.BeginInvoke( DispatcherPriority.Normal , () => _application.StartStreaming() );
		}

		public void StartStreaming( string uri )
		{
			_dispatcher.BeginInvoke( DispatcherPriority.Normal , () => _application.StartStreaming( uri ) );
		}

		public void StopStreaming()
		{
			_dispatcher.BeginInvoke( DispatcherPriority.Normal , () => _application.StopStreaming() );
		}
	}
}
