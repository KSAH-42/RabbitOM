using System;

namespace RabbitOM.Node.Scripting
{
	public interface IScriptRunner : IDisposable
	{
		bool IsStarted { get; }

		void Start();

		void Stop();

		void PostEvent( object source , EventArgs e );
	}
}
