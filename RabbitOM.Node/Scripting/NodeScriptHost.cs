using System;

namespace RabbitOM.Node.Scripting
{
	using RabbitOM.Threading;

	public sealed class NodeScriptHost : IScriptHost
	{
		private readonly NodeScript _script;
		private readonly BackgroundWorker _worker;
		private readonly MessageChannel _messages;
		private volatile bool _disposed;

		public NodeScriptHost( NodeScript script )
		{
			_script = script ?? throw new ArgumentNullException( nameof( script ) );

			_worker = new BackgroundWorker( "Node script runner" );
			_messages = new MessageChannel();
		}

		public bool IsStarted
		{
			get => _worker.IsStarted;
		}

		public void Start()
		{
			EnsureNotDisposed();

			_worker.Start( PumpMessages );
		}

		public void Stop()
		{
			_worker.Stop();
			_messages.ClearMessages();
		}

		public void Dispose()
		{
			_disposed = true;
			Stop();
			_script.Dispose();
			_messages.Dispose();
		}

		public void PostMessage( Message message )
		{
			EnsureNotDisposed();

			_messages.WriteMessage( message );
		}

		private void EnsureNotDisposed()
		{
			if ( _disposed )
			{
				throw new ObjectDisposedException( nameof(NodeScriptHost) );
			}
		}

		private void PumpMessages()
		{
			_script.Setup();

			var count = 0;

			while ( count <= 1 )
			{
				count += MessageChannel.Wait( _messages , _worker.ExitHandle ) ? 0 : 1;

				while ( _messages.TryReadMessage( out var message ) )
				{
					_script.TryHandle( message );
				}
			}
		}
	}
}
