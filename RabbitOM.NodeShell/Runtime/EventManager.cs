using System;

namespace RabbitOM.NodeShell.Runtime
{
	using RabbitOM.Collections;
	using RabbitOM.Threading;

	public sealed class EventManager : IDisposable
	{
		private readonly IEventDispatcher _dispatcher;
		private readonly CircualConcurrentQueue<Action> _actions;
		private readonly BackgroundWorker _worker;
		private volatile bool _isDisposed;

		public EventManager( IEventDispatcher dispatcher )
		{
			_dispatcher = dispatcher ?? throw new ArgumentNullException( nameof( dispatcher ) );
			_actions = new CircualConcurrentQueue<Action>();
			_worker = new BackgroundWorker( "EventManager" );
			_worker.Start( PumpEvents );
		}

		public void Dispose()
		{
			_isDisposed = true;
			_worker.Stop();
			_actions.Clear();
			_dispatcher.Dispose();
		}

		public void PostEvent( string eventType )
		{
			if ( _isDisposed )
			{
				throw new ObjectDisposedException( nameof(EventManager) );
			}

			_actions.Enqueue( () => _dispatcher.DispatchEvent( eventType ) );
		}

		private void PumpEvents()
		{
			while ( CircualConcurrentQueue<Action>.Wait( _actions , _worker.ExitHandle ) )
			{
				if ( _actions.TryDequeue( out Action action ) )
				{
					action.Invoke();
				}
			}

			while ( _actions.TryDequeue( out Action action ) )
			{
				action.Invoke();
			}
		}
	}
}
