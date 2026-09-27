using System;

namespace RabbitOM.Player.Scripting
{
	using RabbitOM.Threading;
	using RabbitOM.Player.Scripting.Messages;

	// TODO: try to refactor and remove the CircularMessageQueue
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
			if ( _disposed )
			{
				throw new ObjectDisposedException( nameof(PlayerScriptHost) );
			}

			_worker.Start( PumpMessages );
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

			if ( _disposed )
			{
				throw new ObjectDisposedException( nameof(PlayerScriptHost) );
			}

			if ( ! _worker.IsStarted || _worker.IsStopping )
			{
				throw new InvalidOperationException( nameof(PlayerScriptHost) );
			}

			_messages.Enqueue( message );
		}

		public void Dispose()
		{
			_disposed = true;
			Stop();
			_script.Dispose();
		}





		private void PumpMessages()
		{
			_script.Setup();

			while ( CircularMessageQueue.Wait( _messages , _worker.ExitHandle ) )
			{
				if ( _messages.TryDequeue( out var message ) )
				{
					_script.TryHandle( message );
				}
			}
		}
	}
}
