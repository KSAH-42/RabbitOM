using System;

namespace RabbitOM.Player.Controls
{
	public interface IScriptRunner : IDisposable
	{
		bool IsRunning { get; }

		void Run( string script );

		void Terminate();
	}
}
