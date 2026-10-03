using System;

namespace RabbitOM.Player.Services
{
	public sealed class ScriptService : IDisposable
	{
		public string Script { get; private set; }

		public void RunScript()
		{
		}

		public void StopScript()
		{
		}

		public void ExportScript()
		{
			throw new NotImplementedException();
		}

		public void ImportScript()
		{
			throw new NotImplementedException();
		}

		public void ClearScript()
		{
			Script = string.Empty;
		}

		public void ResetScript()
		{
			Script = ResourceService.GetResourceFile( ResourceService.ScriptTemplate );
		}

		public void Dispose()
		{
		}
	}
}
