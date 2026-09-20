using System;

namespace RabbitOM.Node.Scripting
{
	using RabbitOM.Threading;
	using RabbitOM.Node.Scripting.Messages;

	public sealed class NodeScriptRunner : IScriptRunner
	{
		private readonly NodeScript _script;
		private readonly BackgroundWorker _worker;
		private readonly CircularMessageQueue _messages;
		private volatile bool _disposed;

		public NodeScriptRunner( NodeScript script )
		{
			_script = script ?? throw new ArgumentNullException( nameof( script ) );

			_worker = new BackgroundWorker( "Node script runner" );
			_messages = new CircularMessageQueue();
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
			_messages.Clear();
		}

		public void PostMessage( Message message )
		{
			if ( message == null )
			{
				throw new ArgumentNullException( nameof( message ) );
			}

			EnsureNotDisposed();

			_messages.Enqueue( message );
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

		private void DoEvents()
		{
			_script.Setup();

			while ( CircularMessageQueue.Wait( _messages , _worker.ExitHandle ) )
			{
				if ( _messages.TryDequeue( out var message ) )
				{
					_script.TryHandle( message );
				}
			}

			while ( _messages.TryDequeue( out var message ) )
			{
				_script.TryHandle( message );
			}
		}
	}
}
