using System;

namespace RabbitOM.Node.Models
{
	public sealed class ScriptReferenceModel
	{
		public string Name { get; set; }

		public bool ForceLoad { get; set; }

		public static bool IsNullOrEmpty( ScriptReferenceModel reference )
		{
			return reference == null || string.IsNullOrWhiteSpace( reference.Name );
		}
	}
}
