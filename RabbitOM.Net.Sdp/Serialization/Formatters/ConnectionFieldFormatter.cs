using System;
using System.Linq;
using System.Text;

namespace RabbitOM.Net.Sdp.Serialization.Formatters
{
    /// <summary>
    /// Represent a class used to format and parse data
    /// </summary>
    public static class ConnectionFieldFormatter
    {
        /// <summary>
        /// Format to string the field
        /// </summary>
        /// <param name="field">the field</param>
        /// <returns>returns a string</returns>
        public static string Format(ConnectionField field)
        {
            if (field == null)
            {
                return string.Empty;
            }

            var builder = new StringBuilder();

            var address = DataConverter.ConvertToLoopBackIfEmpty(field.Address, field.AddressType);

            builder.AppendFormat(
                  "{0} {1} {2}"
                , DataConverter.ConvertToString(field.NetworkType)
                , DataConverter.ConvertToString(field.AddressType)
                , address 
                );

            if ( ! string.IsNullOrWhiteSpace( address ) )
            {
                builder.AppendFormat( "/{0}" , field.TTL );
            }

            return builder.ToString();
        }

        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="result">the field result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryParse(string value, out ConnectionField result)
        {
            result = null;

            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            var tokens = value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (tokens.Length < 3)
            {
                return false;
            }

            result = new ConnectionField()
            {
                NetworkType = DataConverter.ConvertToNetworkType(tokens.ElementAtOrDefault(0) ?? string.Empty),
                AddressType = DataConverter.ConvertToAddressType(tokens.ElementAtOrDefault(1) ?? string.Empty),
                Address     = tokens.ElementAtOrDefault(2),
                TTL         = DataConverter.ConvertToByteFromTTLFormat(tokens.ElementAtOrDefault(2)),
            };

            return true;
        }
    }
}
