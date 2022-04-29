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
        /// <param name="format">the format</param>
        /// <param name="formatProvider">the format provider</param>
        /// <returns>returns a string</returns>
        public static string Format(MediaDescriptionField field, string format, IFormatProvider formatProvider )
		{
            if ( field == null )
			{
                return string.Empty;
			}

            var builder = new StringBuilder();

            builder.AppendFormat(formatProvider, "{0} ", SessionDescriptorDataConverter.ConvertToString(field.Type));
            builder.AppendFormat(formatProvider, "{0} ", field.Port);
            builder.AppendFormat(formatProvider, "{0} ", SessionDescriptorDataConverter.ConvertToString(field.Protocol));
            builder.AppendFormat(formatProvider, "{0} ", SessionDescriptorDataConverter.ConvertToString(field.Profile));
            builder.AppendFormat(formatProvider, "{0} ", field.Payload);

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

            if ( string.IsNullOrWhiteSpace(value) )
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
                Type     = SessionDescriptorDataConverter.ConvertToMediaType(tokens.ElementAtOrDefault(0) ?? string.Empty),
                Port     = SessionDescriptorDataConverter.ConvertToInt(tokens.ElementAtOrDefault(1) ?? string.Empty),
                Payload  = SessionDescriptorDataConverter.ConvertToInt(tokens.ElementAtOrDefault(3) ?? string.Empty),
                Protocol = SessionDescriptorDataConverter.ConvertToProtocolType(protocolTokens.ElementAtOrDefault(0)),
                Profile  = SessionDescriptorDataConverter.ConvertToProfileType(protocolTokens.ElementAtOrDefault(1)),
            };

            return true;
        }
    }
}
