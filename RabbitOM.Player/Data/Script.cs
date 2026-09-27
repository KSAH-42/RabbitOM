using System;
using System.ComponentModel.DataAnnotations;

namespace RabbitOM.Player.Data
{
	public sealed class Script
	{
		public string Language { get; set; }

		public List<ScriptReference> References { get; set; }

		public List<ScriptProperty> Properties { get; set; }

		public string Code { get; set; }




		public static void Validate( Script script )
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

			if ( script?.References.Any( ScriptReference.IsNullOrEmpty ) == true )
		    {
				throw new ValidationException( "Contains null or empty reference" );
			}

			if ( script?.Properties.Any( ScriptProperty.IsNullOrEmpty ) == true )
		    {
				throw new ValidationException( "Contains null or empty property" );
			}
		}
	}
}
