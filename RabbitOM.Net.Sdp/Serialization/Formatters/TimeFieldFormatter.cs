using System;
using System.Linq;

namespace RabbitOM.Net.Sdp.Serialization.Formatters
{
	/// <summary>
	/// Represent a class used to format and parse data
	/// </summary>
	public static class TimeFieldFormatter
	{
		/// <summary>
		/// Format to string the field
		/// </summary>
		/// <param name="formatProvider">the format provider</param>
		/// <param name="field">the field</param>
		/// <returns>returns a string</returns>
		public static string Format(IFormatProvider formatProvider,TimeField field)
		{
			if (field == null)
			{
				return string.Empty;
			}

			return string.Format(formatProvider, "{0} {1}", field.StartTime, field.StopTime);
		}

		/// <summary>
		/// Try to parse
		/// </summary>
		/// <param name="value">the value</param>
		/// <param name="result">the field result</param>
		/// <returns>returns true for a success, otherwise false</returns>
		public static bool TryFrom(string value, out TimeField result)
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

			result = new TimeField()
			{
				StartTime = SessionDescriptorDataConverter.ConvertToLong(tokens.ElementAtOrDefault(0) ?? string.Empty),
				StopTime  = SessionDescriptorDataConverter.ConvertToLong(tokens.ElementAtOrDefault(1) ?? string.Empty),
			};

			return true;
		}
	}
}
