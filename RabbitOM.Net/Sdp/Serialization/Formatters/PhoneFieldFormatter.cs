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
        /// <returns>returns a string</returns>
        public static string Format(PhoneField field)
        {
            return string.Format("{0}" , field?.Value ?? string.Empty );
        }

        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="result">the field result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryParse(string value, out PhoneField result)
        {
            result = default;

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            result = new PhoneField()
            {
                Value = value
            };

            return true;
        }
    }
}
