using System;

namespace RabbitOM.Node.Scripting
{
	using RabbitOM.Node.Scripting.Messages;

	public interface IScriptRunner : IDisposable
	{
		bool IsStarted { get; }

		void Start();

		void Stop();

		void PostMessage( Message message );
	}
}
