using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RabbitOM.Node.Models
{
	public static class ScriptValidator
	{
		public static void Validate( ScriptModel script )
		{
			if ( script == null )
			{
				throw new ArgumentNullException( nameof( script ) );
			}

			if ( string.IsNullOrWhiteSpace( script.Language ) )
			{
				throw new ValidationException( "The language must be specified" );
			}

			if ( string.IsNullOrWhiteSpace( script.Code ) )
			{
				throw new ValidationException( "The code must be provided" );
			}

			if ( script?.References.Any( ScriptReferenceModel.IsNullOrEmpty ) == true )
		    {
				throw new ValidationException( "Contains null or empty reference" );
			}

			if ( script?.Properties.Any( ScriptPropertyModel.IsNullOrEmpty ) == true )
		    {
				throw new ValidationException( "Contains null or empty property" );
			}
		}
	}
}
