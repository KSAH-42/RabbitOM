using System;
using System.Diagnostics;

namespace RabbitOM.Player.Services
{
	public sealed class WebNavigationService   // we don't implemente an interface it's enougth
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
