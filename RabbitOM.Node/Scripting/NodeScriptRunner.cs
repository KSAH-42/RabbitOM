using System;

namespace RabbitOM.Node.Scripting
{
	using RabbitOM.Collections;
	using RabbitOM.Threading;

	public sealed class NodeScriptRunner : IScriptRunner
	{
		private readonly NodeScript _script;
		private readonly BackgroundWorker _worker;
		private readonly CircualConcurrentQueue<NodeEvent> _events;
		private volatile bool _disposed;

		public NodeScriptRunner( NodeScript script )
		{
			_script = script ?? throw new ArgumentNullException( nameof( script ) );

			_worker = new BackgroundWorker( "Node script runner" );
			_events = new CircualConcurrentQueue<NodeEvent>();
		}

		public bool IsStarted
		{
			get => _worker.IsStarted;
		}

		public void Start()
		{
			EnsureNotDisposed();

			_worker.Start( DoEvents );
		}

		public void Stop()
		{
			_worker.Stop();
			_events.Clear();
		}

		public void PostEvent( object source , EventArgs e )
		{
			if ( source == null )
			{
				throw new ArgumentNullException( nameof( source ) );
			}

			if ( e == null )
			{
				throw new ArgumentNullException( nameof( e ) );
			}

			EnsureNotDisposed();

			_events.Enqueue( new NodeEvent( source , e ) );
		}

		public void Dispose()
		{
			_disposed = true;

			using ( _script )
			{
				Stop();
			}
		}

		private void EnsureNotDisposed()
		{
			if ( _disposed )
			{
				throw new ObjectDisposedException( nameof(NodeScriptRunner) );
			}
		}

		// TODO: refactor this code
		private void DoEvents()
		{
			_script.Setup();

			while ( CircualConcurrentQueue<NodeEvent>.Wait( _events , _worker.ExitHandle ) )
			{
				if ( _events.TryDequeue( out NodeEvent nodeEvent ) )
				{
					_script.TryHandle( nodeEvent.Sender , nodeEvent.EventArgs );
				}
			}

			while ( _events.TryDequeue( out NodeEvent nodeEvent ) )
			{
				_script.TryHandle( nodeEvent.Sender , nodeEvent.EventArgs );
			}
		}
	}
}
