using System;

namespace RabbitOM.Player.Scripting
{
	public sealed class PlayerScriptHost : IDisposable
	{
		public PlayerScriptHost( IApplication application )
		{
			throw new NotImplementedException();
		}

		public bool IsStarted
		{
			get;
		}

		public void Start()
		{
		}

		public void Stop()
		{
		}

		public void Dispose()
		{
			Stop();
		}
	}
}
