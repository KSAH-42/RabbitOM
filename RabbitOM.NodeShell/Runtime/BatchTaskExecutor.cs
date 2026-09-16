using System;
using System.Diagnostics;

namespace RabbitOM.NodeShell.Runtime
{
	public sealed class BatchTaskExecutor : ITaskExecutor
	{
		private readonly Process _process;

		public BatchTaskExecutor()
		{
			_process = new Process() { StartInfo = new ProcessStartInfo { FileName = "cmd.exe", RedirectStandardInput = true, RedirectStandardOutput = false, RedirectStandardError = false, UseShellExecute = false, CreateNoWindow = true } };
			_process.Start();
			_process.StandardInput.AutoFlush = true;
		}

		public TimeSpan WaitTimeout { get; set; } = TimeSpan.FromSeconds(5);

		public void Execute( string input )
		{
			var entries = input?.Split( new[] { '\r', '\n' } , StringSplitOptions.RemoveEmptyEntries ) ?? Array.Empty<string>();

			foreach ( var entry in entries )
			{
				if ( entry.Trim().Equals( "powershell" , StringComparison.OrdinalIgnoreCase ) )
				{
					continue;
				}

				_process.StandardInput.WriteLine( entry );
			}
		}

		public void Dispose()
		{
			try
			{
				_process.StandardInput.WriteLine( "exit" );

				if ( ! _process.WaitForExit( (int) WaitTimeout.TotalMilliseconds ) )
				{
					_process.Kill();
				}
			}
			catch ( Exception ex )
			{
				System.Diagnostics.Debug.WriteLine( ex );
			}
			finally
			{
				_process.Dispose();
			}
		}
	}
}
