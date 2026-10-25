using System;

namespace RabbitOM.Node.Models
{
	public sealed class ScriptPropertyModel
	{
		public string Name { get; set; }

		public string Value { get; set; }

		public static bool IsNullOrEmpty( ScriptPropertyModel property )
		{
			return property == null || string.IsNullOrWhiteSpace( property.Name );
		}
	}
}
