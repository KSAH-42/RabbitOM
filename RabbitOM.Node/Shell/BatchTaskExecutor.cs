using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RabbitOM.Node.Shell
{
	public sealed class BatchTaskExecutor : TaskExecutor
	{
		public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(3);

		public override void Execute( string input )
		{
			if ( string.IsNullOrWhiteSpace( input ) )
			{
				throw new ArgumentNullException( nameof( input ) );
			}

			var entries = input.Split( new[] { '\r', '\n' } , StringSplitOptions.RemoveEmptyEntries );

			using ( var process = new Process() )
			{
				process.StartInfo = new ProcessStartInfo {
					FileName = "cmd.exe",
					RedirectStandardInput = true,
					RedirectStandardOutput = false,
					RedirectStandardError = false,
					UseShellExecute = false,
					CreateNoWindow = true
				};

				process.Start();
				process.StandardInput.AutoFlush = true;

				var postEvents = new Queue<string>();

				postEvents.Enqueue( "exit" );

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
