using System;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Sdp.Serialization.Formatters
{
    public static class EncryptionFieldFormatter
    {
        public static string Format(EncryptionField field)
        {
            if (field == null)
            {
                return string.Empty;
            }

            var builder = new StringBuilder();

            builder.AppendFormat("{0}", field.Method);

            if (!string.IsNullOrWhiteSpace(field.Key))
            {
                builder.AppendFormat( ":{0}", field.Key);
            }

            return builder.ToString();
        }

        public static bool TryParse(string value, out EncryptionField result)
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

            result = new EncryptionField()
            {
                Method = tokens.ElementAtOrDefault(0),
                Key    = tokens.ElementAtOrDefault(1),
            };

            return true;
        }
    }
}
