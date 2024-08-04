using System;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Sdp.Serialization.Formatters
{
    /// <summary>
    /// Represent a class used to format and parse data
    /// </summary>
    public static class RepeatFieldFormatter
    {
        /// <summary>
        /// Gets the tokenizer used for parsing
        /// </summary>
        public static Tokenizer Tokenizer { get; } = new Tokenizer();





        /// <summary>
        /// Format to string the field
        /// </summary>
        /// <param name="field">the field</param>
        /// <returns>returns a string</returns>
        public static string Format(RepeatField field)
        {
            var builder = new StringBuilder();

            builder.AppendFormat("{0} ", field.RepeatInterval.StartTime);
            builder.AppendFormat("{0} ", field.RepeatInterval.StopTime);
            builder.AppendFormat("{0} ", field.ActiveDuration.StartTime);
            builder.AppendFormat("{0} ", field.ActiveDuration.StopTime);

            return builder.ToString();
        }

        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="result">the field result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryParse(string value, out RepeatField result)
        {
            result = null;

            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            var tokens = Tokenizer.Split( value );

            if ( ! tokens.Any() )
            {
                return false;
            }

            result = new RepeatField()
            {
                RepeatInterval = new ValueTime( DataConverter.ConvertToLong(tokens.ElementAtOrDefault(0) ), DataConverter.ConvertToLong(tokens.ElementAtOrDefault(1) ) ),
                ActiveDuration = new ValueTime( DataConverter.ConvertToLong(tokens.ElementAtOrDefault(2) ), DataConverter.ConvertToLong(tokens.ElementAtOrDefault(3) ) ),
            };

            return true;
        }
    }
}
