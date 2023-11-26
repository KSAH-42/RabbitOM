using System;
using System.Net;
using System.Net.Sockets;

namespace RabbitOM.Net.Sdp.Validation
{
	/// <summary>
	/// Represent a validator help class
	/// </summary>
	internal static class ValidatorHelper
	{
		/// <summary>
		/// Try validate
		/// </summary>
		/// <param name="address">the address</param>
		/// <param name="addressType">the address type</param>
		/// <returns>returns true for a success, otherwise false.</returns>
		/// <remarks>
		///		<para>The address type and the network type must be defined</para>
		///		<para>The ip address value must be well formed</para>
		///		<para>Empty ip addresss are allowed and are considered as loopback address</para>
		/// </remarks>
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
