using System;

namespace RabbitOM.Player.Net
{
	public sealed class HttpApiKeyValidator : IApiKeyValidator
	{
		private readonly string _apiKey;

		public HttpApiKeyValidator( string apiKey)
		{
			if ( string.IsNullOrWhiteSpace( apiKey ) )
			{
				throw new ArgumentNullException( nameof( apiKey ) );
			}

			_apiKey = apiKey.Trim();
		}

		public bool TryValidate( string apiKey )
		{
			if ( string.IsNullOrWhiteSpace( apiKey ) )
			{
				return false;
			}

			return StringComparer.OrdinalIgnoreCase.Equals( _apiKey , apiKey.Trim() );
		}
	}
}
