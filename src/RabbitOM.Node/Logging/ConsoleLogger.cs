using System;

namespace RabbitOM.Node.Logging
{
	public sealed class ConsoleLogger : ILogger
	{
		private volatile bool _isEnabled = true;





		public bool IsEnabled
		{
			get => _isEnabled;
			set => _isEnabled = value;
		}






		public void Debug(string message)
			=> Log( ConsoleColor.Cyan , "DEBUG" , message );

		public void Debug(string format, params object[] args)
			=> Log( ConsoleColor.Cyan , "DEBUG" , format , args );

		public void Info(string message)
			=> Log( ConsoleColor.White , "INFO" , message );

		public void Info(string format, params object[] args)
			=> Log( ConsoleColor.White , "INFO" , format , args );

		public void Warn(string message)
			=> Log( ConsoleColor.Yellow , "WARN" , message );

		public void Warn(string format, params object[] args)
			=> Log( ConsoleColor.Yellow , "WARN" , format , args );

		public void Error(string message)
			=> Log( ConsoleColor.Red , "ERROR" , message );

		public void Error(string format, params object[] args)
			=> Log( ConsoleColor.Red , "ERROR" , format , args );

		public void Critical(string message)
			=> Log( ConsoleColor.DarkRed , "CRITICAL" , message );

		public void Critical(string format, params object[] args)
			=> Log( ConsoleColor.DarkRed , "CRITICAL" , format , args );






		private void Log( ConsoleColor color , string level , string message  )
		{
			if ( ! _isEnabled )
			{
				return;
			}

			Console.ForegroundColor = color;
			Console.WriteLine( "{0} - {1} - {2}" , DateTime.Now , level , message );
			Console.ResetColor();
		}

		private void Log( ConsoleColor color , string level , string format , params object[] args )
		{
			if ( ! _isEnabled )
			{
				return;
			}

			Console.ForegroundColor = color;
			Console.WriteLine( "{0} - {1} - {2}" , DateTime.Now , level , string.Format( format , args ) );
			Console.ResetColor();
		}
	}
}
