using System;

namespace RabbitOM.Player.Scripting
{
	public sealed class MessageChannel : IDisposable
	{
		private readonly object _lock = new object();
		private readonly Queue<Message> _messages = new Queue<Message>();
		private readonly EventWaitHandle _eventHandle = new EventWaitHandle( false , EventResetMode.ManualReset );
		private volatile bool _isDisposed;



		public void WriteMessage( Message message )
		{
			EnsureNotDisposed();

			lock ( _lock )
			{
				_messages.Enqueue( message ?? throw new ArgumentNullException( nameof( message ) ) );
				_eventHandle.Set();
			}
		}

		public bool TryReadMessage( out Message result )
		{
			result = null;

			EnsureNotDisposed();

			lock ( _lock )
			{
				try
				{
					if ( _messages.Count > 0 )
					{
						result = _messages.Dequeue();
					}

					return result != null;
				}
				finally
				{
					if ( _messages.Count > 0 )
					{
						_eventHandle.Set();
					}
					else
					{
						_eventHandle.Reset();
					}
				}
			}
		}

		public void ClearMessages()
		{
			EnsureNotDisposed();

			lock ( _lock )
			{
				_messages.Clear();
				_eventHandle.Reset();
			}
		}

		public void Dispose()
		{
			if ( _isDisposed )
			{
				return;
			}

			_isDisposed = true;

			lock ( _lock )
			{
				_eventHandle.Dispose();
				_messages.Clear();
			}
		}

		private void EnsureNotDisposed()
		{
			if ( _isDisposed )
			{
				throw new ObjectDisposedException( nameof( MessageChannel ) );
			}
		}

		public static bool Wait( MessageChannel channel , WaitHandle cancellationHandle )
		{
			if ( channel == null )
			{
				throw new ArgumentNullException( nameof( channel ) );
			}

			if ( cancellationHandle == null )
			{
				throw new ArgumentNullException( nameof( cancellationHandle ) );
			}

			channel.EnsureNotDisposed();

			return WaitHandle.WaitAny( new [] { cancellationHandle , channel._eventHandle } ) == 1;
		}
	}
}
