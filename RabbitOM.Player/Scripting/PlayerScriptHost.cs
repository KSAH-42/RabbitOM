using System;

namespace RabbitOM.Player.Scripting
{
	using RabbitOM.Threading;
	using RabbitOM.Player.Scripting.Messages;

	[Obsolete]
	public sealed class PlayerScriptHost : IDisposable
	{
		private readonly PlayerScript _script;
		private readonly BackgroundWorker _worker;
		private readonly CircularMessageQueue _messages;
		private volatile bool _disposed;


		public PlayerScriptHost( PlayerScript script )
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

			_worker.Start( PumpMessages );
		}

		public void Stop()
		{
			_worker.Stop();
			_messages.Clear();
		}

		public void Dispose()
		{
			_disposed = true;
			Stop();
			_script.Dispose();
		}

		public void PostMessage( Message message )
		{
			EnsureNotDisposed();

			_messages.Enqueue( message ?? throw new ArgumentNullException( nameof( message ) ) );
		}

		private void EnsureNotDisposed()
		{
			if ( _disposed )
			{
				throw new ObjectDisposedException( nameof(PlayerScriptHost) );
			}
		}

		private void PumpMessages()
		{
			_script.Setup();

			var count = 0;

			while ( count <= 1 )
			{
				count += CircularMessageQueue.Wait( _messages , _worker.ExitHandle ) ? 0 : 1;

				while ( _messages.TryDequeue( out var message ) )
				{
					_script.TryHandle( message );
				}
			}
		}
	}
}
