using System;

namespace RabbitOM.Player.Configuration
{
	public interface IDataProtection
	{
		string UnProtect( string value );
	}
}
