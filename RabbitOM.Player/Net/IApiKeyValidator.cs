using System;

namespace RabbitOM.Player.Net
{
	public interface IApiKeyValidator
	{
		bool TryValidate( string apiKey );
	}
}
