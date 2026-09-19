using System;

namespace RabbitOM.NodeShell.Runtime
{
	using RabbitOM.Collections;
	using RabbitOM.Threading;

	public sealed class EventManager : IDisposable
	{
		private readonly IEventDispatcher _dispatcher;
		private readonly CircualConcurrentQueue<string> _events;
		private readonly BackgroundWorker _worker;
		private volatile bool _isDisposed;

		public EventManager( IEventDispatcher dispatcher )
		{
			_dispatcher = dispatcher ?? throw new ArgumentNullException( nameof( dispatcher ) );
			_events = new CircualConcurrentQueue<string>();
			_worker = new BackgroundWorker( "EventManager" );
			_worker.Start( PumpEvents );
		}

		public void Dispose()
		{
			_isDisposed = true;
			_worker.Stop();
			_events.Clear();
			_dispatcher.Dispose();
		}

		public void PostEvent( string eventType )
		{
			if ( _isDisposed )
			{
				throw new ObjectDisposedException( nameof(EventManager) );
			}

			_events.Enqueue( eventType );
		}

		private void PumpEvents()
		{
			while ( CircualConcurrentQueue<string>.Wait( _events , _worker.ExitHandle ) )
			{
				if ( _events.TryDequeue( out var evt ) )
				{
					_dispatcher.DispatchEvent( evt );
				}
			}

			while ( _events.TryDequeue( out var evt ) )
			{
				_dispatcher.DispatchEvent( evt );
			}
		}
	}
}
