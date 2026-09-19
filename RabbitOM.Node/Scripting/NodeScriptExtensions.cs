using System;

namespace RabbitOM.Node.Scripting
{
	internal static class NodeScriptExtensions
	{
		public static bool TryHandle( this NodeScript script , object sender , EventArgs e )
		{
			if ( script == null )
			{
				throw new ArgumentNullException( nameof( script ) );
			}

			try
			{
				script.Handle( sender , e );
				return true;
			}
			catch ( Exception ex )
			{
				System.Diagnostics.Debug.WriteLine( ex );
			}

			return false;
		}
	}
}
