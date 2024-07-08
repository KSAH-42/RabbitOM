using System;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Sdp.Serialization.Formatters
{
    /// <summary>
    /// Represent a class used to format and parse data
    /// </summary>
    public static class EncryptionFieldFormatter
    {
        /// <summary>
        /// Format to string the field
        /// </summary>
        /// <param name="field">the field</param>
        /// <returns>returns a string</returns>
        public static string Format(EncryptionField field)
        {
            if (field == null)
            {
                return string.Empty;
            }

            var builder = new StringBuilder();

            builder.AppendFormat("{0}", field.Method);

            if (!string.IsNullOrWhiteSpace(field.Key))
            {
                builder.AppendFormat( ":{0}", field.Key);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="result">the field result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryParse(string value, out EncryptionField result)
        {
            result = null;

            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            var tokens = value.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

            if (tokens.Length == 0)
            {
                return false;
            }

            result = new EncryptionField()
            {
                Method = tokens.ElementAtOrDefault(0),
                Key    = tokens.ElementAtOrDefault(1),
            };

            return true;
        }
    }
}
