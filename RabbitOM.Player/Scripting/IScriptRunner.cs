using System;

namespace RabbitOM.Player.Scripting
{
	public interface IScriptRunner : IDisposable
	{
		bool IsRunning { get; }

		void Run( string document );

		void Terminate();
	}
}
