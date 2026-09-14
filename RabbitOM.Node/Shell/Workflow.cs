using System;
using System.Collections.Generic;

namespace RabbitOM.Node.Shell
{
	public sealed class Workflow
	{
		public string Name { get; set; }

		public List<WorkflowHandler> Handlers { get; set; }
	}
}
