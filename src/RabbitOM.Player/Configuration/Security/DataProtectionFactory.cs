using System;

namespace RabbitOM.Player.Configuration.Security
{
	public static class DataProtectionFactory
	{
		private const string AES_KEY_ENV_VARIABLE = "MEDIA_PLAYER_AES_KEY";

		public static IDataProtection CreateAesDataProtection()
		{
			return CreateAesDataProtection( AES_KEY_ENV_VARIABLE );
		}

		public static IDataProtection CreateAesDataProtection( string keyVariable )
		{
			if ( string.IsNullOrWhiteSpace( keyVariable ) )
			{
				throw new InvalidOperationException( "key env-var has been set" );
			}

			var keyValue = Environment.GetEnvironmentVariable( keyVariable );

			if ( string.IsNullOrWhiteSpace( keyValue ) )
			{
				throw new InvalidOperationException( "no key has been found" );
			}

			var key = Convert.FromHexString( keyValue );

			if ( key.Length == 0 )
			{
				throw new InvalidOperationException( "the key is empty" );
			}

			return new AesDataProtector( key );
		}
	}
}
