using System;
using System.Linq;

namespace RabbitOM.Node.Shell
{
	public sealed class EventDispatcher : IEventDispatcher
	{
		private readonly Workflow _workflow;

		private readonly ITaskExecutor _executor;




		public EventDispatcher( Workflow workflow , ITaskExecutor executor )
		{
			_workflow = workflow ?? throw new ArgumentNullException( nameof( workflow ) );

			_executor = executor ?? throw new ArgumentNullException( nameof( executor ) );
		}



		// TODO: do we need to do the same thing like the rtsp event dispatcher, actually no

		public void DispatchEvent( string eventType )
		{
			if ( eventType == null || eventType.IndexOf( ' ' ) >= 0 )
			{
				throw new ArgumentNullException( nameof( eventType ) );
			}

			foreach ( var handler in _workflow.Handlers ?? Enumerable.Empty<WorkflowHandler>() )
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
					System.Diagnostics.Debug.WriteLine( ex );
				}
			}
		}
	}
}
