using System;

namespace RabbitOM.Node.Scripting
{
	public interface IScriptHost : IDisposable
	{
		bool IsStarted { get; }

		void Start();

		void Stop();

		void PostMessage( Message message );
	}
}
