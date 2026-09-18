using System;
using System.Collections.Generic;

namespace RabbitOM.Node.Scripting
{
	public sealed class Script
	{
		public string Name { get; set; }

		public string Language { get; set; }

		public List<AssemblyFile> Assemblies { get; set; }

		public string Code { get; set; }
	}
}
