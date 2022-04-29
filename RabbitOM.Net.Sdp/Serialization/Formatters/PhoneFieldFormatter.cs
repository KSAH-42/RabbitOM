using System;

namespace RabbitOM.Net.Sdp.Serialization.Formatters
{
	/// <summary>
	/// Represent a class used to format and parse data
	/// </summary>
	public static class PhoneFieldFormatter
	{
		/// <summary>
		/// Format to string the field
		/// </summary>
		/// <param name="field">the field</param>
		/// <param name="format">the format</param>
		/// <param name="formatProvider">the format provider</param>
		/// <returns>returns a string</returns>
		public static string Format(PhoneField field, string format, IFormatProvider formatProvider)
		{
			// TODO: ideally this class must format and fill properties the phone prefix like +33 

			return field?.Value ?? string.Empty;
		}

		/// <summary>
		/// Try to parse
		/// </summary>
		/// <param name="value">the value</param>
		/// <param name="result">the field result</param>
		/// <returns>returns true for a success, otherwise false</returns>
		public static bool TryParse(string value, out PhoneField result)
		{
			// TODO: Ideally this class must parse using the dot and the phone prefix like +33 

			result = new PhoneField()
			{
				Value = value
			};

			return true;
		}
	}
}
