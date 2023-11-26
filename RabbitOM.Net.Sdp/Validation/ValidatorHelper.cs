using System;
using System.Net;
using System.Net.Sockets;

namespace RabbitOM.Net.Sdp.Validation
{
	/// <summary>
	/// Represent a validator help class
	/// </summary>
	public static class ValidatorHelper
	{
		/// <summary>
		/// Try validate
		/// </summary>
		/// <param name="address">the address</param>
		/// <param name="addressType">the address type</param>
		/// <returns>returns true for a success, otherwise false.</returns>
		public static bool TryValidateAddress( string address, AddressType addressType )
		{
			if ( addressType == AddressType.None )
			{
				return false;
			}

			if ( ! IPAddress.TryParse( address, out IPAddress ipAddress ) || ipAddress == null )
			{
				return string.IsNullOrWhiteSpace( address ) ;
			}

			return (addressType == AddressType.IPV4 && ipAddress.AddressFamily == AddressFamily.InterNetwork )
				|| (addressType == AddressType.IPV6 && ipAddress.AddressFamily == AddressFamily.InterNetworkV6 )
				;
		}

	}
}
