using System;

namespace RabbitOM.Node.Logging
{
	public interface ILogger
	{
		bool IsEnabled { get; set; }

		void Debug(string message);

		void Debug(string format,params object[] args);

		void Info(string message);

		void Info(string format,params object[] args);

		void Warn(string message);

		void Warn(string format,params object[] args);

		void Error(string message);

		void Error(string format,params object[] args);

		void Critical(string message);

		void Critical(string format,params object[] args);
	}
}
