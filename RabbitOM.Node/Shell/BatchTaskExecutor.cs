using System;
using System.Diagnostics;

namespace RabbitOM.Node.Shell
{
	public sealed class BatchTaskExecutor : ITaskExecutor
	{
		public void Execute( string input )
		{
			if ( string.IsNullOrEmpty( input ) )
			{
				return;
			}

			var entries = input.Split( new[] { '\r', '\n' } , StringSplitOptions.RemoveEmptyEntries );

			if ( entries.Length == 0 )
			{
				return;
			}

			using ( var process = new Process() )
			{
				process.StartInfo = new ProcessStartInfo { FileName = "cmd.exe", RedirectStandardInput = true, RedirectStandardOutput = false, RedirectStandardError = false, UseShellExecute = false, CreateNoWindow = true };
				process.Start();
				process.StandardInput.AutoFlush = true;

				foreach (var entry in entries)
				{
					if ( entry.Trim().Equals( "powershell" , StringComparison.OrdinalIgnoreCase ) )
					{
						continue;
					}

					process.StandardInput.WriteLine( entry );
				}

				process.StandardInput.WriteLine( "exit" );
				process.WaitForExit();
			}
		}
	}
}
