﻿using System;
using System.Linq;

namespace RabbitOM.Streaming.Sdp.Serialization.Formatters
{
    /// <summary>
    /// Represent a class used to format and parse data
    /// </summary>
    public static class BandwithFieldFormatter
    {
        /// <summary>
        /// Format to string the field
        /// </summary>
        /// <param name="field">the field</param>
        /// <returns>returns a string</returns>
        public static string Format(BandwithField field)
        {
            if (field == null)
            {
                return string.Empty;
            }

            return string.Format( "{0}:{1}", field.Modifier, field.Value);
        }

        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="result">the field result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryParse(string value, out BandwithField result)
        {
            result = null;

            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            var tokens = value.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

            if ( tokens.Length <= 0 )
            {
                return false;
            }

            result = new BandwithField()
            {
                Modifier  = tokens.ElementAtOrDefault(0),
                Value     = DataConverter.ConvertToLong(tokens.ElementAtOrDefault(1)),
            };

            return true;
        }
    }
}
