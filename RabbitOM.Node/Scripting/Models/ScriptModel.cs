using System;
using System.Collections.Generic;

namespace RabbitOM.Node.Scripting.Models
{
	public sealed class ScriptModel
	{
		public string Name { get; set; }

		public string Language { get; set; }

		public List<ScriptAssemblyModel> Assemblies { get; } = new List<ScriptAssemblyModel>();

		public List<ScriptPropertyModel> Properties { get; } = new List<ScriptPropertyModel>();

		public string Code { get; set; }
	}
}
