using System;
using System.Collections.Generic;

namespace RabbitOM.NodeShell.Runtime
{
	public sealed class EventDispatcher : IEventDispatcher
	{
		private readonly ITaskExecutor _executor;
		private readonly Dictionary<string,string> _handlers;

		public EventDispatcher( ITaskExecutor executor )
		{
			_executor = executor ?? throw new ArgumentNullException( nameof( executor ) );
			_handlers = new Dictionary<string, string>();
		}

		public void AddHandler( string eventType , string code )
		{
			if ( string.IsNullOrEmpty( eventType ) )
			{
				throw new ArgumentNullException( nameof( eventType ) );
			}

			if ( string.IsNullOrEmpty( code ) )
			{
				throw new ArgumentNullException( nameof( code ) );
			}

			_handlers.Add( eventType , code );
		}

		public void DispatchEvent( string eventType )
		{
			if ( _handlers.TryGetValue( eventType ?? string.Empty , out var handler ) )
			{
				try
				{
					_executor.Execute( handler );
				}
				catch ( Exception ex )
				{
					System.Diagnostics.Debug.WriteLine( ex );
				}
			}
		}

		public void Dispose()
		{
			_executor.Dispose();
		}
	}
}
