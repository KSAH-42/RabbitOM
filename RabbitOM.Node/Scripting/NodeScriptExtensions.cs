using System;

namespace RabbitOM.Node.Scripting
{
	using RabbitOM.Node.Scripting.Messages;

	internal static class NodeScriptExtensions
	{
		public static bool TryHandle( this NodeScript script , Message message )
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
