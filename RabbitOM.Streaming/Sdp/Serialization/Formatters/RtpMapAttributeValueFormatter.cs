using System;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Sdp.Serialization.Formatters
{
    /// <summary>
    /// Represent a class used to format and parse data
    /// </summary>
    public static class RtpMapAttributeValueFormatter
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
        public static string Format(RtpMapAttributeValue field)
        {
            if (field == null)
            {
                return string.Empty;
            }

            var builder = new StringBuilder();

            builder.AppendFormat("{0} ", field.PayloadType);

            if (!string.IsNullOrWhiteSpace(field.Encoding))
            {
                builder.AppendFormat("{0}/{1}", field.Encoding, field.ClockRate);
            }

            if (!field.Extensions.IsEmpty)
            {
                foreach (var extension in field.Extensions)
                {
                    builder.AppendFormat(" {0}", extension);
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

            var tokens = Tokenizer.Split( DataConverter.ReArrange( value , '/' ) );

            if ( tokens.Length <= 1  )
            {
                return false;
            }

            result = new RtpMapAttributeValue()
            {
                PayloadType = DataConverter.ConvertToByte( tokens.FirstOrDefault() )
            };

            if ( ! string.IsNullOrWhiteSpace( tokens.ElementAtOrDefault( 1 ) ) )
            {
                var encodingTokens = tokens.ElementAtOrDefault( 1 ).Split( new char[] { '/' } , StringSplitOptions.RemoveEmptyEntries );

                if ( encodingTokens.Length > 0 )
                {
                    result.Encoding  = encodingTokens.ElementAtOrDefault( 0 );
                    result.ClockRate = DataConverter.ConvertToUInt( encodingTokens.ElementAtOrDefault( 1 ) );
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
