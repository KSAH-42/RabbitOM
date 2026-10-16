using System;
using System.Security.Cryptography;
using System.Text;

namespace RabbitOM.Player.Configuration.Security
{
	public sealed class AesDataProtector : IDataProtection , IDisposable
	{
		private readonly AesGcm _algorithm;

		public AesDataProtector( byte[] key )
		{
			if ( key == null )
			{
				throw new ArgumentNullException( nameof( key ) );
			}

			if ( key.Length == 0 )
			{
				throw new ArgumentException( nameof( key ) );
			}

			_algorithm = new AesGcm( key , 16 );
		}

		public void Dispose()
		{
			_algorithm.Dispose();
		}

		public string UnProtect( string value )
		{
			if ( string.IsNullOrEmpty( value ) )
			{
				return string.Empty;
			}

			var info = AesDataProtectorInfo.FromString( value );

			_algorithm.Decrypt( info.Nonce , info.CipherValues , info.Tag , info.PlainValues );

			return Encoding.UTF8.GetString(info.PlainValues);
		}
	}
}
