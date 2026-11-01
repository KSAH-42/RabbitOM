using System;

namespace RabbitOM.Player.Scripting.Documents
{
	public sealed class ScriptProperty
	{
		public string Name { get; set; }

		public string Value { get; set; }

		public static bool IsNullOrEmpty( ScriptProperty property )
		{
			return property == null || string.IsNullOrWhiteSpace( property.Name );
		}
	}
}
