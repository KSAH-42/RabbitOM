using System;
using System.Linq;

namespace RabbitOM.Streaming.Sdp.Serialization.Formatters
{
    public static class BandwithFieldFormatter
    {
        public static string Format(BandwithField field)
        {
            if (field == null)
            {
                return string.Empty;
            }

            return string.Format( "{0}:{1}", field.Modifier, field.Value);
        }

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
