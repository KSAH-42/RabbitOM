using System;
using System.Linq;
using System.Text;

namespace RabbitOM.Net.Sdp.Serialization.Formatters
{
	/// <summary>
	/// Represent a class used to format and parse data
	/// </summary>
	public static class EmailFieldFormatter
	{
		/// <summary>
		/// Format to string the field
		/// </summary>
		/// <param name="field">the field</param>
		/// <param name="format">the format</param>
		/// <param name="formatProvider">the format provider</param>
		/// <returns>returns a string</returns>
		public static string Format(EmailField field, string format, IFormatProvider formatProvider)
		{
			if (field == null)
			{
				return string.Empty;
			}

			var builder = new StringBuilder();

			builder.AppendFormat(formatProvider, "{0}", field.Address);

			if (!string.IsNullOrWhiteSpace(field.Name))
			{
				builder.AppendFormat(formatProvider, " ({0})", field.Name);
			}

			return builder.ToString();
		}

		/// <summary>
		/// Try to parse
		/// </summary>
		/// <param name="value">the value</param>
		/// <param name="result">the field result</param>
		/// <returns>returns true for a success, otherwise false</returns>
		public static bool TryParse(string value, out EmailField result)
		{
			result = null;

			if (string.IsNullOrWhiteSpace(value))
			{
				return false;
			}

			var tokens = value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

			if (tokens.Length == 0)
			{
				return false;
			}

			result = new EmailField()
			{
				Address = tokens.ElementAtOrDefault(0),
				Name = tokens.ElementAtOrDefault(1),
			};

			return true;
		}
	}
}
