using System;
using System.Diagnostics;

namespace RabbitOM.Player.Services
{
	public sealed class WebNavigationService
	{
		public void Navigate(string address )
		{
			var uri = new Uri( address , UriKind.Absolute );

			var process = new Process()
            {
                StartInfo = new ProcessStartInfo() { UseShellExecute = true , FileName = uri.ToString() }
            };

            process.Start();
		}
	}
}
