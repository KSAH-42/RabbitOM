﻿using System;
using System.Text;

namespace RabbitOM.Streaming.Sdp.Serialization.Formatters
{
    /// <summary>
    /// Represent a class used to format and parse data
    /// </summary>
    public static class AttributeFieldFormatter
    {
        /// <summary>
        /// Format to string the field
        /// </summary>
        /// <param name="field">the field</param>
        /// <returns>returns a string</returns>
        public static string Format(AttributeField field)
        {
            if (field == null)
            {
                return string.Empty;
            }

            var builder = new StringBuilder();

            builder.AppendFormat( "{0}", field.Name);

            if (!string.IsNullOrWhiteSpace(field.Value))
            {
                builder.AppendFormat( ":{0}", field.Value);
            }
               
            return builder.ToString();
        }

        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="result">the field result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryParse(string value, out AttributeField result)
        {
            result = null;

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            result = new AttributeField();

            if ( StringPair.TryParse(value, ':', out StringPair field) )
            {
                result.Name = field.First;
                result.Value = field.Second;
            }
            else
            {
                result.Name = value;
            }

            return true;
        }
    }
}
