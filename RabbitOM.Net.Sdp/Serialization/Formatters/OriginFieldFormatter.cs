using System;
using System.Linq;
using System.Text;

namespace RabbitOM.Net.Sdp.Serialization.Formatters
{
	/// <summary>
	/// Represent a class used to format and parse data
	/// </summary>
	public static class OriginFieldFormatter
	{
		/// <summary>
		/// Format to string the field
		/// </summary>
		/// <param name="formatProvider">the format provider</param>
		/// <param name="field">the field</param>
		/// <returns>returns a string</returns>
		public static string Format(IFormatProvider formatProvider, OriginField field)
		{
			if (field == null)
			{
				return string.Empty;
			}

			var builder = new StringBuilder();

			var userName = !string.IsNullOrWhiteSpace(field.UserName) ? field.UserName : "-";

			builder.AppendFormat(formatProvider, "{0} ", userName);
			builder.AppendFormat(formatProvider, "{0} ", field.SessionId);
			builder.AppendFormat(formatProvider, "{0} ", field.Version);
			builder.AppendFormat(formatProvider, "{0} ", SessionDescriptorDataConverter.ConvertToString(field.NetworkType));
			builder.AppendFormat(formatProvider, "{0} ", SessionDescriptorDataConverter.ConvertToString(field.AddressType));
			builder.AppendFormat(formatProvider, "{0} ", field.Address);

			return builder.ToString();
		}

		/// <summary>
		/// Try to parse
		/// </summary>
		/// <param name="value">the value</param>
		/// <param name="result">the field result</param>
		/// <returns>returns true for a success, otherwise false</returns>
		public static bool TryFrom(string value, out OriginField result)
		{
			result = null;

			if (string.IsNullOrWhiteSpace(value))
			{
				return false;
			}

			var tokens = value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

			if (!tokens.Any())
			{
				return false;
			}

			result = new OriginField()
			{
				UserName    = tokens.ElementAtOrDefault(0) ?? string.Empty,
				SessionId   = tokens.ElementAtOrDefault(1) ?? string.Empty,
				Version     = tokens.ElementAtOrDefault(2) ?? string.Empty,
				Address     = tokens.ElementAtOrDefault(5) ?? string.Empty,
				NetworkType = SessionDescriptorDataConverter.ConvertToNetworkType(tokens.ElementAtOrDefault(3) ?? string.Empty),
				AddressType = SessionDescriptorDataConverter.ConvertToAddressType(tokens.ElementAtOrDefault(4) ?? string.Empty),
			};

			return true;
		}
	}
}
