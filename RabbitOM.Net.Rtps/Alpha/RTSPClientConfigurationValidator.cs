using System;
using System.Net;
using System.Net.Sockets;

namespace RabbitOM.Net.Rtsp.Alpha
{
    /// <summary>
    /// Represent a field validator
    /// </summary>
    internal static class RTSPClientConfigurationValidator
    {
        /// <summary>
        /// Throw an exception if the port number is invalid
        /// </summary>
        /// <param name="port">the port</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public static void EnsurePortNumber( int port )
        {
            if ( port <= 0 )
            {
                throw new ArgumentOutOfRangeException(nameof(port),"Invalid port number");
            }
        }

        /// <summary>
        /// Throw an exception if the ttl is invalid
        /// </summary>
        /// <param name="value">the value</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public static void EnsureTTLValue(byte value)
        {
            if (value == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Invalid TTL value");
            }
        }

        /// <summary>
        /// Throw an exception if the timeout value is invalid
        /// </summary>
        /// <param name="value">the value</param>
        /// <exception cref="ArgumentException"/>
        public static void EnsureTimeoutValue(TimeSpan value)
        {
            if ( value == TimeSpan.Zero )
            {
                throw new ArgumentException(nameof(value), "Invalid timeout value");
            }
        }


        /// <summary>
        /// Throw an exception if the timeout value is invalid
        /// </summary>
        /// <param name="value">the value</param>
        /// <exception cref="UriFormatException"/>
        public static void EnsureUriWellFormed( string uri )
        {
            if ( ! RTSPUri.IsWellFormed( uri ) )
            {
                throw new UriFormatException( nameof(uri) );
            }
        }

        /// <summary>
        /// Throw an exception if it is not a multicast ipAddress
        /// </summary>
        /// <param name="ipAddress">the ip address</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="NotSupportedException"/>
        public static void EnsureMulticastAddress(string ipAddress)
        {
            if (ipAddress == null)
            {
                throw new ArgumentNullException(nameof(ipAddress));
            }

            var address = IPAddress.Parse(ipAddress.Trim());

            if (address.AddressFamily != AddressFamily.InterNetwork)
            {
                throw new NotSupportedException("Address family not supported");
            }

            var bytesAddress = address.GetAddressBytes();

            if (bytesAddress == null || bytesAddress.Length < 4)
            {
                throw new NotSupportedException("Bad multicast address format");
            }

            if (bytesAddress[0] < 0xE0 || bytesAddress[0] > 0xEF )
            {
                throw new NotSupportedException("Bad multicast address format");
            }
        }
    }
}
