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
				throw new ArgumentNullException( nameof( keyVariable ) );
			}

			var keyValue = Environment.GetEnvironmentVariable( keyVariable );

			if ( string.IsNullOrWhiteSpace( keyValue ) )
			{
				throw new ArgumentException( "env-var is not found" , nameof( keyVariable ) );
			}

			var key = Convert.FromHexString( keyValue );

			if ( key.Length == 0 )
			{
				throw new ArgumentException( "env-var is empty" , nameof( keyVariable ) );
			}

			return new AesDataProtector( key );
		}
	}
}
