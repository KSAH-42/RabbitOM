using System;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Net.Sdp.Serialization.Formatters
{
    /// <summary>
    /// Represent a class used to format and parse data
    /// </summary>
    public static class OriginFieldFormatter
    {
        /// <summary>
        /// Format to string the field
        /// </summary>
        /// <param name="field">the field</param>
        /// <returns>returns a string</returns>
        public static string Format(OriginField field)
        {
            if (field == null)
            {
                return string.Empty;
            }

            var builder = new StringBuilder();

            var userName = !string.IsNullOrWhiteSpace(field.UserName) ? field.UserName : "-";

            builder.AppendFormat("{0} ", userName);
            builder.AppendFormat("{0} ", field.SessionId);
            builder.AppendFormat("{0} ", field.Version);
            builder.AppendFormat("{0} ", DataConverter.ConvertToString(field.NetworkType));
            builder.AppendFormat("{0} ", DataConverter.ConvertToString(field.AddressType));
            builder.AppendFormat("{0} ", field.Address);

            return builder.ToString();
        }

        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="result">the field result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryParse(string value, out OriginField result)
        {
            result = null;

            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            var tokens = DataConverter.ReArrange( value , '/' ).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if ( tokens.Length <= 0 )
            {
                return false;
            }

            result = new OriginField()
            {
                UserName    = tokens.ElementAtOrDefault(0) ,
                SessionId   = DataConverter.ConvertToULong( tokens.ElementAtOrDefault(1) ),
                Version     = DataConverter.ConvertToULong( tokens.ElementAtOrDefault(2) ),
                NetworkType = DataConverter.ConvertToNetworkType( tokens.ElementAtOrDefault(3) ),
                AddressType = DataConverter.ConvertToAddressType( tokens.ElementAtOrDefault(4) ),
                Address     = tokens.ElementAtOrDefault(5) ,
            };

            return true;
        }
    }
}
