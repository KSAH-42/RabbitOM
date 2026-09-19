using System;
using System.Collections.Generic;

namespace RabbitOM.Node.Scripting
{
	public sealed class ScriptModel
	{
		public string Name { get; set; }

		public string Language { get; set; }

		public List<AssemblyModel> Assemblies { get; set; }

		public List<PropertyModel> Properties { get; set; }

		public string Code { get; set; }
	}
}
