using System;

namespace RabbitOM.Player.Scripting
{
	public sealed class PlayerScriptProxy : MarshalByRefObject
	{
		public override object InitializeLifetimeService()
		{
			return null;
		}
	}
}
