using System;
using System.ComponentModel.DataAnnotations;

namespace RabbitOM.Node.Scripting
{
	public static class ScriptValidator
	{
		public static void Validate( Script script )
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
