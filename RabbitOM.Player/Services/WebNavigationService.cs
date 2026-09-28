using System;
using System.Diagnostics;

namespace RabbitOM.Player.Services
{
	public sealed class WebNavigationService   // not necesseraty to implemente an interface it's enougth
	{
		public bool TryNavigate(string address , out Exception error)
		{
			error = null;

			if ( ! Uri.TryCreate( address , UriKind.Absolute , out var uri ) )
			{
				return false;
			}

			var process = new Process()
            {
                StartInfo = new ProcessStartInfo() { UseShellExecute = true , FileName = uri.ToString() }
            };

            try
            {
                process.Start();

				return true;
            }
            catch ( Exception ex )
            {
                error = ex;
            }

			return false;
		}
	}
}
