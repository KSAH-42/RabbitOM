using System;

namespace RabbitOM.Player.Configuration.Security
{
	internal sealed class AesValueProtectorInfo
	{
		public byte[] Nonce { get; set; }
		public byte[] Tag { get; set; }
		public byte[] CipherValues { get; set; }
		public byte[] PlainValues { get; set; }


		public static AesValueProtectorInfo FromString( string value )
		{
			if ( string.IsNullOrEmpty( value ) )
			{
				throw new ArgumentNullException( nameof( value ) );
			}

			var payload = Convert.FromBase64String(value);

			return new AesValueProtectorInfo()
			{
				Nonce = payload[..12],
				Tag = payload[12..28],
				CipherValues = payload[28..],
				PlainValues = new byte[payload[28..].Length]
			};
		}
	}
}
