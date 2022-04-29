using System;
using System.Linq;
using System.Text;

namespace RabbitOM.Net.Sdp.Serialization.Formatters
{
	/// <summary>
	/// Represent a class used to format and parse data
	/// </summary>
	public static class RtpMapAttributeValueFormatter
	{
		/// <summary>
		/// Format to string the field
		/// </summary>
		/// <param name="field">the field</param>
		/// <param name="format">the format</param>
		/// <param name="formatProvider">the format provider</param>
		/// <returns>returns a string</returns>
		public static string Format(RtpMapAttributeValue field, string format, IFormatProvider formatProvider)
		{
			if (field == null)
			{
				return string.Empty;
			}

			var builder = new StringBuilder();

			builder.AppendFormat(formatProvider, "{0}", field.ClockRate);

			if (!string.IsNullOrWhiteSpace(field.Encoding))
			{
				builder.AppendFormat(formatProvider, "{0}/{1}", field.Encoding, field.ClockRate);
			}

			if (!field.Extensions.IsEmpty)
			{
				foreach (var extension in field.Extensions)
				{
					builder.AppendFormat(formatProvider, " {0}", extension);
				}
			}

			return builder.ToString();
		}

		/// <summary>
		/// Try to parse
		/// </summary>
		/// <param name="value">the value</param>
		/// <param name="result">the field result</param>
		/// <returns>returns true for a success, otherwise false</returns>
		public static bool TryParse(string value, out RtpMapAttributeValue result)
		{
			result = null;

			if (string.IsNullOrWhiteSpace(value))
			{
				return false;
			}

			var tokens = value.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

			if (!tokens.Any())
			{
				return false;
			}

			result = new RtpMapAttributeValue()
			{
				PayloadType = SessionDescriptorDataConverter.ConvertToByte(tokens.FirstOrDefault())
			};

			if (tokens.Length > 1)
			{
				var elements = tokens[1].Split(new char[] { '/' });

				result.Encoding = elements.FirstOrDefault();

				if (elements.Length > 1)
				{
					result.ClockRate = SessionDescriptorDataConverter.ConvertToUInt(elements.ElementAtOrDefault(1));
				}
			}

			for (int i = 2; i < tokens.Length; ++i)
			{
				result.Extensions.TryAdd(tokens[i]);
			}

			return true;
		}
	}
}
