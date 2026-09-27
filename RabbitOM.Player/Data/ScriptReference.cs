using System;

namespace RabbitOM.Player.Data
{
	public sealed class ScriptReference
	{
		public string Name { get; set; }

		public bool ForceLoad { get; set; }


		public static bool IsNullOrEmpty( ScriptReference reference )
		{
			return reference == null || string.IsNullOrWhiteSpace( reference.Name );
		}
	}
}
