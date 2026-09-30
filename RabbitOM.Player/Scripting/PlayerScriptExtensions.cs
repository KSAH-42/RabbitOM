using System;

namespace RabbitOM.Player.Scripting
{
	public static class PlayerScriptExtensions
	{
		public static bool TryHandle( this PlayerScript script , Message message )
		{
			if ( script == null )
			{
				throw new ArgumentNullException( nameof( script ) );
			}

			try
			{
				script.Handle( message );
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
