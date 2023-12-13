using System;
using System.Linq;
using System.Text;

namespace RabbitOM.Net.Sdp.Serialization.Formatters
{
    /// <summary>
    /// Represent a class used to format and parse data
    /// </summary>
    public static class MediaDescriptionFieldFormatter
    {
        /// <summary>
        /// Format to string the field
        /// </summary>
        /// <param name="field">the field</param>
        /// <returns>returns a string</returns>
        public static string Format(MediaDescriptionField field)
        {
            if (field == null)
            {
                return string.Empty;
            }

            var builder = new StringBuilder();

            builder.AppendFormat("{0} ", DataConverter.ConvertToString(field.Type));
            builder.AppendFormat("{0} ", field.Port);
            builder.AppendFormat("{0}/", DataConverter.ConvertToString(field.Protocol));
            builder.AppendFormat("{0} ", DataConverter.ConvertToString(field.Profile));
            builder.AppendFormat("{0} ", field.Payload);

            return builder.ToString();
        }

        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="result">the field result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryParse(string value, out MediaDescriptionField result)
        {
            result = null;

            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            var tokens = value.Split(new char[] { ' ' });

            if (tokens.Length < 4)
            {
                return false;
            }

            var protocolTokens = tokens[2].Split('/');

            if (protocolTokens.Length <= 1)
            {
                return false;
            }

            result = new MediaDescriptionField()
            {
                Type     = DataConverter.ConvertToMediaType(tokens.ElementAtOrDefault(0) ?? string.Empty),
                Port     = DataConverter.ConvertToInt(tokens.ElementAtOrDefault(1) ?? string.Empty),
                Payload  = DataConverter.ConvertToInt(tokens.ElementAtOrDefault(3) ?? string.Empty),
                Protocol = DataConverter.ConvertToProtocolType(protocolTokens.ElementAtOrDefault(0)),
                Profile  = DataConverter.ConvertToProfileType(protocolTokens.ElementAtOrDefault(1)),
            };

            return true;
        }
    }
}
