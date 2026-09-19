using System;
using System.IO;
using System.Linq;

namespace RabbitOM.NodeShell.Services
{
	using RabbitOM.NodeShell.Runtime;

	public sealed class ApplicationServiceBuilder
	{
		private ApplicationParameters _parameters;

		private IEventDispatcher _dispatcher;





		public ApplicationServiceBuilder SetParameters( string[] parameters )
		{
			if ( _parameters != null )
			{
				throw new InvalidOperationException( "the parameters has been already set" );
			}

			var applicationParameters = ApplicationParameters.Parse( parameters );
			applicationParameters.Validate();
			_parameters = applicationParameters;

			return this;
		}

		public ApplicationServiceBuilder SetupEventDispatcher()
		{
			if ( _parameters == null )
			{
				throw new InvalidOperationException( "Parameters must be set be call this method" );
			}

			if ( _dispatcher != null )
			{
				throw new InvalidOperationException( "The event dispatcher is already setup" );
			}

			if ( string.IsNullOrWhiteSpace( _parameters.ScriptWorkflow ) )
			{
				return this;
			}

			var script = File.Exists( _parameters.ScriptWorkflow ) ? File.ReadAllText( _parameters.ScriptWorkflow ) : _parameters.ScriptWorkflow;

			var workflow = WorkflowDeserializer.Deserialize( script );

			_dispatcher = new EventDispatcher( new BatchTaskExecutor() );

			foreach ( var handler in workflow.Handlers ?? Enumerable.Empty<WorkflowHandler>() )
			{
				if ( handler != null )
				{
					_dispatcher.AddHandler( handler.Type , handler.Code );
				}
			}

			return this;
		}

		public IApplicationService Build()
		{
			return new ApplicationService( _dispatcher ?? NullEventDispatcher.Instance , _parameters.Uri );
		}
	}
}
