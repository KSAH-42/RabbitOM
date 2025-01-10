using System;
using System.Linq;

namespace RabbitOM.Streaming.Sdp.Serialization.Formatters
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
        /// <returns>returns a string</returns>
        public static string Format(EmailField field)
        {
            if (field == null)
            {
                return string.Empty;
            }

            if ( string.IsNullOrWhiteSpace(field.Name) )
            {
                return field.Address;
            }

            return string.Format( "{0} ({1})" , field.Address , field.Name );
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

            if ( tokens.Length <= 0 )
            {
                return false;
            }

            result = new EmailField()
            {
                Address = tokens.ElementAtOrDefault(0),
                Name    = string.Join( " " , tokens.Skip(1) )
                                .Replace( "(" , "" )
                                .Replace( ")" , "" ) ,
            };

            return true;
        }
    }
}
