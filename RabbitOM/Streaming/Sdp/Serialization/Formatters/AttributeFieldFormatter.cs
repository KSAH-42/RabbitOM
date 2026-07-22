using System;
using System.Text;

namespace RabbitOM.Streaming.Sdp.Serialization.Formatters
{
    public static class AttributeFieldFormatter
    {
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
