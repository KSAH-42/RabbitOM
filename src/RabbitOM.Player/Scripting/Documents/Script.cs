using System;

namespace RabbitOM.Player.Scripting.Documents
{
	public sealed class Script
	{
		public string Language { get; set; }

		public List<ScriptReference> References { get; set; }

		public List<ScriptProperty> Properties { get; set; }

		public string Code { get; set; }
	}
}
