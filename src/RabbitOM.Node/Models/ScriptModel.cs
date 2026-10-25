using System;
using System.Collections.Generic;

namespace RabbitOM.Node.Models
{
	public sealed class ScriptModel
	{
		public string Language { get; set; }

		public List<ScriptReferenceModel> References { get; } = new List<ScriptReferenceModel>();

		public List<ScriptPropertyModel> Properties { get; } = new List<ScriptPropertyModel>();

		public string Code { get; set; }
	}
}
