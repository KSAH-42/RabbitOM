using System;
using System.Collections.Generic;

namespace RabbitOM.Player.Scripting.Models
{
	public sealed class ScriptModel
	{
		public string Name { get; set; }

		public string Language { get; set; }

		public List<ReferenceModel> References { get; } = new List<ReferenceModel>();

		public List<PropertyModel> Properties { get; } = new List<PropertyModel>();

		public string Code { get; set; }
	}
}
