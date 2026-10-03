using System;

namespace RabbitOM.Player.Scripting
{
	using RabbitOM.Player.Data;

	public interface IScriptRunner : IDisposable
	{
		bool IsRunning { get; }

		void Run( Script script );

		void Terminate();
	}
}
