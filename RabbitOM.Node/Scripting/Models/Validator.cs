using System;
using System.ComponentModel.DataAnnotations;

namespace RabbitOM.Node.Scripting.Models
{
	public static class Validator
	{
		// a very simple validator here, but no remote validation here
		// done by an authority to validate the script remotely and check
		// dependencies, and even instanciated with mocks into a sandbox and look how it's behave and also to observed it's i/o

		public static void Validate( ScriptModel script )
		{
			if ( script == null )
			{
				throw new ArgumentNullException( nameof( script ) );
			}

			if ( string.IsNullOrWhiteSpace( script.Name ) )
			{
				throw new ValidationException( "A name must be provided" );
			}

			if ( string.IsNullOrWhiteSpace( script.Language ) )
			{
				throw new ValidationException( "The language must be specified" );
			}

			if ( string.IsNullOrWhiteSpace( script.Code ) )
			{
				throw new ValidationException( "The code must be provided" );
			}
		}
	}
}
