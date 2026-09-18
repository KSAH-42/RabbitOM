using System;
using System.IO;
using System.Linq;

namespace RabbitOM.Node.Services
{
	public sealed class ApplicationServiceBuilder
	{
		private ApplicationParameters _parameters;





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

		public IApplicationService Build()
		{
			return new ApplicationService( _parameters );
		}
	}
}
