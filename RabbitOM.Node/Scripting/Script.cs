using System;
using System.Collections.Generic;

namespace RabbitOM.Node.Scripting
{
	public sealed class Script
	{
		public string Name { get; set; }

		public string Language { get; set; }

		public List<ScriptAssembly> Assemblies { get; set; }

		public List<ScriptProperty> Properties { get; set; }

		public string Code { get; set; }
	}
}
