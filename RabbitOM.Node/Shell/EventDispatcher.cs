using System;
using System.Collections.Generic;

namespace RabbitOM.Node.Shell
{
	public sealed class EventDispatcher : IEventDispatcher
	{
		private readonly ITaskExecutor _executor;
		private readonly Workflow _workflow;

		public EventDispatcher( ITaskExecutor executor , Workflow workflow )
		{
			_executor = executor ?? throw new ArgumentNullException( nameof( executor ) );
			_workflow = workflow ?? throw new ArgumentNullException( nameof( workflow ) );
		}

		public void DispatchEvent( string eventType )
		{
			if ( string.IsNullOrWhiteSpace( eventType ) )
			{
				throw new ArgumentNullException( nameof( eventType ) );
			}

			foreach ( var handler in _workflow.Handlers ?? new List<WorkflowHandler>() )
			{
				if ( handler == null || handler.Type != eventType )
				{
					continue;
				}

				try
				{
					_executor.Execute( handler.Code );
				}
				catch ( Exception ex )
				{
					OnException( ex );
				}
			}
		}

		public void Dispose()
		{
			try
			{
				_executor.Abort();
			}
			catch( Exception ex )
			{
				OnException( ex );
			}

			_executor.Dispose();
		}





		private void OnException( Exception ex )
		{
			System.Diagnostics.Debug.WriteLine( ex );
		}
	}
}
